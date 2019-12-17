using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class LogPayment
    {
        public int LogId { get; set; }
        public string LogAction { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedByAuthUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public int Id { get; set; }
        public string Code { get; set; }
        public int PaymentTypeId { get; set; }
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
        public int? DebitBankAccountId { get; set; }
        public int? CreditBankAccountId { get; set; }
        public int? DebitClientCompanyVirtualAccountId { get; set; }
        public int? CreditClientCompanyVirtualAccountId { get; set; }
        public int? CreditClientCompanyOpiid { get; set; }
        public string Reference { get; set; }
        public decimal? ApplicableRate { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
        public bool? IsSwiftpayment { get; set; }
        public int? PaymentSwiftoutgoingStatusId { get; set; }
        public int? SwiftAuth1ByAuthUserId { get; set; }
        public int? SwiftAuth2ByAuthUserId { get; set; }
        public DateTime? SwiftAuth1DateTime { get; set; }
        public DateTime? SwiftAuth2DateTime { get; set; }
    }
}
