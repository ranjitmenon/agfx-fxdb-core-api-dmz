namespace Argentex.Core.Service.Models.Identity
{
    public class RefreshTokenModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
