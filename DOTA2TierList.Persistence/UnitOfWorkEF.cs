using DOTA2TierList.Application.Interfaces.DbInterfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTA2TierList.Persistence
{
    public class UnitOfWorkEF : IUnitOfWork
    {
        private readonly ApplicationContext _db;
        private IDbContextTransaction? _transaction;
        private bool _disposed;

        public UnitOfWorkEF(ApplicationContext db)
        { _db = db; }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _db.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync() 
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                _transaction.Dispose();
            }
           
            _transaction = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _transaction?.Dispose();
                }
            }

            _disposed = true;
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                _transaction.Dispose();
            }
            
            _transaction = null;
        }

    }
}
