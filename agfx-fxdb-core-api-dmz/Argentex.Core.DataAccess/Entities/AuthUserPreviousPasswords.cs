using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class AuthUserPreviousPasswords
    {
        public int Id { get; set; }
        public int AuthUserId { get; set; }
        public string Password { get; set; }
        public DateTime DateTime { get; set; }

        public AuthUser AuthUser { get; set; }
    }
}
