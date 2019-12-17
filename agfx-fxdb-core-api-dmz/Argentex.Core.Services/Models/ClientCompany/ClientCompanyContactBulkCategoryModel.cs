using System;
using System.Collections.Generic;
using System.Text;

namespace Argentex.Core.Service.Models.ClientCompany
{
    public class ClientCompanyContactBulkCategoryModel
    {
        public int ClientCompanyContactId { get; set; }
        public int[] ContactCategoryIds { get; set; }
        public int  CreatedByAuthUserId { get; set; }
    }
}
