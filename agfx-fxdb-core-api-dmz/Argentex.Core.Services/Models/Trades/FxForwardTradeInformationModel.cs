using System;
using System.Collections.Generic;
using Argentex.Core.DataAccess.Entities;

namespace Argentex.Core.Service.Models.Trades
{
    public class FxForwardTradeInformationModel
    {
        public string InstructedBy { get; set; }
        public DateTime InstructedDateTime { get; set; }
        public string Method { get; set; }
        public string TradeRef { get; set; }
        public string SellCcy { get; set; }
        public decimal SellAmount { get; set; }
        public string BuyCcy { get; set; }
        public decimal BuyAmount { get; set; }
        public double Rate { get; set; }
        public DateTime ValueDate { get; set; }
        public decimal Collateral { get; set; }
        public string CollateralCcy { get; set; }
        public string CurrencyPair { get; set; }
        public string ClientEmail { get; set; }
        public ClientCompanyOpi SettlementAccountDetails { get; set; }
        public DataAccess.Entities.ClientCompany ClientCompany { get; set; }
    }
}
