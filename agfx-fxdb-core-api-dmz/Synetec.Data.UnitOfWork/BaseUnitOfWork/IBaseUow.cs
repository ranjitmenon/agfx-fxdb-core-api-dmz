using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Synetec.Data.UnitOfWork.BaseUnitOfWork
{
    public interface IBaseUow : IDisposable
    {
        int SaveContext();
        Task<int> SaveContextAsync();
        Task<int> SaveContextAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveContextAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));
        DbContext DbContext { get; }
    }
}
