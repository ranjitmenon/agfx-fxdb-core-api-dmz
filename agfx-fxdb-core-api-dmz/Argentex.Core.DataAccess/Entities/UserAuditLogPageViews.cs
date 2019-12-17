using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class UserAuditLogPageViews
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int AuthUserId { get; set; }
        public string PageViewName { get; set; }
        public string IpAddress { get; set; }

        public AuthUser AuthUser { get; set; }
    }
}
