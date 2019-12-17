using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class LastWorkingDay
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public DateTime LastWorkingDay1 { get; set; }
    }
}
