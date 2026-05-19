using Amazon.core.Entities;
using Amazon.core.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Amazon.repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer connection)
        {
            _database=connection.GetDatabase();
        }
        public async Task<bool> DeleteBasket(string Id)
        {
            return await _database.KeyDeleteAsync(Id); 
        }

        public async Task<CustomerBasket?> GetBasket(string Id)
        {
            var basket = await _database.StringGetAsync(Id);
            return basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket> UpdateBasket(CustomerBasket basket)
        {
            var createdOrUpdatedBasket = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(1));
            if (createdOrUpdatedBasket is false) return null;
            return await GetBasket(basket.Id);
        }
    }
}
