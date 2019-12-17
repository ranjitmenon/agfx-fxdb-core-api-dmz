using Argentex.Core.DataAccess.Entities;
using System;

namespace Argentex.Core.Service
{
    public class ClientCompanyContactModel
    {
        public int ID { get; set; }
        public string ContactTitle { get; set; }
        public string ContactForename { get; set; }
        public string ContactSurname { get; set; }
        public string ContactEmail { get; set; }
        public string ContactTelephone { get; set; }
        public bool Authorized { get; set; }
        public string TelephoneMobile { get; set; }
        public string TelephoneOther { get; set; }
        public DateTime? BirthDay { get; set; }
        public string NiNumber { get; set; }
        public string BloombergGpi { get; set; }
        public bool ReceiveNotifications { get; set; }
        public bool ReceiveAMReport { get; set; }
        public bool ReceiveActivityReport { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public string Notes { get; set; }
        public string FullName { get; set; }
        public int ClientSiteAuthUserID { get; set; }
        public string Position { get; set; }
        public bool PrimaryContact { get; set; }
        public string ASPNumber { get; set; }
        public DateTime ASPExpirationDate { get; set; }
        public DateTime? ASPCreationDate { get; set; }
        public DateTime? LastTelephoneChangeDate { get; set; }
        public DateTime? LastEmailChangeDate { get; set; }
        public string UserName { get; set; }
        public bool IsApproved { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public ClientCompanyModel ClientCompany { get; set; }
    }
}
