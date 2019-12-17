using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Service.Models.Identity;
using System;

namespace Argentex.Core.Service.Models.Email
{
    public class BrokerTradeNoteModel
    {
        public string TradeCode { get; set; }
        // RegulatoryID
        public string SellCcy { get; set; }
        public decimal SellAmount { get; set; }
        public string BuyCcy { get; set; }
        public decimal BuyAmount { get; set; }
        public double Rate { get; set; }
        public DateTime ValueDate { get; set; }
        public DataAccess.Entities.ClientCompany ClientCompany { get; set; }
        public ClientCompanyOpi SettlementAccountDetails { get; set; }

        public Broker Broker { get; set; }
        public AuthUser DealerAuthUser { get; set; }

        public string InstructedBy { get; set; }
        public DateTime InstructedDateTime { get; set; }
        public string Method { get; set; }
        public string CurrencyPair { get; set; }
        public decimal Collateral { get; set; }
        public string CollateralCcy { get; set; }
    }
}
