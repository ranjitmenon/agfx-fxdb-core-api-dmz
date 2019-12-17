namespace Argentex.Core.Service.Models.Identity
{
    public class LoginServiceModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PrimaryIP { get; set; }
        public string SecondaryIP { get; set; }
        public string Grant_Type { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RefreshToken { get; set; }
        public string Resource { get; set; }
        public string Scope { get; set; }
    }
}
