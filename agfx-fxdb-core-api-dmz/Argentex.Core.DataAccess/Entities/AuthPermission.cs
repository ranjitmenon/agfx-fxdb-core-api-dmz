using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class AuthPermission
    {
        public AuthPermission()
        {
            AuthRolePermission = new HashSet<AuthRolePermission>();
            NavMenuItem = new HashSet<NavMenuItem>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public ICollection<AuthRolePermission> AuthRolePermission { get; set; }
        public ICollection<NavMenuItem> NavMenuItem { get; set; }
    }
}
