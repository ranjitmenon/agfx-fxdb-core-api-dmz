using System;

namespace Argentex.Core.DataAccess.Entities
{
    public class CurrencyPairPricing
    {
        public string CurrencyPair { get; set; }
        public double Rate { get; set; }
        public DateTime? RateTimeStamp { get; set; }
        public DateTime FeedTimeStamp { get; set; }
        public string RateCurrencyPair { get; set; }
    }
}
