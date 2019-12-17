using System;
using System.Collections.Generic;

namespace Argentex.Core.UnitsOfWork.Users.Model
{
    public class UserChangeRequestResponse
    {
        public bool InsertOrUpdateUserChangeRequest { get; set; }
        public bool SendUserChangeAlerts { get; set; }
        public string WarningMessage { get; set; }
    }
}
