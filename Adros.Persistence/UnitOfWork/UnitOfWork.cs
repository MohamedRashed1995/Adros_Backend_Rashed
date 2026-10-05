using Adros.Persistence.Contexts;
using Adros.Persistence.Repositories;
using Adros.Shared.Interfaces;
using Adros.Shared;
using System.Collections;

namespace Adros.Persistence.UnitOfWork
{
    public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
    {
        private readonly AppDbContext _dbContext = dbContext;
        private Hashtable? _repositories;

        public IRepository<T> Repository<T>() where T : BaseEntity
        {
            _repositories ??= [];

            var typeName = typeof(T).Name;

            if (!_repositories.ContainsKey(typeName))
            {
                var repositoryInstance = new GenericRepository<T>(_dbContext);
                _repositories.Add(typeName, repositoryInstance);
            }

            return (IRepository<T>)_repositories[typeName]!;
        }

        public async Task<int> CompleteAsync() => await _dbContext.SaveChangesAsync();

        public async Task RollbackAsync() => await _dbContext.DisposeAsync();

        public void Dispose() => _dbContext.Dispose();
    }
}
