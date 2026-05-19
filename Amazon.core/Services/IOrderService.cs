using Amazon.core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.core.Services
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int deliveryMethodId, Address shippingAddress);
        Task<Order> GetOrderByIdForUserAsync(string buyerEmail, int orderId);
        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();

    }
}
