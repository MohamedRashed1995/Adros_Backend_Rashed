using Adros.Shared;
using Adros.Shared.Interfaces;
using System.Linq.Expressions;

namespace Adros.Application.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
<<<<<<< HEAD
        //Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllAsync(
        Func<IQueryable<T>, IQueryable<T>>? include = null,
        Func<IQueryable<T>, IQueryable<T>>? filter = null
    );
        Task<T> GetBYIdAsync(Guid? id);
        //IQueryable<T> GetAll();
=======
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetBYIdAsync(Guid? id);
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task<T> GetEntityWithSpecAsync(ISpecification<T> spec);
        Task<int> GetCountWithSpecAsync(ISpecification<T> spec);
<<<<<<< HEAD
        Task<T?> GetAsync(
                        Expression<Func<T, bool>> predicate,
                        Func<IQueryable<T>, IQueryable<T>>? include = null
                    );
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a

        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(T entity, bool hardDelete = true);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetFirstOrDefaultAsync(
        Func<IQueryable<T>, IQueryable<T>>? include = null,
        Func<IQueryable<T>, IQueryable<T>>? filter = null
<<<<<<< HEAD

    );
    }
}

=======
    );
    }
}
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
