namespace Argentex.Core.Service.Models.Identity
{
    public class TokenModel
    {
        public string Token_type { get; set; }
        public string Access_token { get; set; }
        public int Expires_in { get; set; }
        public string Refresh_token { get; set; }
        public string Id_token { get; set; }
        public string Validation_code { get; set; }
    }
}
