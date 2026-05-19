using Amazon.core.Entities;

namespace Amazon.API_s.DTO_s
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int ProductBrandId { get; set; } // very very important for setting data
        public string ProductBrand { get; set; } // important for getting data
        public int ProductTypeId { get; set; }
        public string ProductType { get; set; }
    }
}
