using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ReportStatus
    {
        public ReportStatus()
        {
            ReportProcessedLog = new HashSet<ReportProcessedLog>();
            ReportQueueToProcess = new HashSet<ReportQueueToProcess>();
        }

        public int Id { get; set; }
        public string Status { get; set; }

        public ICollection<ReportProcessedLog> ReportProcessedLog { get; set; }
        public ICollection<ReportQueueToProcess> ReportQueueToProcess { get; set; }
    }
}
