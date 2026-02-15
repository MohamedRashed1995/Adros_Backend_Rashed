using System;
using Microsoft.EntityFrameworkCore;
namespace Adros.Shared.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : BaseEntity;
        Task<int> CompleteAsync();
        Task RollbackAsync();
    }

}
