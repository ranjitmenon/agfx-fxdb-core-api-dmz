using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class VirtualAccountTypeBankAccount
    {
        public int VirtualAccountTypeId { get; set; }
        public int BankAccountId { get; set; }

        public BankAccount BankAccount { get; set; }
        public VirtualAccountType VirtualAccountType { get; set; }
    }
}
