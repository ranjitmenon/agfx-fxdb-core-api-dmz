using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class AuthUserPasswordToken
    {
        public int AuthUserId { get; set; }
        public string Token { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? ExpiryDateTime { get; set; }
        public int IsExpired { get; set; }

        public AuthUser AuthUser { get; set; }
    }
}
