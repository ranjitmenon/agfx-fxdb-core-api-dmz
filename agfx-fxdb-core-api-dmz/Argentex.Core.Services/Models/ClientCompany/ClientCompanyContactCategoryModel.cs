using System;
using System.Collections.Generic;
using System.Text;

namespace Argentex.Core.Service.Models.ClientCompany
{
    public class ClientCompanyContactCategoryModel
    {
        public int ClientCompanyContactId { get; set; }
        public int ContactCategoryId { get; set; }
        public string ContactCategoryDescription { get; set; }
    }
}
