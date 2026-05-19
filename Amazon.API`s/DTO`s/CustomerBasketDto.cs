using Amazon.core.Entities;

namespace Amazon.API_s.DTO_s
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }
        public string? PaymentIntentId { get; set; }
        public int? DeliveryMethodId { get; set; }
        public string? ClientSecret { get; set; }
        public decimal? ShippingCost { get; set; }
        public List<BasketItemDto> basketItems { get; set; } = new List<BasketItemDto>();
        public CustomerBasketDto(string id)
        {
            Id = id;
        }
    }
}
