using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class Fxswap
    {
        public Fxswap()
        {
            ClientSiteAction2Fxswap = new HashSet<ClientSiteAction2Fxswap>();
        }

        public int Id { get; set; }
        public int CreatedAuthUserId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
        public string ParentTradeCode { get; set; }
        public string DeliveryLegTradeCode { get; set; }
        public string ReversalLegTradeCode { get; set; }

        public AuthUser CreatedAuthUser { get; set; }
        public FxforwardTrade DeliveryLegTradeCodeNavigation { get; set; }
        public FxforwardTrade ParentTradeCodeNavigation { get; set; }
        public FxforwardTrade ReversalLegTradeCodeNavigation { get; set; }
        public ICollection<ClientSiteAction2Fxswap> ClientSiteAction2Fxswap { get; set; }
    }
}
