<<<<<<< HEAD
﻿using Adros.Core.Entities;
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
=======
﻿//using Adros.Persistence.Contexts;
using Adros.Persistence.Repositories;
using Adros.Persistence.UnitOfWork;
using Adros.Persistence.Configurations;
//using Adros.Persistence.Migrations;
using Adros.Shared.Interfaces;
using Adros.Shared;
using System.Collections;
using Adros.Infrastructure.Data;
using System.Reflection;
using Adros.Core.Entities.Subscription;
using Adros.Core.Entities.Users;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Adros.Persistence.Contexts;


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

>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            if (!_repositories.ContainsKey(typeName))
            {
                var repositoryInstance = new GenericRepository<T>(_dbContext);
                _repositories.Add(typeName, repositoryInstance);
            }
<<<<<<< HEAD
=======

>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
            return (IRepository<T>)_repositories[typeName]!;
        }

        public async Task<int> CompleteAsync() => await _dbContext.SaveChangesAsync();

        public async Task RollbackAsync() => await _dbContext.DisposeAsync();

        public void Dispose() => _dbContext.Dispose();
    }
}
