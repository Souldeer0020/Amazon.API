using Amazon.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.core.Specifications
{
    public class ProductWithFilterationTypeSpec : BaseSpecification<Product>
    {
        public ProductWithFilterationTypeSpec(ProductSpecParams specParams) : base(
            p => (!specParams.brandId.HasValue || p.ProductBrandId == specParams.brandId) &&
            (!specParams.typeId.HasValue || p.ProductTypeId == specParams.typeId)
            )
        {


        }
    }
}
