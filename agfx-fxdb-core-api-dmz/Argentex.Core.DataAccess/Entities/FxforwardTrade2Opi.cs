using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class FxforwardTrade2Opi
    {
        public FxforwardTrade2Opi()
        {
            ClientSiteAction2FxforwardTrade2Opi = new HashSet<ClientSiteAction2FxforwardTrade2Opi>();
        }

        public long Id { get; set; }
        public string FxforwardTradeCode { get; set; }
        public int ClientCompanyOpiid { get; set; }
        public decimal Amount { get; set; }
        public DateTime TradeValueDate { get; set; }
        public string Details { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByAuthUserId { get; set; }

        public ClientCompanyOpi ClientCompanyOpi { get; set; }
        public AuthUser CreatedByAuthUser { get; set; }
        public FxforwardTrade FxforwardTradeCodeNavigation { get; set; }
        public ICollection<ClientSiteAction2FxforwardTrade2Opi> ClientSiteAction2FxforwardTrade2Opi { get; set; }
    }
}
