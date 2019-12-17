using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ReportQueueToProcess
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FunctionName { get; set; }
        public string Parameters { get; set; }
        public string ResultPage { get; set; }
        public DateTime? StartDateTime { get; set; }
        public int ReportStatusId { get; set; }
        public int AuthUserId { get; set; }

        public AuthUser AuthUser { get; set; }
        public ReportStatus ReportStatus { get; set; }
    }
}
