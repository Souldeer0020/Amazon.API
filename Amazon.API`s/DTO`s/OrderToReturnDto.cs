using Amazon.core.Entities.Order_Aggregate;

namespace Amazon.API_s.DTO_s
{
    public class OrderToReturnDto
    {
        public string buyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public string Status { get; set; }
        public Address ShippingAddress { get; set; }
        //[NotMapped]
        public string DeliveryMethod { get; set; }
        public decimal DeliveryMethodCost { get; set; }
        public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
