namespace Argentex.Core.UnitsOfWork.Users.Model
{
    public class UserValidationModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public int ClientCompanyId { get; set; }
        public int ClientCompanyContactId { get; set; }
        public bool? ValidateUserDetails { get; set; }
    }
}
