using System;


namespace Argentex.Core.Service.Models.Order
{
    public class AppUserNotificationModel
    {
        public int ID { get; set; }
        public long AppUserID { get; set; }
        public int ClientCompanyID { get; set; }
        public bool TradeNotifications { get; set; }
        public bool InwardPaymentNotifications { get; set; }
        public bool OutwardPaymentNotifications { get; set; }
        public bool SettlementRequests { get; set; }

        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ClientCompany { get; set; }
        public string AddedBy { get; set; }
        public string Position { get; set; }
        public string PhoneNumberDirect { get; set; }
        public string PhoneNumberMobile { get; set; }
        public bool IsApproved { get; set; }
        public bool IsAdmin { get; set; }
        public string AppClientUrl { get; set; }
    }
}
