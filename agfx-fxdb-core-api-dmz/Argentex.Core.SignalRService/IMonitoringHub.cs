using System.Threading.Tasks;

namespace Argentex.Core.SignalRService
{
    public interface IMonitoringHub
    {
        Task TradingStarted(string user);
        Task CheckExecuteTrade(string modelJson);
        Task RefreshClientDetails();
    }
}