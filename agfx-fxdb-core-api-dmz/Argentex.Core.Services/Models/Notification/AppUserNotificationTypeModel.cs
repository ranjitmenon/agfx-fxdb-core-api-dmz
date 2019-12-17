using Argentex.Core.Service.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Argentex.Core.Service.Models.Order
{
    public class AppUserNotificationTypeModel
    {
        public int ID { get; set; }
        public int AppUserID { get; set; }
        public int ClientCompanyID { get; set; }
        public int NotificationTypeID { get; set; }
    }
}
