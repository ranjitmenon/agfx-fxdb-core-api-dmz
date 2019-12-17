using System.ComponentModel.DataAnnotations;

namespace Argentex.Core.Api.Models.SecurityModels
{
    public class ChangePasswordModel
    {
        [Required]
        public long UserId { get; set; }

        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
