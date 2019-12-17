using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ReportProcessedLog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FunctionName { get; set; }
        public string Parameters { get; set; }
        public string Result { get; set; }
        public string ResultPage { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int ReportStatusId { get; set; }
        public int AuthUserId { get; set; }
        public string ExceptionInfo { get; set; }

        public AuthUser AuthUser { get; set; }
        public ReportStatus ReportStatus { get; set; }
    }
}
