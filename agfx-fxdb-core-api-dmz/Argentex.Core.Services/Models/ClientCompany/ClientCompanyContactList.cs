namespace Argentex.Core.Service.Models.ClientCompany
{
    public class ClientCompanyContactList
    {
        public int ID { get; set; }
        public string ContactTitle { get; set; }
        public string ContactForename { get; set; }
        public string ContactSurname { get; set; }
        public string ContactEmail { get; set; }
        public string FullName { get; set; }
        public bool Authorized { get; set; }
        public string Position { get; set; }
        public bool PrimaryContact { get; set; }
        public int ClientCompanyId { get; set; }
    }
}
