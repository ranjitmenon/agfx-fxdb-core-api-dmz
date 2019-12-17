using System.Collections.Generic;

namespace Argentex.Core.Service.Models.ClientCompany
{
    public class ClientCompanyContactListResponseModel
    {
        public IDictionary<string, string[]> ResponseMessages { get; set; }
        public bool Succeeded { get; set; }
        public IList<ClientCompanyContactList> CompanyContactListModel { get; set; }
    }
}
