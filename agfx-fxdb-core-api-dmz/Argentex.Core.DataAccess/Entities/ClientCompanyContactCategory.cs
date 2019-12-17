using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientCompanyContactCategory
    {
        public int ClientCompanyContactId { get; set; }
        public int ContactCategoryId { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedByAuthUserId { get; set; }

        public ClientCompanyContact ClientCompanyContact { get; set; }
        public ContactCategory ContactCategory { get; set; }
        public AuthUser CreatedByAuthUser { get; set; }
    }
}
