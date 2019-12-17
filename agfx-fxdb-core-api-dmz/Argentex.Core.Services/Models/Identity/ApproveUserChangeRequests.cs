using System;
using System.Collections.Generic;
using System.Text;

namespace Argentex.Core.Service.Models.Identity
{
    public class ApproveUserChangeRequests
    {
        public int UserChangeRequestID { get; set; }
        public int ApprovedByAuthUserId { get; set; }
        public string AuthApplicationName { get; set; }
    }
}
