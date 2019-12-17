using System.ComponentModel.DataAnnotations;

namespace Argentex.Core.Api.Models.SecurityModels
{
    public class UpdateUserModel
    {
        [Display(Name = "Id")]
        public long Id { get; set; }

        [Required]
        [MaxLength(128)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [MaxLength(256)]
        public string Forename { get; set; }

        [Required]
        [MaxLength(256)]
        public string Surname { get; set; }

        [Required]
        public int ClientCompanyId { get; set; }

        [Required]
        public int UpdatedByAuthUserId { get; set; }
    }
}
