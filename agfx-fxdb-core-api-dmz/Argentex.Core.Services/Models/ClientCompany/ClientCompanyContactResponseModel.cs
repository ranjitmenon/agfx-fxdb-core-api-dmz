using System.Collections.Generic;

namespace Argentex.Core.Service.Models.ClientCompany
{
    public class ClientCompanyContactResponseModel
    {
        public IDictionary<string, string[]> ResponseMessages { get; set; }
        public bool Succeeded { get; set; }
        public ClientCompanyContactModel CompanyContactModel { get; set; }
    }
}
