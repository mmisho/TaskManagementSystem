using Domain.Shared.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Xml.Schema;

namespace Infrastructure.Repositories
{
    public class BaseRepository<TContext, TAggregateRoot> : IBaseRepository<TAggregateRoot>
        where TAggregateRoot : class
        where TContext : DbContext
    {
        private readonly TContext _context;

        public BaseRepository(TContext context)
        {
            _context = context;
        }
        public virtual void Delete(TAggregateRoot aggregateRoot)
        {
            _context.Set<TAggregateRoot>().Remove(aggregateRoot);
        }

        public virtual async Task InsertAsync(TAggregateRoot aggregateRoot)
        {
          await _context.Set<TAggregateRoot>().AddAsync(aggregateRoot);
        }

        public virtual async Task<TAggregateRoot?> OfIdAsync(Guid id)
        {
            return await _context.Set<TAggregateRoot>().FindAsync(id);
        }

        public async Task<TAggregateRoot?> OfIdAsync(string id)
        {
            return await _context.Set<TAggregateRoot>().FindAsync(id);
        }

        public virtual IQueryable<TAggregateRoot> Query()
        {
           return _context.Set<TAggregateRoot>();
        }

        public virtual void Update(TAggregateRoot aggregateRoot)
        {
            _context.Set<TAggregateRoot>().Update(aggregateRoot);
        }
    }
}
