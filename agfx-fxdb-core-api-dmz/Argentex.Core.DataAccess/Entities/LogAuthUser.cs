using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class LogAuthUser
    {
        public int LogId { get; set; }
        public string LogAction { get; set; }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsApproved { get; set; }
        public bool IsLockedOut { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastPasswordChangeDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public DateTime? LastLockOutDate { get; set; }
        public int FailedPasswordAttemptCount { get; set; }
        public DateTime FailedPasswordAttemptWindowStart { get; set; }
        public int ApplicationId { get; set; }
    }
}
