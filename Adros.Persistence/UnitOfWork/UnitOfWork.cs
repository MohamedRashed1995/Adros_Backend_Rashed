using Adros.Core.Entities;
using Adros.Persistence.Contexts;
using Adros.Persistence.Repositories;
using Adros.Shared;
using Adros.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Adros.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private Hashtable? _repositories;

        // DbContext property عامة
        public AppDbContext DbContext => _dbContext;

        // constructor
        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<T> Repository<T>() where T : BaseEntity
        {
            _repositories ??= new Hashtable();
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
