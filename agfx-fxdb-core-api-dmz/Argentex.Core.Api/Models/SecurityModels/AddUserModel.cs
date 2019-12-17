using System;
using System.ComponentModel.DataAnnotations;
using Argentex.Core.Api.Validation.Attributes;

namespace Argentex.Core.Api.Models.SecurityModels
{
    public class AddUserModel
    {
        [Display(Name = "Id")]
        public long Id { get; set; }

        [MaxLength(50, ErrorMessage = "Username has to be 50 characters or less")]
        [RequiredIf(nameof(IsApproved), true, AllowEmptyStrings = false, ErrorMessage = "A User name is required")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [RequiredIf(nameof(IsApproved), true, AllowEmptyStrings = false, ErrorMessage = "An Email address is required")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "A Forename is required")]
        [MaxLength(256, ErrorMessage = "Forename has to be 256 characters or less")]
        public string Forename { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "A Surname is required")]
        [MaxLength(256, ErrorMessage = "Surname has to be 256 characters or less")]
        public string Surname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "A Title is required")]
        [MaxLength(10, ErrorMessage = "Title has to be 10 characters or less")]
        public string Title { get; set; }

        [Required(ErrorMessage = "No client company specified")]
        public int ClientCompanyId { get; set; }

        [Required(ErrorMessage = "No Update user Id is specified")]
        public int UpdatedByAuthUserId { get; set; }

        [RequiredIf(nameof(IsApproved), true, AllowEmptyStrings = false, ErrorMessage = "A birthday is required")]
        public string Birthday { get; set; }

        [RequiredIf(nameof(IsApproved), true, AllowEmptyStrings = false, ErrorMessage = "A position is required")]
        public string Position { get; set; }

        [RequiredIf(nameof(IsApproved), true, AllowEmptyStrings = false, ErrorMessage = "A direct phone number is required")]
        public string PhoneNumberDirect { get; set; }

        [RequiredIf(nameof(IsApproved), true, AllowEmptyStrings = false, ErrorMessage = "A mobile phone number is required")]
        public string PhoneNumberMobile { get; set; }

        public string AppClientUrl { get; set; }
		
        public string PhoneNumberOther { get; set; }
        
        public string ASPNumber { get; set; }
        public DateTime? ASPCreationDate { get; set; }
        public bool PrimaryContact { get; set; }
        public int? ClientCompanyContactId { get; set; }
        public int? AuthUserId { get; set; }
		
        public string Notes { get; set; }
        public string Fullname { get; set; }
        public bool Authorized { get; set; }
        public bool RecNotification { get; set; }
        public bool RecAmReport { get; set; }
        public bool RecActivityReport { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public DateTime? LastTelephoneChangeDate { get; set; }
        public DateTime? LastEmailChangeDate { get; set; }
        public string BloombergGpi { get; set; }
        public string NiNumber { get; set; }
        public int[] AssignedCategoryIds { get; set; }
		
        public bool? IsLockedOut { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastPasswordChangeDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public DateTime? LastLockOutDate { get; set; }
        public int FailedPasswordAttemptCount { get; set; }
        public DateTime FailedPasswordAttemptWindowStart { get; set; }
        public int ApplicationId { get; set; }
		
        public bool IsAdmin { get; set; }
        public bool IsSignatory { get; set; }
        public bool IsAuthorisedSignatory { get; set; }
        public bool IsApproved { get; set; }
        public int? ApprovedByAuthUserId { get; set; }

        public bool? FindUserByUsername { get; set; }
        public bool? FindUserByEmail { get; set; }
    }
}
