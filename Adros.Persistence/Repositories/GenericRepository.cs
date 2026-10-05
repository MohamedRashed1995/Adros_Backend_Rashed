using Adros.Persistence.Contexts;
using Adros.Shared.Interfaces;
using Adros.Shared;
using Microsoft.EntityFrameworkCore;

namespace Adros.Persistence.Repositories
{
    public class GenericRepository<T>(AppDbContext dbContext) : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _dbContext = dbContext;

        public async Task<T?> GetByIdAsync(Guid id)
            => await _dbContext.Set<T>().FindAsync(id).ConfigureAwait(false);

        public async Task<T?> GetEntityWithSpec(ISpecification<T> spec)
            => await ApplySpecification(spec).FirstOrDefaultAsync().ConfigureAwait(false);

        public async Task<IReadOnlyList<T>> ListAllAsync()
            => await _dbContext.Set<T>().AsNoTracking().ToListAsync().ConfigureAwait(false);

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
            => await ApplySpecification(spec).AsNoTracking().ToListAsync().ConfigureAwait(false);

        public async Task<int> GetCountWithSpecAsync(ISpecification<T> spec)
            => await ApplySpecification(spec).CountAsync().ConfigureAwait(false);

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity).ConfigureAwait(false);
            return entity;
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.Set<T>().Update(entity);
        }

        public async Task SoftDeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id).ConfigureAwait(false);
            if (entity != null)
            {
                entity.Deleted = true;
                Update(entity);
            }
        }
        public void Delete(T entity)
        {

            entity.Deleted = true;
            _dbContext.Set<T>().Update(entity);
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
            => SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
    }
}