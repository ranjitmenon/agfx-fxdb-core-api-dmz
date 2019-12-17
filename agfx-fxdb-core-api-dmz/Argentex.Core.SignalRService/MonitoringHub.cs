using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Argentex.Core.SignalRService
{
    public class MonitoringHub : Hub, IMonitoringHub
    {
        private readonly IHubContext<MonitoringHub> _context;
        private readonly ITraderActionsHub _actionHub;

        public MonitoringHub(IHubContext<MonitoringHub> context, ITraderActionsHub actionHub)
        {
            _context = context;
            _actionHub = actionHub;
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }

        public async Task TradingStarted(string user)
        {
            await _context.Clients.All.SendAsync("TradingStarted", user);
        }

        public async Task CheckExecuteTrade(string model)
        {
            await _context.Clients.All.SendAsync("CheckExecuteTrade", model);
        }

        public async Task RefreshClientDetails()
        {
            await _context.Clients.All.SendAsync("RefreshClientDetails");
        }
    }
}
