using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Synetec.Data.UnitOfWork.BaseUnitOfWork
{
    public class BaseUow : IBaseUow
    {
        private DbContext _context;
        private bool _disposed = false;

        public BaseUow(DbContext context)
        {
            _context = context;
        }

        public DbContext Context
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException("BaseUow: database connection was disposed");
                }

                return _context; //= _context ?? new DbContext();
            }
        }

        public int SaveContext()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveContextAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public async Task<int> SaveContextAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
                return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> SaveContextAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public DbContext DbContext
        {
            get
            {
                if (_disposed || _context == null)
                {
                    throw new ObjectDisposedException("BaseUow: database connection was disposed");
                }

                return _context;// = _context ?? new DbContext();
            }
        }


        //*****************************Context disposal******************************

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
