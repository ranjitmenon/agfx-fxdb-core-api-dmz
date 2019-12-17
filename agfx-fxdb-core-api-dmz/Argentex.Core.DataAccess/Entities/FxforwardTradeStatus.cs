using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class FxforwardTradeStatus
    {
        public FxforwardTradeStatus()
        {
            FxforwardTrade = new HashSet<FxforwardTrade>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public ICollection<FxforwardTrade> FxforwardTrade { get; set; }
    }
}
