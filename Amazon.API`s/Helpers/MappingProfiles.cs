using Amazon.API_s.DTO_s;
using Amazon.core.Entities;
using Amazon.core.Entities.Identity;
using Amazon.core.Entities.Order_Aggregate;
using AutoMapper;

namespace Amazon.API_s.Helpers
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d=>d.PictureUrl ,o=>o.MapFrom<ProductPictureUrlResolver>());
            CreateMap<RegisterDto, AppUser>().ForMember(a => a.UserName, u => u.MapFrom(s => s.Email.Split( new[] { '@' })[0]));
            CreateMap<Amazon.core.Entities.Identity.Address, AddressDto>().ReverseMap();

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<AddressDto, Amazon.core.Entities.Order_Aggregate.Address>();

            CreateMap<Order,OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(s => s.DeliveryMethod.Cost))
                .ForMember(d => d.Total, o => o.MapFrom(s => s.GetTotal()));

            CreateMap<OrderItem,OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.productItem.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.productItem.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.productItem.PictureUrl));
        }
    }
}
