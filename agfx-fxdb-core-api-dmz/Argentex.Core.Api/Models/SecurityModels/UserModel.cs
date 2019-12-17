using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Argentex.Core.Api.Models.SecurityModels
{
    public class UserModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
