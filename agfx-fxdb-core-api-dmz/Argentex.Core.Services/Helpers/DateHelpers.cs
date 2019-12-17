using System;

namespace Argentex.Core.Service.Helpers
{
    public static class DateHelpers
    {
        public static int GetDaysDifferencreBetween(DateTime date, DateTime other)
        {
            var dateValue = date.Date;
            var dateValueOther = other.Date;

            if (dateValue == dateValueOther)
                return 0;

            if (dateValue > dateValueOther)
                return (dateValue - dateValueOther).Days;

            return (dateValueOther - dateValue).Days;
        }
    }
}
