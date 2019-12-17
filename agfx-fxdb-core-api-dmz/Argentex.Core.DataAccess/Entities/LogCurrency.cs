using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class LogCurrency
    {
        public long LogId { get; set; }
        public string LogAction { get; set; }
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string SwiftAmountFormat { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
        public int CreatedByAuthUserId { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
