using Amazon.core;
using Amazon.core.Entities;
using Amazon.core.Entities.Order_Aggregate;
using Amazon.core.Repositories;
using Amazon.core.Services;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.service
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration configuration,
            IBasketRepository basketRepository,
            IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string BasketId) // el payment intent beyt3ml elawal ba3deen el order bnb3tloh el payment intent
        {
            StripeConfiguration.ApiKey = _configuration["StripeSetting:SecretKey"]; // Secret key gebnah mn stripe

            var basket =await _basketRepository.GetBasket(BasketId);

            if (basket is null) return null;

            var ShippingPrice = 0m;

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod =await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);

                basket.ShippingCost = deliveryMethod.Cost;

                ShippingPrice = deliveryMethod.Cost;
            } // mehtagen ma3lomet el total price w elshipping cost

            if (basket?.basketItems.Count > 0)
            {
                foreach (var item in basket.basketItems)
                {
                    var product = await _unitOfWork.Repository<core.Entities.Product>().GetByIdAsync(item.ProductId);
                    if(item.Price!=product.Price)
                        item.Price = product.Price; // bngeb el price el hakeke mn table el product 3an tarek product id el fe el basket
                }
            }

            PaymentIntent paymentIntent;

            var service = new PaymentIntentService();

            if (string.IsNullOrEmpty(basket.PaymentIntentId)) // law awl mara a3mal payment intent
            {
                var options = new PaymentIntentCreateOptions()  // for creating
                {
                    Amount = (long)((basket.basketItems.Sum(item => item.Price*item.Quantinty) + (long)(basket.ShippingCost)) * 100), // *100 3shan Amount bt5azen 5rosh msh pounds
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" } 
                };
                paymentIntent = await service.CreateAsync(options);

                basket.PaymentIntentId= paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else // fe halet 3andna payment intent fa ehna 7n3dl bs 3la el intent
            {
                var options = new PaymentIntentUpdateOptions() //for updating
                {

                    Amount = (long)((basket.basketItems.Sum(item => item.Price * item.Quantinty) + (long)(basket.ShippingCost)) * 100)
                };
                await service.UpdateAsync(basket.PaymentIntentId, options);
            }
            await _basketRepository.UpdateBasket(basket); //3shan el updates el 3mlnaha swa3 3la price aw ay haga tanya

            return basket;
        }
    }
}
