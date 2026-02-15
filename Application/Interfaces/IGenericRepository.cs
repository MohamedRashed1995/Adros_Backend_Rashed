using Adros.Shared;
using Adros.Shared.Interfaces;
using System.Linq.Expressions;

namespace Adros.Application.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        //Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllAsync(
        Func<IQueryable<T>, IQueryable<T>>? include = null,
        Func<IQueryable<T>, IQueryable<T>>? filter = null
    );
        Task<T> GetBYIdAsync(Guid? id);
        //IQueryable<T> GetAll();

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task<T> GetEntityWithSpecAsync(ISpecification<T> spec);
        Task<int> GetCountWithSpecAsync(ISpecification<T> spec);
        Task<T?> GetAsync(
                        Expression<Func<T, bool>> predicate,
                        Func<IQueryable<T>, IQueryable<T>>? include = null
                    );

        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(T entity, bool hardDelete = true);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetFirstOrDefaultAsync(
        Func<IQueryable<T>, IQueryable<T>>? include = null,
        Func<IQueryable<T>, IQueryable<T>>? filter = null

    );
    }
}

