using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class AppUserNotification
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public int ClientCompanyId { get; set; }
        public bool TradeNotifications { get; set; }
        public bool InwardPaymentNotifications { get; set; }
        public bool OutwardPaymentNotifications { get; set; }
        public bool SettlementRequests { get; set; }
    }
}
