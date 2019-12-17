using System;

namespace Argentex.Core.Service.Models.ClientSiteAction
{
    public class CSASwapsModel
    {
        public long ActionID { get; set; }
        public int ClientCompanyID { get; set; }
        public string ClientCompanyName { get; set; }
        public string CreatedByClientName { get; set; }
        public string FXForwardTradeCode { get; set; }
        public DateTime? ValueDate { get; set; }
        public decimal? SellAmount { get; set; }
        public decimal? BuyAmount { get; set; }
        public decimal Rate { get; set; }
        public string CurrencyPair { get; set; }
        public string ActionStatus { get; set; }
        public int ActionStatusID { get; set; }
        public DateTime ActionCreatedDateTime { get; set; }
        public bool IsParentTrade { get; set; }
    }
}
