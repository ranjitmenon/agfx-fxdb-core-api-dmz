using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientSiteAction2FixFxforwardTrade
    {
        public long Id { get; set; }
        public long ClientSiteActionId { get; set; }
        public string FxforwardTradeCode { get; set; }

        public ClientSiteAction ClientSiteAction { get; set; }
        public FxforwardTrade FxforwardTradeCodeNavigation { get; set; }
    }
}
