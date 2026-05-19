using Amazon.core;
using Amazon.core.Entities;
using Amazon.core.Entities.Order_Aggregate;
using Amazon.core.Repositories;
using Amazon.core.Services;
using Amazon.core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        //private readonly IGenericRepository<Product> _productsRepo;
        //private readonly IGenericRepository<DeliveryMethod> _deliveryMethod;
        //private readonly IGenericRepository<Order> _orderRepository;

        public OrderService(IBasketRepository basketRepository,
            IUnitOfWork unitOfWork,
            IPaymentService paymentService
            //IGenericRepository<Product> ProductsRepo,
            //IGenericRepository<DeliveryMethod> deliveryMethod,
            //IGenericRepository<Order> orderRepository)
            )
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
            //_productsRepo = ProductsRepo;
            //_deliveryMethod = deliveryMethod;
            //_orderRepository = orderRepository;
        }
        public async Task<Order?>? CreateOrderAsync(string BuyerEmail, string BasketId, int deliveryMethodId, Address shippingAddress)
        {
            //Get Basket from Baskets Repo

            var basket = await _basketRepository.GetBasket(BasketId);

            //Get Selected Items(which are in Basket) from Products Repo
            var orderItems = new List<OrderItem>();

            foreach (var item in basket.basketItems)
            {
                var products = _unitOfWork.Repository<Product>();

                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.ProductId);

                var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);

                var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantinty);

                orderItems.Add(orderItem);
            }

            //Calculate Subtotal
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

            //Get Delivery Method from DeliveryMethods Repo

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            // Create Order

            var spec = new OrderWithPaymentIntentIdSpecifications(basket.PaymentIntentId);

            var existingOrder = await _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(spec); // ensure if there is already an order with the same PaymentIntentId

            if(existingOrder is not null)
            {
                _unitOfWork.Repository<Order>().Delete(existingOrder); // deletes it
                _paymentService.CreateOrUpdatePaymentIntent(BasketId); // uodate the amount in case the previous order had different amount
            }

            var order = new Order(BuyerEmail, shippingAddress, deliveryMethod, orderItems, subTotal,basket.PaymentIntentId);
            _unitOfWork.Repository<Order>().Add(order);

            var result = await _unitOfWork.complete();
            if (result > 0)
                return order;

            return null;

            //Save To database
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return deliveryMethods;
        }

        public Task<Order?> GetOrderByIdForUserAsync(string buyerEmail, int orderId)
        {
            var spec = new OrdersSpecification(buyerEmail, orderId);

            var order = _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(spec);

            if (order is null) return null;

            return order;
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersSpecification(buyerEmail);

            var orders= _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);

            return orders;
        }
    }
}
