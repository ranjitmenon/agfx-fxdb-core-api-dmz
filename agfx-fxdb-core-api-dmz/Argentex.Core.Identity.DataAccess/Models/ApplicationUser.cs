using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Argentex.Core.Identity.DataAccess
{
    public class ApplicationUser : IdentityUser<long>
    {
        [Key]
        [Required]
        public int AuthUserId { get; set; }
        [Required]
        [MaxLength(16)]
        public string Title { get; set; }
        [Required]
        [MaxLength(256)]
        public string Forename { get; set; }
        [Required]
        [MaxLength(100)]
        public string Surname { get; set; }
        [Required]
        public int ClientCompanyId { get; set; }
        [Required]
        public int ClientCompanyContactId { get; set; }
        [Required]
        public int UpdatedByAuthUserId { get; set; }
        [MaxLength(128)]
        public string PhoneNumberMobile { get; set; }
        [MaxLength(128)]
        public string PhoneNumberOther { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string ASPNumber { get; set; }
        public DateTime? ASPCreationDate { get; set; }
        public DateTime? LastTelephoneChange { get; set; }
        public DateTime? LastEmailChange { get; set; }
        [Required]
        public DateTime LastPasswordChange { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsApproved { get; set; }
        public int? ApprovedByAuthUserId { get; set; }
        public DateTime? Birthday { get; set; }
        public string Notes { get; set; }
        [MaxLength(50)]
        public string Position { get; set; }
        public bool? PrimaryContact { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSignatory { get; set; }
        public bool IsAuthorisedSignatory { get; set; }
        public virtual IList<UserReport> UserReports { get; set; }
        public virtual ICollection<PreviousPassword> PreviousPasswords { get; set; }
    }
}
