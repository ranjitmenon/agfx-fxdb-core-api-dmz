using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class ClientCompanyVirtualAccount
    {
        public ClientCompanyVirtualAccount()
        {
            ClientCompanyVirtualAccountCurrencyBalance = new HashSet<ClientCompanyVirtualAccountCurrencyBalance>();
            ClientCompanyVirtualAccountCurrencyBalanceHistory = new HashSet<ClientCompanyVirtualAccountCurrencyBalanceHistory>();
            VirtualAccountTransaction = new HashSet<VirtualAccountTransaction>();
        }

        public int Id { get; set; }
        public int VirtualAccountTypeId { get; set; }
        public int ClientCompanyId { get; set; }
        public byte[] UpdateTimeStamp { get; set; }

        public ClientCompany ClientCompany { get; set; }
        public VirtualAccountType VirtualAccountType { get; set; }
        public ICollection<ClientCompanyVirtualAccountCurrencyBalance> ClientCompanyVirtualAccountCurrencyBalance { get; set; }
        public ICollection<ClientCompanyVirtualAccountCurrencyBalanceHistory> ClientCompanyVirtualAccountCurrencyBalanceHistory { get; set; }
        public ICollection<VirtualAccountTransaction> VirtualAccountTransaction { get; set; }
    }
}
