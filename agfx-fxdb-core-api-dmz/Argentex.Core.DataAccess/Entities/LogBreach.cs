using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class LogBreach
    {
        public int LogId { get; set; }
        public string LogAction { get; set; }
        public int Id { get; set; }
        public int BreachTypeId { get; set; }
        public int BreachLevelId { get; set; }
        public string TradeCode { get; set; }
        public int? ClientCompanyOpiid { get; set; }
        public string OriginalLimit { get; set; }
        public string OverrideValue { get; set; }
        public string Notes { get; set; }
        public int CreatedByAuthUserId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public int? PaymentId { get; set; }
    }
}
