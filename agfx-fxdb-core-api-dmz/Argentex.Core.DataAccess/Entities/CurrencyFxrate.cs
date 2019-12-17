using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class CurrencyFxrate
    {
        public int LhsCcyid { get; set; }
        public int RhsCcyid { get; set; }
        public decimal? Rate { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
