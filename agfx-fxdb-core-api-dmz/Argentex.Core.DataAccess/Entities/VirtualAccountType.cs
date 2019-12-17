using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class VirtualAccountType
    {
        public VirtualAccountType()
        {
            ClientCompanyVirtualAccount = new HashSet<ClientCompanyVirtualAccount>();
            VirtualAccountTypeBankAccount = new HashSet<VirtualAccountTypeBankAccount>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public bool? IsPaymentAllowed { get; set; }

        public ICollection<ClientCompanyVirtualAccount> ClientCompanyVirtualAccount { get; set; }
        public ICollection<VirtualAccountTypeBankAccount> VirtualAccountTypeBankAccount { get; set; }
    }
}
