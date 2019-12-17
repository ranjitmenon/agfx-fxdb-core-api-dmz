using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Identity.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Argentex.Core.UnitsOfWork.Users.Model
{
    public class ClientUserModel
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public int ClientCompanyId { get; set; }
        public int ClientCompanyContactId { get; set; }
        public int AuthUserId { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public string Position { get; set; }
        public string PhoneNumberDirect { get; set; }
        public string PhoneNumberMobile { get; set; }
        public string PhoneNumberOther { get; set; }
        public DateTime? Birthday { get; set; }
        public bool IsApproved { get; set; }
        public bool PrimaryContact { get; set; }
        public string Notes { get; set; }
        public DateTime? LastPasswordChangeDate { get; set; }
        /// <summary>
        /// Refers to LastTelephoneChangeDate in the ClientCompanyContact Table
        /// </summary>
        public DateTime? LastPhoneNumberMobileChangeDate { get; set; }
        public DateTime? LastEmailChangeDate { get; set; }
        public string ASPNumber { get; set; }
        public DateTime? ASPCreationDate { get; set; }

        public string Fullname { get; set; }
        public bool Authorized { get; set; }
        public bool RecNotification { get; set; }
        public bool RecAmReport { get; set; }
        public bool RecActivityReport { get; set; }
        public bool IsDeleted { get; set; }
        public string BloombergGpi { get; set; }
        public string NiNumber { get; set; }
        public int[] AssignedCategoryIds { get; set; }

        public bool? IsLockedOut { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public DateTime? LastLockOutDate { get; set; }
        public int FailedPasswordAttemptCount { get; set; }
        public DateTime FailedPasswordAttemptWindowStart { get; set; }
        public int ApplicationId { get; set; }

        public bool EmailConfirmed { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSignatory { get; set; }
        public bool IsAuthorisedSignatory { get; set; }
        public int? ApprovedByAuthUserId { get; set; }
    }
}
