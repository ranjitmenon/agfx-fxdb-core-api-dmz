using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class AuthApplication
    {
        public AuthApplication()
        {
            AuthUser = new HashSet<AuthUser>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public AuthApplication IdNavigation { get; set; }
        public AuthApplication InverseIdNavigation { get; set; }
        public ICollection<AuthUser> AuthUser { get; set; }
    }
}
