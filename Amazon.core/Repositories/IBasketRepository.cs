using Amazon.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.core.Repositories
{
    public interface IBasketRepository
    {
        public Task<CustomerBasket?> GetBasket(string Id);

        public Task<CustomerBasket> UpdateBasket(CustomerBasket basket);
        public Task<bool> DeleteBasket(string Id);
    }
}
