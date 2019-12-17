using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ContactCategory
    {
        public ContactCategory()
        {
            ClientCompanyContactCategory = new HashSet<ClientCompanyContactCategory>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int? Sequence { get; set; }

        public ICollection<ClientCompanyContactCategory> ClientCompanyContactCategory { get; set; }
    }
}
