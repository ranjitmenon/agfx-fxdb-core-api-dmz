using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ActivityTabHourDayRange
    {
        public ActivityTabHourDayRange()
        {
            ActivityTabUserData = new HashSet<ActivityTabUserData>();
        }

        public int Id { get; set; }
        public string Range { get; set; }
        public int LowerLimit { get; set; }
        public int UpperLimit { get; set; }

        public ICollection<ActivityTabUserData> ActivityTabUserData { get; set; }
    }
}
