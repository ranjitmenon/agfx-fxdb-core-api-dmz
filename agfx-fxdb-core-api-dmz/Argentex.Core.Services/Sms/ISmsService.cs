using Argentex.Core.Service.Sms.Models;
using System;
using System.Threading.Tasks;

namespace Argentex.Core.Service
{
    public interface ISmsService : IDisposable
    {
        Task<string> Send2FAMessage(string username);
    }
}
