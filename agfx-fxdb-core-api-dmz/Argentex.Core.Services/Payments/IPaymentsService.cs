using System;
using System.Threading.Tasks;

namespace Argentex.Core.Service.Payments
{
    public interface IPaymentsService : IDisposable
    {
        Task<bool> NotifyContacts(string paymentCode);
    }
}
