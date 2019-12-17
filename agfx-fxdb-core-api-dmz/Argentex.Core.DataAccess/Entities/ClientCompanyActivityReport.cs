using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientCompanyActivityReport
    {
        public int Id { get; set; }
        public int ClientCompanyId { get; set; }
        public string ClientCompanyName { get; set; }
        public DateTime? LastActivityReportSentDateTime { get; set; }
        public int? LastActivityReportSentByAppUserId { get; set; }
    }
}
