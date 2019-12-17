using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class Broker
    {
        public Broker()
        {
            FxforwardTrade = new HashSet<FxforwardTrade>();
            Fxoption = new HashSet<Fxoption>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int? MarginBankAccountId { get; set; }
        public string BrokerNoteEmailAddress { get; set; }
        public string EmirLei { get; set; }
        public int? BankAccountBrokerPaymentsInId { get; set; }
        public int? BankAccountBrokerPaymentsOutId { get; set; }
        public int? BankAccountClientPaymentsInId { get; set; }
        public int? BankAccountClientPaymentsOutId { get; set; }
        public int? BankAccountSettlePaymentsInId { get; set; }
        public int? BankAccountSettlePaymentsOutId { get; set; }

        public BankAccount BankAccountBrokerPaymentsIn { get; set; }
        public BankAccount BankAccountBrokerPaymentsOut { get; set; }
        public BankAccount BankAccountClientPaymentsIn { get; set; }
        public BankAccount BankAccountClientPaymentsOut { get; set; }
        public ICollection<FxforwardTrade> FxforwardTrade { get; set; }
        public ICollection<Fxoption> Fxoption { get; set; }
    }
}
