

using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        private readonly OrderingContext _context;
        public OrderRepository(OrderingContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetByUserName(string userName)
        {
            return await _context.Orders.Where(w => w.UserName == userName).ToListAsync();
        }
    }
}
