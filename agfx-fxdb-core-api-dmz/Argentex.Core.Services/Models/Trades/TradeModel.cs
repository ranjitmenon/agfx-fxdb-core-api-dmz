using System;

namespace Argentex.Core.Service.Models.Trade
{
    public class TradeModel
    {
        public string TradeId { get; set; }
        public DateTime? ContractDate { get; set; }
        public DateTime? ValueDate { get; set; }
        public decimal? ClientRate { get; set; }
        public decimal Balance { get; set; }
        public string SellCcy { get; set; }
        public string BuyCcy { get; set; }
        public decimal? ClientBuyAmount { get; set; }
        public decimal? ClientSellAmount { get; set; }
        public bool IsFullPayment { get; set; }
        public string Reference { get; set; }
        public string Status { get; set; }
        public bool PayToDefaultOPI { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ValidityDate { get; set; }
        public string MajorCcy { get; set; }
    }
}
