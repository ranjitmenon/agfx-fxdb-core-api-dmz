using System;
using System.Collections.Generic;
using System.Text;

namespace Argentex.Core.UnitsOfWork.Users.Model
{
    public class ApproveUserChangeRequest
    {
        public int UserChangeRequestID { get; set; }
        public int ApprovedByAuthUserId { get; set; }

    }
}
