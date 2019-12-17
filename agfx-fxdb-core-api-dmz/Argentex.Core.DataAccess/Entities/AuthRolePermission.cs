using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class AuthRolePermission
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        public AuthPermission Permission { get; set; }
        public AuthRole Role { get; set; }
    }
}
