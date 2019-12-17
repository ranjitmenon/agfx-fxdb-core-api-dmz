using System.ComponentModel.DataAnnotations;

namespace Argentex.Core.Api.Models.SecurityModels
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string Grant_type { get; set; }
        //[Required]
        public string Client_id { get; set; }
        public string Client_secret { get; set; }
        public string Refresh_token { get; set; }

        public string Primary_ip { get; set; }
        public string Secondary_ip { get; set; }
        public string Scope { get; set; }
        public string Resource { get; set; }
    }
}
