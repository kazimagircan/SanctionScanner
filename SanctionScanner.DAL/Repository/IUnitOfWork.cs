using System;
using System.Collections.Generic;
using System.Text;

namespace SanctionScanner.DAL.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : class;
        void SaveChanges();
    }
}
