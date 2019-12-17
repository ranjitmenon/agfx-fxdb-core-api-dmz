using System;

namespace Argentex.Core.Service.Models.Fix
{
    public class FixNewOrderResponseModel
    {
        public string TradeId { get; set; }
        public string BarclaysTradeId { get; set; }
        public string BarclaysAssignedId { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsFilled { get; set; }
        public string RejectReason { get; set; }
        public decimal Price { get; set; }
        public decimal OrderQty { get; set; }
        public decimal SecondaryQty { get; set; }
        public decimal Side { get; set; }
        public string BrokerMajorPart { get; set; }
        public string CurrencyPair { get; set; }
    }
}
