using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class UserAuditLogChanges
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string IpAddress { get; set; }
        public string ActionType { get; set; }
        public string Data { get; set; }
    }
}
