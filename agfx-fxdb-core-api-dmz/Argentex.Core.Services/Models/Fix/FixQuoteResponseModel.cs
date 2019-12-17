using System;

namespace Argentex.Core.Service.Models.Fix
{
    public class FixQuoteResponseModel
    {
        public int QuoteIndex { get; set; }
        public string QuoteId { get; set; }
        public string QuoteReqId { get; set; }
        public decimal BrokerRate { get; set; }
        public DateTime? ExpirationDateTime { get; set; }
        public string ErrorMessage { get; set; }
        //Not received from FIX, calculated internally
        public decimal ClientRate { get; set; }
    }
}
