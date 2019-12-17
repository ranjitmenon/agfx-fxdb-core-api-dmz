using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class LogFxforwardTradeCcmlimitOverride
    {
        public long LogId { get; set; }
        public string LogAction { get; set; }
        public int Id { get; set; }
        public string TradeCode { get; set; }
        public string LimitName { get; set; }
        public string OriginalLimit { get; set; }
        public string OverrideValue { get; set; }
        public int OverrideByAppUserId { get; set; }
        public DateTime OverrideDateTime { get; set; }
        public bool Closed { get; set; }
        public int? ClosedByAppUserId { get; set; }
        public string ClosedNotes { get; set; }
        public DateTime? ClosedDateTime { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public int? UpdatedByAuthUserId { get; set; }
    }
}
