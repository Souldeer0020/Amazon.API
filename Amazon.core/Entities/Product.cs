using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.core.Entities
{
    public class Product :BaseEntity
    {
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int ProductBrandId { get; set; } // very very important for setting data
        public ProductBrand ProductBrand { get; set; } // important for getting data
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
    }
}
