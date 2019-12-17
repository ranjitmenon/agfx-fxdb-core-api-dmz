using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientCompanyContact
    {
        public ClientCompanyContact()
        {
            FxforwardTrade = new HashSet<FxforwardTrade>();
            Fxoption = new HashSet<Fxoption>();
            ClientCompanyContactCategory = new HashSet<ClientCompanyContactCategory>();
        }

        public int Id { get; set; }
        public int ClientCompanyId { get; set; }
        public string Title { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string TelephoneDirect { get; set; }
        public string TelephoneMobile { get; set; }
        public string TelephoneOther { get; set; }
        public DateTime? Birthday { get; set; }
        public bool Authorized { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public string Notes { get; set; }
        public string Fullname { get; set; }
        public bool RecNotifications { get; set; }
        public bool RecAmreport { get; set; }
        public int? AuthUserId { get; set; }
        public string Position { get; set; }
        public bool? PrimaryContact { get; set; }
        public bool RecActivityReport { get; set; }
        public bool IsDeleted { get; set; }
        public string Aspnumber { get; set; }
        public DateTime? AspcreationDate { get; set; }
        public DateTime? LastTelephoneChangeDate { get; set; }
        public DateTime? LastEmailChangeDate { get; set; }
        public string BloombergGpi { get; set; }
        public string NiNumber { get; set; }
        public AuthUser AuthUser { get; set; }
        public ClientCompany ClientCompany { get; set; }
        public AuthUser UpdatedByAuthUser { get; set; }
        public ICollection<FxforwardTrade> FxforwardTrade { get; set; }
        public ICollection<Fxoption> Fxoption { get; set; }
        public ICollection<ClientCompanyContactCategory> ClientCompanyContactCategory { get; set; }
    }
}
