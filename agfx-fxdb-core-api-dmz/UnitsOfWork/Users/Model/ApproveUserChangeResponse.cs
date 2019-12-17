using Argentex.Core.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Argentex.Core.UnitsOfWork.Users.Model
{
    public class ApproveUserChangeResponse
    {
        public UserChangeRequest UserChangeRequest { get; set; }
        public bool SendNotification { get; set; }
        public IdentityResult Result { get; set; }
    }
}
