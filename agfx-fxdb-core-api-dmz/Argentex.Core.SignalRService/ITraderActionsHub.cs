using System.Threading.Tasks;

namespace Argentex.Core.SignalRService
{
    public interface ITraderActionsHub
    {
        Task ContinueExecuteTrade(string model);
        Task ManageClientTrade(string model);
    }
}