using System.Collections.Generic;

namespace Argentex.Core.Service.Models.Identity
{
    public class ApproveUsersRequest
    {
        public int ApproverAuthUserId { get; set; }
        public ICollection<int> UserIdsToApprove { get; set; }
    }
}