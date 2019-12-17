using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class UserChangeRequest
    {
        public UserChangeRequest()
        {
            UserChangeRequestApproval = new HashSet<UserChangeRequestApproval>();
        }

        public int Id { get; set; }
        public int AuthUserId { get; set; }
        public string CurrentValue { get; set; }
        public string ProposedValue { get; set; }
        public string ChangeValueType { get; set; }
        public DateTime ChangeDateTime { get; set; }
        public int ChangedByAuthUserId { get; set; }
        public string ChangeStatus { get; set; }

        public AuthUser AuthUser { get; set; }
        public AuthUser ChangedByAuthUser { get; set; }
        public ICollection<UserChangeRequestApproval> UserChangeRequestApproval { get; set; }
    }
}
