using System.Linq.Expressions;

namespace Domain.Shared.Repository
{
    public interface IBaseRepository<TAggregateRoot> where TAggregateRoot : class
    {
        Task<TAggregateRoot?> OfIdAsync(Guid id);
        Task<TAggregateRoot?> OfIdAsync(string id);
        void Delete(TAggregateRoot aggregateRoot);
        Task  InsertAsync(TAggregateRoot aggregateRoot);
        void Update(TAggregateRoot aggregateRoot);
        IQueryable<TAggregateRoot> Query();
    }
}
