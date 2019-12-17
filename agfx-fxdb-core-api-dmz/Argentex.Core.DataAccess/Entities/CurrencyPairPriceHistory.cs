using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class CurrencyPairPriceHistory
    {
        public string CurrencyPair { get; set; }
        public DateTime PriceDate { get; set; }
        public decimal? Price { get; set; }
        public int? UpdatedByAuthUserId { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
    }
}
