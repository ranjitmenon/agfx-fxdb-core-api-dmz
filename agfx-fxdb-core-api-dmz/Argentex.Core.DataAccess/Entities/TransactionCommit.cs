using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class TransactionCommit
    {
        public TransactionCommit()
        {
            BankAccountCurrencyBalance = new HashSet<BankAccountCurrencyBalance>();
            BankAccountCurrencyBalanceHistory = new HashSet<BankAccountCurrencyBalanceHistory>();
            ClientCompanyVirtualAccountCurrencyBalanceHistory = new HashSet<ClientCompanyVirtualAccountCurrencyBalanceHistory>();
            FxforwardTrade = new HashSet<FxforwardTrade>();
            Fxoption = new HashSet<Fxoption>();
            Payment = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public DateTime CommitDateTime { get; set; }
        public int AuthUserId { get; set; }

        public AuthUser AuthUser { get; set; }
        public ICollection<BankAccountCurrencyBalance> BankAccountCurrencyBalance { get; set; }
        public ICollection<BankAccountCurrencyBalanceHistory> BankAccountCurrencyBalanceHistory { get; set; }
        public ICollection<ClientCompanyVirtualAccountCurrencyBalanceHistory> ClientCompanyVirtualAccountCurrencyBalanceHistory { get; set; }
        public ICollection<FxforwardTrade> FxforwardTrade { get; set; }
        public ICollection<Fxoption> Fxoption { get; set; }
        public ICollection<Payment> Payment { get; set; }
    }
}
