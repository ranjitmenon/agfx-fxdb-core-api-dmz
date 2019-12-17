using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class LogCassRecs
    {
        public int LogId { get; set; }
        public string LogAction { get; set; }
        public int Id { get; set; }
        public int? CassRecsStatementFileId { get; set; }
        public DateTime CassRecsDate { get; set; }
        public string CurrencyCode { get; set; }
        public decimal LastNightsClosingLedger { get; set; }
        public int? Check1ByAuthUserId { get; set; }
        public DateTime? Check1UpdatedDateTime { get; set; }
        public int? Check2ByAuthUserId { get; set; }
        public DateTime? Check2UpdatedDateTime { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public int? CompletedByAuthUserId { get; set; }
        public DateTime? CompletedDateTime { get; set; }
    }
}
