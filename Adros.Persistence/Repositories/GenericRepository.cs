<<<<<<< HEAD
﻿using Adros.Persistence.Contexts;
using Adros.Shared;
using Adros.Shared.Interfaces;
=======
﻿////using Adros.Persistence.Contexts;
//using Adros.Shared.Interfaces;
//using Adros.Shared;
//using Microsoft.EntityFrameworkCore;
//using Adros.Infrastructure.Data;
//using Adros.Persistence.Contexts;

//namespace Adros.Persistence.Repositories
//{
//    public class GenericRepository<T>(AppDbContext dbContext) : IRepository<T> where T : BaseEntity
//    {
//        protected readonly AppDbContext _dbContext = dbContext;

//        public async Task<T?> GetByIdAsync(Guid id)
//            => await _dbContext.Set<T>().FindAsync(id);
//        public async Task<T?> GetFirstOrDefaultAsync(
//            Func<IQueryable<T>, IQueryable<T>>? include = null,
//            Func<IQueryable<T>, IQueryable<T>>? filter = null)
//        {
//            IQueryable<T> query = _dbContext.Set<T>();

//            if (include != null)
//                query = include(query);

//            if (filter != null)
//                query = filter(query);

//            return await query.FirstOrDefaultAsync();
//        }

//        public async Task<T?> GetEntityWithSpec(ISpecification<T> spec)
//            => await ApplySpecification(spec).FirstOrDefaultAsync();

//        public async Task<IReadOnlyList<T>> ListAllAsync()
//            => await _dbContext.Set<T>().AsNoTracking().ToListAsync();

//        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
//            => await ApplySpecification(spec).AsNoTracking().ToListAsync();

//        public async Task<int> GetCountWithSpecAsync(ISpecification<T> spec)
//            => await ApplySpecification(spec).CountAsync();

//        public async Task<T> AddAsync(T entity)
//        {
//            await _dbContext.Set<T>().AddAsync(entity);
//            return entity;
//        }

//        public void Update(T entity)
//        {
//            _dbContext.Entry(entity).State = EntityState.Modified;
//            _dbContext.Set<T>().Update(entity);
//        }
//        public void Delete(T entity)
//        {
//            //_dbContext.Entry(entity).State = EntityState.Modified;
//            _dbContext.Set<T>().Remove(entity);
//        }

//        //public async Task SoftDeleteAsync(Guid id)
//        //{
//        //    var entity = await GetByIdAsync(id).ConfigureAwait(false);
//        //    if (entity != null)
//        //    {
//        //        entity.Deleted = true;
//        //        _dbContext.Update(entity);
//        //    }
//        //}
//        //public void Delete(T entity, bool hardDelete = true)
//        //{
//        //    if (hardDelete)
//        //    {
//        //        _dbContext.Set<T>().Remove(entity);
//        //    }
//        //    else
//        //    {
//        //        entity.Deleted = true;
//        //        _dbContext.Set<T>().Update(entity);
//        //    }
//        //}

//        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
//            => SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
//    }
//}


using Adros.Persistence.Contexts;
using Adros.Shared.Interfaces;
using Adros.Shared;
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
<<<<<<< HEAD
using System.Linq.Expressions;
=======
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
using System.Threading.Tasks;

namespace Adros.Persistence.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _dbContext;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

<<<<<<< HEAD
        // ===== Get by Id =====
        public async Task<T?> GetByIdAsync(Guid id)
            => await _dbContext.Set<T>().FindAsync(id);

        public async Task<T> GetBYIdAsync(Guid? id)
            => await _dbContext.Set<T>().FindAsync(id);

        // ===== Get all =====
        //public async Task<IReadOnlyList<T>> GetAllAsync()
        //    => await _dbContext.Set<T>().AsNoTracking().ToListAsync();

        public async Task<IReadOnlyList<T>> GetAllAsync(
    Func<IQueryable<T>, IQueryable<T>>? include = null,
    Func<IQueryable<T>, IQueryable<T>>? filter = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (include != null)
                query = include(query);

            if (filter != null)
                query = filter(query);

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
            => await _dbContext.Set<T>().AsNoTracking().ToListAsync();

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
            => await ApplySpecification(spec).AsNoTracking().ToListAsync();

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
            => await ApplySpecification(spec).AsNoTracking().ToListAsync();

        public async Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec)
            => await ApplySpecification(spec).FirstOrDefaultAsync();

        public async Task<T?> GetEntityWithSpec(ISpecification<T> spec)
           => await ApplySpecification(spec).FirstOrDefaultAsync();

        public async Task<int> GetCountWithSpecAsync(ISpecification<T> spec)
            => await ApplySpecification(spec).CountAsync();

        // ===== Add / Update / Delete =====
        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }
        public async Task<IReadOnlyList<T>> GetAllWithIncludeAsync(
    Expression<Func<T, bool>>? filter = null,
    params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (filter != null)
                query = query.Where(filter);

            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            return await query.ToListAsync();
        }

        public void Delete(T entity, bool hardDelete = true)
        {
            if (hardDelete)
                _dbContext.Set<T>().Remove(entity);
            else
                entity.Deleted = true;
        }

        // ===== Count with predicate =====
        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
            => await _dbContext.Set<T>().CountAsync(predicate);

        // ===== Get first or default =====
=======
        public async Task<T?> GetByIdAsync(Guid id)
            => await _dbContext.Set<T>().FindAsync(id);

>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        public async Task<T?> GetFirstOrDefaultAsync(
            Func<IQueryable<T>, IQueryable<T>>? include = null,
            Func<IQueryable<T>, IQueryable<T>>? filter = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (include != null)
                query = include(query);

            if (filter != null)
                query = filter(query);

            return await query.FirstOrDefaultAsync();
        }

<<<<<<< HEAD
        // ===== Get with predicate + include =====
        public async Task<T?> GetAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (include != null)
                query = include(query);

            return await query.FirstOrDefaultAsync(predicate);
        }
        // في IRepository<Video> أو GenericRepository<Video>

        


        // ===== Helper to apply specification =====
=======
        public async Task<T?> GetEntityWithSpec(ISpecification<T> spec)
            => await ApplySpecification(spec).FirstOrDefaultAsync();

        public async Task<IReadOnlyList<T>> ListAllAsync()
            => await _dbContext.Set<T>().AsNoTracking().ToListAsync();

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
            => await ApplySpecification(spec).AsNoTracking().ToListAsync();

        public async Task<int> GetCountWithSpecAsync(ISpecification<T> spec)
            => await ApplySpecification(spec).CountAsync();

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
            => SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
    }
}
