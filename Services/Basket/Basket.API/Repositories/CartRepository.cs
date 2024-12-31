using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Basket.API.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IDistributedCache _redisCache;

        public CartRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }


        public async Task<Cart?> GetAsync(string userName)
        {
            string? cart = await _redisCache.GetStringAsync(userName);
            if (cart is null)
                return null;

            return JsonSerializer.Deserialize<Cart?>(cart);
        }

        public async Task<Cart?> UpdateAsync(Cart cart)
        {
            await _redisCache.SetStringAsync(
                key: cart.UserName,
                value: JsonSerializer.Serialize(cart));

            return await GetAsync(cart.UserName);
        }

        public async Task DeleteAsync(string userName)
        {
            await _redisCache.RemoveAsync(key: userName);
        }

    }
}
