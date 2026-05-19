using Amazon.API_s.DTO_s;
using Amazon.core.Entities.Order_Aggregate;
using AutoMapper;
using AutoMapper.Execution;

namespace Amazon.API_s.Helpers
{
    public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!String.IsNullOrEmpty(source.productItem.PictureUrl))
                return $"{_configuration["ApiBaseUrl"]}{source.productItem.PictureUrl}";
            return string.Empty;
        }
    }
}
