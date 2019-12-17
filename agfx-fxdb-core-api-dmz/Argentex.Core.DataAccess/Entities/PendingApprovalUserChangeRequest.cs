using System;
using System.Collections.Generic;
using System.Text;

namespace Argentex.Core.DataAccess.Entities
{
    public class PendingApprovalUserChangeRequest
    {
        public int UserChangeRequestID { get; set; }
        public int AuthUserID { get; set; }
        public string AuthUserName { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public string AuthApplicationDescription { get; set; }
        public string CurrentValue { get; set; }
        public string ProposedValue { get; set; }
        public string ChangeValueType { get; set; }
        public DateTime ChangeDateTime { get; set; }
        public int ChangedByAuthUserID { get; set; }
        public string ChangedByAuthUserName { get; set; }
        public string ChangeStatus { get; set; }
        public int? ApprovalsRequired { get; set; }
        public string ApprovedBy { get; set; }
    }
}
