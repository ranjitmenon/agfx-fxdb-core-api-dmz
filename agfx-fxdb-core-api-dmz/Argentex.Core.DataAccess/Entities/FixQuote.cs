using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class FixQuote
    {
        public string QuoteId { get; set; }
        public string TradeId { get; set; }
        public bool Cancelled { get; set; }

        public FxforwardTrade Trade { get; set; }
    }
}
