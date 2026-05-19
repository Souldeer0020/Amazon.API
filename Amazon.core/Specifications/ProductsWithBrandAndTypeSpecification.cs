using Amazon.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.core.Specifications
{
    public class ProductsWithBrandAndTypeSpecification : BaseSpecification<Product> // this class was made because we want to set the value of includes for product so we cant just simply add it to the parameterless ctor of base specification because by this we will violate the generic 
    {
        public ProductsWithBrandAndTypeSpecification(int id)
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }
        public ProductsWithBrandAndTypeSpecification(ProductSpecParams specParams) : base(
            p => 
            (string.IsNullOrEmpty(specParams.Search) || p.Name.ToLower().Contains(specParams.Search)) &&
            (!specParams.brandId.HasValue || p.ProductBrandId == specParams.brandId) &&
            (!specParams.typeId.HasValue || p.ProductTypeId == specParams.typeId)
            )
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p=>p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(p=>p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
            applyPagination((specParams.PageSize * (specParams.PageIndex - 1)), specParams.PageSize);
        }
    }
}
