using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class LogCassRecsPaymentFile
    {
        public int LogId { get; set; }
        public string LogAction { get; set; }
        public int Id { get; set; }
        public DateTime CassRecsDate { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public string DocumentId { get; set; }
        public DateTime UploadedDateTime { get; set; }
        public int UploadedByAuthUserId { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public int UpdatedByAuthUserId { get; set; }
    }
}
