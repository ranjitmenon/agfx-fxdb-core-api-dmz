namespace Argentex.Core.Api.Models.ClientCompanies
{
    public class ClientCompanyAccountDto
    {
        public int ClientCompanyOpiId { get; set; }
        public int ClientCompanyId { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string Currency { get; set; }
    }
}
