

using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderingContext : DbContext
    {
        public OrderingContext(DbContextOptions<OrderingContext> options) : base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        {
                            entry.Entity.CreateDate = DateTime.UtcNow;
                            entry.Entity.CreatedBy = "user create";
                        }
                        break;
                    case EntityState.Modified:
                        {
                            entry.Entity.ModifiedDate = DateTime.UtcNow;
                            entry.Entity.LastModifiedBy = "user modified";
                        }
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
