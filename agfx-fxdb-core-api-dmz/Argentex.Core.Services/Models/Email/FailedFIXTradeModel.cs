using System;

namespace Argentex.Core.Service.Models.Email
{
    public class FailedFIXTradeModel
    {
        public DataAccess.Entities.ClientCompany ClientCompany { get; set; }
        public string TradeCode { get; set; }
        public string CurrencyPair { get; set; }
        public DateTime ValueDate { get; set; }
        public string SellCcy { get; set; }
        public string BuyCcy { get; set; }
        public double Rate { get; set; }
    }
}
