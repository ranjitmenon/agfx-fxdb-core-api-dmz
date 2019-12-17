using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class BankAccount
    {
        public BankAccount()
        {
            BankAccountCurrencyBalance = new HashSet<BankAccountCurrencyBalance>();
            BankAccountCurrencyBalanceHistory = new HashSet<BankAccountCurrencyBalanceHistory>();
            BankAccountCurrencyDetails = new HashSet<BankAccountCurrencyDetails>();
            BankAccountTransaction = new HashSet<BankAccountTransaction>();
            BrokerBankAccountBrokerPaymentsIn = new HashSet<Broker>();
            BrokerBankAccountBrokerPaymentsOut = new HashSet<Broker>();
            BrokerBankAccountClientPaymentsIn = new HashSet<Broker>();
            BrokerBankAccountClientPaymentsOut = new HashSet<Broker>();
            VirtualAccountTypeBankAccount = new HashSet<VirtualAccountTypeBankAccount>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }

        public ICollection<BankAccountCurrencyBalance> BankAccountCurrencyBalance { get; set; }
        public ICollection<BankAccountCurrencyBalanceHistory> BankAccountCurrencyBalanceHistory { get; set; }
        public ICollection<BankAccountCurrencyDetails> BankAccountCurrencyDetails { get; set; }
        public ICollection<BankAccountTransaction> BankAccountTransaction { get; set; }
        public ICollection<Broker> BrokerBankAccountBrokerPaymentsIn { get; set; }
        public ICollection<Broker> BrokerBankAccountBrokerPaymentsOut { get; set; }
        public ICollection<Broker> BrokerBankAccountClientPaymentsIn { get; set; }
        public ICollection<Broker> BrokerBankAccountClientPaymentsOut { get; set; }
        public ICollection<VirtualAccountTypeBankAccount> VirtualAccountTypeBankAccount { get; set; }
    }
}
