using Amazon.API_s.DTO_s;
using Amazon.core.Entities;
using AutoMapper;

namespace Amazon.API_s.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!String.IsNullOrEmpty(source.PictureUrl))
                return $"{_configuration["ApiBaseUrl"]}{source.PictureUrl}";
            return string.Empty;
        }
    }
}
