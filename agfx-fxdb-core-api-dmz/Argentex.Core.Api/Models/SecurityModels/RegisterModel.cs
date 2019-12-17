using System.ComponentModel.DataAnnotations;

namespace Argentex.Core.Api.Models.SecurityModels
{
    public class RegisterModel
    {
        [MaxLength(128)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [MaxLength(256)]
        public string Forename { get; set; }

        [Required]
        [MaxLength(256)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(10)]
        public string Title { get; set; }

        [Required]
        public int ClientCompanyId { get; set; }

        [Required]
        public int UpdatedByAuthUserId { get; set; }
    }
}
