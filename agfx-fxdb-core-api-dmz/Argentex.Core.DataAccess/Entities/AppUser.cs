using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class AppUser
    {
        public AppUser()
        {
            AppUserEmailAlternative = new HashSet<AppUserEmailAlternative>();
            ClientCompanySalesAppUser = new HashSet<ClientCompanySalesAppUser>();
            Commission = new HashSet<Commission>();
        }

        public int Id { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public int AppUserTypeId { get; set; }
        public int AuthUserId { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
        public string FullName { get; set; }
        public bool IsDirector { get; set; }
        public string Ipaddress { get; set; }
        public string Extension { get; set; }
        public string Aspnumber { get; set; }
        public DateTime? AspcreationDate { get; set; }
        public string TelephoneNumber { get; set; }
        public int? TelephoneCountryCodeId { get; set; }
        public DateTime? UserStartDate { get; set; }
        public bool Is2Famember { get; set; }
        public bool IsUserManager { get; set; }
        public DateTime? LastTelephoneChangeDate { get; set; }
        public DateTime? LastEmailChangeDate { get; set; }
        public string BloombergGpi { get; set; }
        public bool OnlineTradingNotifications { get; set; }

        public AppUserType AppUserType { get; set; }
        public AuthUser AuthUser { get; set; }
        public TelephoneCountryCode TelephoneCountryCode { get; set; }
        public ICollection<AppUserEmailAlternative> AppUserEmailAlternative { get; set; }
        public ICollection<ClientCompanySalesAppUser> ClientCompanySalesAppUser { get; set; }
        public ICollection<Commission> Commission { get; set; }
    }
}
