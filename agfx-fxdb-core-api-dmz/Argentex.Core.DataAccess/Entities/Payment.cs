using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class Payment
    {
        public Payment()
        {
            BankAccountTransaction = new HashSet<BankAccountTransaction>();
            Breach = new HashSet<Breach>();
            ClientCompanyOpitransaction = new HashSet<ClientCompanyOpitransaction>();
            SwiftincomingFileStatement = new HashSet<SwiftincomingFileStatement>();
            Swiftmessage = new HashSet<Swiftmessage>();
            VirtualAccountTransaction = new HashSet<VirtualAccountTransaction>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public int PaymentTypeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByAuthUserId { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public bool Authorised { get; set; }
        public int? AuthorisedByAuthUserId { get; set; }
        public DateTime? AuthorisedDateTime { get; set; }
        public string FxforwardTradeCode { get; set; }
        public int? ClientCompanyId { get; set; }
        public int CurrencyId { get; set; }
        public DateTime ValueDate { get; set; }
        public decimal? Amount { get; set; }
        public string Reason { get; set; }
        public string Comments { get; set; }
        public bool? NotifyClient { get; set; }
        public bool Applied { get; set; }
        public DateTime? AppliedDateTime { get; set; }
        public int? PaymentRecReasonId { get; set; }
        public int? TransactionCommitId { get; set; }
        public bool IsDeleted { get; set; }
        public string Reference { get; set; }
        public decimal? ApplicableRate { get; set; }
        public bool? IsSwiftpayment { get; set; }
        public int PaymentSwiftoutgoingStatusId { get; set; }
        public int? SwiftAuth1ByAuthUserId { get; set; }
        public int? SwiftAuth2ByAuthUserId { get; set; }
        public DateTime? SwiftAuth1DateTime { get; set; }
        public DateTime? SwiftAuth2DateTime { get; set; }
        public bool? IsDebitedForMfidaccounts { get; set; }

        public AuthUser AuthorisedByAuthUser { get; set; }
        public ClientCompany ClientCompany { get; set; }
        public AuthUser CreatedByAuthUser { get; set; }
        public Currency Currency { get; set; }
        public FxforwardTrade FxforwardTradeCodeNavigation { get; set; }
        public PaymentRecReason PaymentRecReason { get; set; }
        public PaymentSwiftoutgoingStatus PaymentSwiftoutgoingStatus { get; set; }
        public PaymentType PaymentType { get; set; }
        public AuthUser SwiftAuth1ByAuthUser { get; set; }
        public AuthUser SwiftAuth2ByAuthUser { get; set; }
        public TransactionCommit TransactionCommit { get; set; }
        public AuthUser UpdatedByAuthUser { get; set; }
        public ICollection<BankAccountTransaction> BankAccountTransaction { get; set; }
        public ICollection<Breach> Breach { get; set; }
        public ICollection<ClientCompanyOpitransaction> ClientCompanyOpitransaction { get; set; }
        public ICollection<SwiftincomingFileStatement> SwiftincomingFileStatement { get; set; }
        public ICollection<Swiftmessage> Swiftmessage { get; set; }
        public ICollection<VirtualAccountTransaction> VirtualAccountTransaction { get; set; }
    }
}
