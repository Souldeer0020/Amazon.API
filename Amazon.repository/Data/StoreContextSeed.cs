using Amazon.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Amazon.repository.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedData(StoreContext context)
        {
            if(!context.ProductBrands.Any())
            {
                var productBrands = File.ReadAllText("../Amazon.repository/Data/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize <List<ProductBrand>>(productBrands);

                if(brands is not null && brands.Count != 0)
                {
                    foreach (var brand in brands)
                        await context.Set<ProductBrand>().AddAsync(brand);

                    await context.SaveChangesAsync();
                }
            }
            if (!context.ProductTypes.Any())
            {
                var productTypes = File.ReadAllText("../Amazon.repository/Data/DataSeed/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(productTypes);

                if (types is not null && types.Count != 0)
                {
                    foreach (var type in types)
                        await context.Set<ProductType>().AddAsync(type);

                    await context.SaveChangesAsync();
                }
            }
            if (!context.Products.Any())
            {
                var productss = File.ReadAllText("../Amazon.repository/Data/DataSeed/products.json");
                var products1 = JsonSerializer.Deserialize<List<Product>>(productss);

                if (products1 is not null && products1.Count != 0)
                {
                    foreach (var product in products1)
                        await context.Set<Product>().AddAsync(product);

                    await context.SaveChangesAsync();
                }
            }

        }
    }
}
