using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientCompanyTradeCount
    {
        public int ClientCompanyId { get; set; }
        public int TradeCount { get; set; }

        public ClientCompany ClientCompany { get; set; }
    }
}
