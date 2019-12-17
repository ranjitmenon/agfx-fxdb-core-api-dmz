using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class FixTradeMessage
    {
        public int Id { get; set; }
        public string TradeCode { get; set; }
        public DateTime MessageDate { get; set; }
        public string FixMessage { get; set; }
        public bool IsReset { get; set; }
    }
}
