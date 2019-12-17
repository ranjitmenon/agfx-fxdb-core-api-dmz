using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class UserChangeRequestApproval
    {
        public int Id { get; set; }
        public int UserChangeRequestId { get; set; }
        public int ApprovedByAuthUserId { get; set; }
        public DateTime ApprovedDateTime { get; set; }
        public bool? IsActive { get; set; }

        public AuthUser ApprovedByAuthUser { get; set; }
        public UserChangeRequest UserChangeRequest { get; set; }
    }
}
