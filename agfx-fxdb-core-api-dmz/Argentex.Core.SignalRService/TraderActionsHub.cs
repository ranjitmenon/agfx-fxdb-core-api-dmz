using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Argentex.Core.SignalRService
{
    public class TraderActionsHub : Hub, ITraderActionsHub
    {
        private readonly IHubContext<TraderActionsHub> _context;

        public TraderActionsHub(IHubContext<TraderActionsHub> context)
        {
            _context = context;
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

        public async Task ContinueExecuteTrade(string model)
        {
            await _context.Clients.All.SendAsync("ContinueExecuteTrade", model);
        }

        public async Task ManageClientTrade(string model)
        {
            await _context.Clients.All.SendAsync("ManageClientTrade", model);
        }
    }
}
