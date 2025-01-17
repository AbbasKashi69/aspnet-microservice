using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace Ordering.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRespository<T>  where T : EntityBase
    {
        private readonly OrderingContext _context;

        public RepositoryBase(OrderingContext context)
        {
            _context = context;
        }

        private DbSet<T> Entity => _context.Set<T>();

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await Entity.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await Entity.Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeString = null, bool disabaleTracking = true)
        {
            var entity = Entity.AsQueryable();

            if (disabaleTracking is true)
                entity = entity.AsNoTracking();

            if(predicate is not null)
                entity = entity.Where(predicate).AsQueryable();

            if(string.IsNullOrWhiteSpace(includeString) is false)
                entity = entity.Include(includeString);

            if (orderBy is not null)
                entity = orderBy(entity).AsQueryable();

            return await entity.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<Expression<Func<T, object>>>? includes = null, bool disabaleTracking = true)
        {
            var entity = Entity.AsQueryable();

            if (disabaleTracking is true)
                entity = entity.AsNoTracking();

            if (predicate is not null)
                entity = entity.Where(predicate).AsQueryable();

            if (includes is not null)
                entity = includes.Aggregate(entity, (current, include)=> current.Include(include));

            if (orderBy is not null)
                entity = orderBy(entity).AsQueryable();

            return await entity.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await Entity.FindAsync(id);
        }

        public async Task<T> CreateAsync(T entity)
        {
            Entity.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            Entity.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity is null)
                return;

            Entity.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
