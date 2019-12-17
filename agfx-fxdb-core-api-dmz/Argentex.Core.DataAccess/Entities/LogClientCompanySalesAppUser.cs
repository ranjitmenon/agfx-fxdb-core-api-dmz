using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class LogClientCompanySalesAppUser
    {
        public long LogId { get; set; }
        public string LogAction { get; set; }
        public int ClientCompanyId { get; set; }
        public int SalesPersonAppUserId { get; set; }
        public int SalesOrder { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
