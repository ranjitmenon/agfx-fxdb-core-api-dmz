using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class AuthUserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public AuthRole Role { get; set; }
    }
}
