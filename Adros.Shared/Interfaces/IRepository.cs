<<<<<<< HEAD
﻿using System.Linq.Expressions;

namespace Adros.Shared.Interfaces
=======
﻿namespace Adros.Shared.Interfaces
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<T?> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<int> GetCountWithSpecAsync(ISpecification<T> spec);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T?> GetFirstOrDefaultAsync(
        Func<IQueryable<T>, IQueryable<T>>? include = null,
        Func<IQueryable<T>, IQueryable<T>>? filter = null
    );
<<<<<<< HEAD
        //Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllAsync(
        Func<IQueryable<T>, IQueryable<T>>? include = null,
        Func<IQueryable<T>, IQueryable<T>>? filter = null);
        Task<T> GetBYIdAsync(Guid? id);
        

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task<T> GetEntityWithSpecAsync(ISpecification<T> spec);
        
        Task<T?> GetAsync(
                        Expression<Func<T, bool>> predicate,
                        Func<IQueryable<T>, IQueryable<T>>? include = null
                    );

        Task<IReadOnlyList<T>> GetAllWithIncludeAsync(
    Expression<Func<T, bool>>? filter = null,
    params Expression<Func<T, object>>[] includes);



        void Delete(T entity, bool hardDelete = true);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        //void Delete(T entity, bool hardDelete = true);
        //Task SoftDeleteAsync(Guid id);
    }
}
