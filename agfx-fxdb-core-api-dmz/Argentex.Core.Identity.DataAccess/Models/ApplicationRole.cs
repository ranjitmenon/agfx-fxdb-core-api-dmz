using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Argentex.Core.Identity.DataAccess
{
    public class ApplicationRole : IdentityRole<long>
    {
        //public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
