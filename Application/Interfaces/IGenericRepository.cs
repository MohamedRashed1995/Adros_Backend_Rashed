using Adros.Shared;
using Adros.Shared.Interfaces;
using System.Linq.Expressions;

namespace Adros.Application.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetBYIdAsync(Guid? id);

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task<T> GetEntityWithSpecAsync(ISpecification<T> spec);
        Task<int> GetCountWithSpecAsync(ISpecification<T> spec);

        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    }
}
