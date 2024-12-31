using Basket.API.Entities;

namespace Basket.API.Repositories
{
    public interface ICartRepository
    {
        Task<Cart?> GetAsync(string userName);
        Task<Cart?> UpdateAsync(Cart cart);
        Task DeleteAsync(string userName);
    }
}
