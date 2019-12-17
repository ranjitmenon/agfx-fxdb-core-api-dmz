using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class AuthRole
    {
        public AuthRole()
        {
            AuthRolePermission = new HashSet<AuthRolePermission>();
            AuthUserRole = new HashSet<AuthUserRole>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public ICollection<AuthRolePermission> AuthRolePermission { get; set; }
        public ICollection<AuthUserRole> AuthUserRole { get; set; }
    }
}
