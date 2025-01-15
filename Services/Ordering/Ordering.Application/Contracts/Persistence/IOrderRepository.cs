

using Ordering.Domain.Entities;

namespace Ordering.Application.Contracts.Persistence
{
    public interface IOrderRepository : IAsyncRespository<Order>
    {
        Task<IEnumerable<Order>> GetByUserName(string userName);
    }
}
