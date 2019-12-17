using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class LogClientCompanyLinkedGroup
    {
        public int LogId { get; set; }
        public string LogAction { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
    }
}
