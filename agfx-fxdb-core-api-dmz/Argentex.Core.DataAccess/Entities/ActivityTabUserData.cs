using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ActivityTabUserData
    {
        public int AppUserId { get; set; }
        public DateTime DataDatetime { get; set; }
        public int HourDayRangeId { get; set; }
        public string DayOfWeek { get; set; }
        public int? Calls { get; set; }
        public int? LongCalls { get; set; }
        public decimal? LongCallsCalls { get; set; }
        public int? Brochures { get; set; }

        public ActivityTabHourDayRange HourDayRange { get; set; }
    }
}
