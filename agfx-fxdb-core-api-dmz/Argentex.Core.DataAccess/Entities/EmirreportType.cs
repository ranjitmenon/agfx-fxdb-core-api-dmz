using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class EmirreportType
    {
        public EmirreportType()
        {
            EmirreportField = new HashSet<EmirreportField>();
            EmirreportFxforwardTrade = new HashSet<EmirreportFxforwardTrade>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public ICollection<EmirreportField> EmirreportField { get; set; }
        public ICollection<EmirreportFxforwardTrade> EmirreportFxforwardTrade { get; set; }
    }
}
