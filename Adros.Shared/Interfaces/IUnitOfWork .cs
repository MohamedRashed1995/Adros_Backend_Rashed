<<<<<<< HEAD
﻿using System;
using Microsoft.EntityFrameworkCore;
namespace Adros.Shared.Interfaces
=======
﻿namespace Adros.Shared.Interfaces
>>>>>>> bace433368d0dd0f17a3b5e1ab6c1620bd5ce99a
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : BaseEntity;
        Task<int> CompleteAsync();
        Task RollbackAsync();
    }

}
