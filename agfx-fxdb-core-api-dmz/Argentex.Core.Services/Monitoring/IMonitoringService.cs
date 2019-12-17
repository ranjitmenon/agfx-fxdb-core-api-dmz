using System;
using System.Threading.Tasks;
using Argentex.Core.Service.Models.Trades;

namespace Argentex.Core.Service.Monitoring
{
    public interface IMonitoringService : IDisposable
    {
        Task<bool> NotifyTradeStarted(int authUserId);
        Task<bool> CheckExecuteTrade(TradeNotificationModel model);
        Task RefreshClientDetails();
    }
}
