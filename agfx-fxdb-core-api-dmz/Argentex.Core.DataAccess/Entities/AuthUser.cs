using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class AuthUser
    {
        public AuthUser()
        {
            AppUser = new HashSet<AppUser>();
            AuthUserPasswordToken = new HashSet<AuthUserPasswordToken>();
            AuthUserPreviousPasswords = new HashSet<AuthUserPreviousPasswords>();
            BankAccountCurrencyDetailsCreatedByAuthUser = new HashSet<BankAccountCurrencyDetails>();
            BankAccountCurrencyDetailsUpdatedByAuthUser = new HashSet<BankAccountCurrencyDetails>();
            BreachCreatedByAuthUser = new HashSet<Breach>();
            BreachInvoiceUpdatedByAuthUser = new HashSet<BreachInvoice>();
            BreachInvoiceUploadedByAuthUser = new HashSet<BreachInvoice>();
            BreachUpdatedByAuthUser = new HashSet<Breach>();
            CassRecsCheck1ByAuthUser = new HashSet<CassRecs>();
            CassRecsCheck2ByAuthUser = new HashSet<CassRecs>();
            CassRecsCompletedByAuthUser = new HashSet<CassRecs>();
            CassRecsPaymentFileUpdatedByAuthUser = new HashSet<CassRecsPaymentFile>();
            CassRecsPaymentFileUploadedByAuthUser = new HashSet<CassRecsPaymentFile>();
            CassRecsUpdatedByAuthUser = new HashSet<CassRecs>();
            ClientCompanyComplianceCorporateSector = new HashSet<ClientCompanyComplianceCorporateSector>();
            ClientCompanyComplianceCurrency = new HashSet<ClientCompanyComplianceCurrency>();
            ClientCompanyComplianceNote = new HashSet<ClientCompanyComplianceNote>();
            ClientCompanyContactAuthUser = new HashSet<ClientCompanyContact>();
            ClientCompanyContactCategory = new HashSet<ClientCompanyContactCategory>();
            ClientCompanyContactUpdatedByAuthUser = new HashSet<ClientCompanyContact>();
            ClientCompanyCurrencyDefaultOpi = new HashSet<ClientCompanyCurrencyDefaultOpi>();
            ClientCompanyIbrelationship = new HashSet<ClientCompanyIbrelationship>();
            ClientCompanyLinkedGroup = new HashSet<ClientCompanyLinkedGroup>();
            ClientCompanyNote = new HashSet<ClientCompanyNote>();
            ClientCompanyOpi = new HashSet<ClientCompanyOpi>();
            ClientCompanyOpiduplicateCreatedByAuthUser = new HashSet<ClientCompanyOpiduplicate>();
            ClientCompanyOpiduplicateIsOkupdatedByAuthUser = new HashSet<ClientCompanyOpiduplicate>();
            ClientCompanyPipeline = new HashSet<ClientCompanyPipeline>();
            ClientCompanySalesAppUser = new HashSet<ClientCompanySalesAppUser>();
            ClientCompanyVirtualAccountCurrencyBalanceHistory = new HashSet<ClientCompanyVirtualAccountCurrencyBalanceHistory>();
            ClientSiteActionCreatedByAuthUser = new HashSet<ClientSiteAction>();
            ClientSiteActionUpdatedByAuthUser = new HashSet<ClientSiteAction>();
            ComplianceClassificationFileUpdatedByAuthUser = new HashSet<ComplianceClassificationFile>();
            ComplianceClassificationFileUploadedByAuthUser = new HashSet<ComplianceClassificationFile>();
            ComplianceIsincurrencyValueDate = new HashSet<ComplianceIsincurrencyValueDate>();
            ComplianceQuestionnaire = new HashSet<ComplianceQuestionnaire>();
            CurrencyCreatedByAuthUser = new HashSet<Currency>();
            CurrencyUpdatedByAuthUser = new HashSet<Currency>();
            FixApatradeCapture = new HashSet<FixApatradeCapture>();
            FixFxforwardTradeOrder = new HashSet<FixFxforwardTradeOrder>();
            FxforwardTrade2Opi = new HashSet<FxforwardTrade2Opi>();
            FxforwardTradeBrokeredByAuthUser = new HashSet<FxforwardTrade>();
            FxforwardTradeCreatedByAuthUser = new HashSet<FxforwardTrade>();
            FxforwardTradeFilledByAuthUser = new HashSet<FxforwardTrade>();
            FxforwardTradeInvoice = new HashSet<FxforwardTradeInvoice>();
            FxforwardTradeOpiupdatedByAuthUser = new HashSet<FxforwardTrade>();
            FxforwardTradeUpdatedByAuthUser = new HashSet<FxforwardTrade>();
            FxforwardTradeVerifiedByAuthUser = new HashSet<FxforwardTrade>();
            FxoptionCreatedByAuthUser = new HashSet<Fxoption>();
            FxoptionUpdatedByAuthUser = new HashSet<Fxoption>();
            FxoptionVerifiedByAuthUser = new HashSet<Fxoption>();
            Fxswap = new HashSet<Fxswap>();
            PaymentAuthorisedByAuthUser = new HashSet<Payment>();
            PaymentCreatedByAuthUser = new HashSet<Payment>();
            PaymentSwiftAuth1ByAuthUser = new HashSet<Payment>();
            PaymentSwiftAuth2ByAuthUser = new HashSet<Payment>();
            PaymentUpdatedByAuthUser = new HashSet<Payment>();
            ReportProcessedLog = new HashSet<ReportProcessedLog>();
            ReportQueueToProcess = new HashSet<ReportQueueToProcess>();
            SuspiciousActivityReportCreatedByAuthUser = new HashSet<SuspiciousActivityReport>();
            SuspiciousActivityReportIssueClosedByAuthUser = new HashSet<SuspiciousActivityReport>();
            SuspiciousActivityReportUpdatedByAuthUser = new HashSet<SuspiciousActivityReport>();
            SwiftincomingMatchedAccountCreatedByAuthUser = new HashSet<SwiftincomingMatchedAccount>();
            SwiftincomingMatchedAccountUpdatedByAuthUser = new HashSet<SwiftincomingMatchedAccount>();
            SwiftintegrationService = new HashSet<SwiftintegrationService>();
            TransactionCommit = new HashSet<TransactionCommit>();
            UserAuditLogPageViews = new HashSet<UserAuditLogPageViews>();
            UserChangeRequestApproval = new HashSet<UserChangeRequestApproval>();
            UserChangeRequestAuthUser = new HashSet<UserChangeRequest>();
            UserChangeRequestChangedByAuthUser = new HashSet<UserChangeRequest>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsApproved { get; set; }
        public bool IsLockedOut { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastPasswordChangeDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public DateTime? LastLockOutDate { get; set; }
        public int FailedPasswordAttemptCount { get; set; }
        public DateTime FailedPasswordAttemptWindowStart { get; set; }
        public int ApplicationId { get; set; }

        public AuthApplication Application { get; set; }
        public ICollection<AppUser> AppUser { get; set; }
        public ICollection<AuthUserPasswordToken> AuthUserPasswordToken { get; set; }
        public ICollection<AuthUserPreviousPasswords> AuthUserPreviousPasswords { get; set; }
        public ICollection<BankAccountCurrencyDetails> BankAccountCurrencyDetailsCreatedByAuthUser { get; set; }
        public ICollection<BankAccountCurrencyDetails> BankAccountCurrencyDetailsUpdatedByAuthUser { get; set; }
        public ICollection<Breach> BreachCreatedByAuthUser { get; set; }
        public ICollection<BreachInvoice> BreachInvoiceUpdatedByAuthUser { get; set; }
        public ICollection<BreachInvoice> BreachInvoiceUploadedByAuthUser { get; set; }
        public ICollection<Breach> BreachUpdatedByAuthUser { get; set; }
        public ICollection<CassRecs> CassRecsCheck1ByAuthUser { get; set; }
        public ICollection<CassRecs> CassRecsCheck2ByAuthUser { get; set; }
        public ICollection<CassRecs> CassRecsCompletedByAuthUser { get; set; }
        public ICollection<CassRecsPaymentFile> CassRecsPaymentFileUpdatedByAuthUser { get; set; }
        public ICollection<CassRecsPaymentFile> CassRecsPaymentFileUploadedByAuthUser { get; set; }
        public ICollection<CassRecs> CassRecsUpdatedByAuthUser { get; set; }
        public ICollection<ClientCompanyComplianceCorporateSector> ClientCompanyComplianceCorporateSector { get; set; }
        public ICollection<ClientCompanyComplianceCurrency> ClientCompanyComplianceCurrency { get; set; }
        public ICollection<ClientCompanyComplianceNote> ClientCompanyComplianceNote { get; set; }
        public ICollection<ClientCompanyContact> ClientCompanyContactAuthUser { get; set; }
        public ICollection<ClientCompanyContactCategory> ClientCompanyContactCategory { get; set; }
        public ICollection<ClientCompanyContact> ClientCompanyContactUpdatedByAuthUser { get; set; }
        public ICollection<ClientCompanyCurrencyDefaultOpi> ClientCompanyCurrencyDefaultOpi { get; set; }
        public ICollection<ClientCompanyIbrelationship> ClientCompanyIbrelationship { get; set; }
        public ICollection<ClientCompanyLinkedGroup> ClientCompanyLinkedGroup { get; set; }
        public ICollection<ClientCompanyNote> ClientCompanyNote { get; set; }
        public ICollection<ClientCompanyOpi> ClientCompanyOpi { get; set; }
        public ICollection<ClientCompanyOpiduplicate> ClientCompanyOpiduplicateCreatedByAuthUser { get; set; }
        public ICollection<ClientCompanyOpiduplicate> ClientCompanyOpiduplicateIsOkupdatedByAuthUser { get; set; }
        public ICollection<ClientCompanyPipeline> ClientCompanyPipeline { get; set; }
        public ICollection<ClientCompanySalesAppUser> ClientCompanySalesAppUser { get; set; }
        public ICollection<ClientCompanyVirtualAccountCurrencyBalanceHistory> ClientCompanyVirtualAccountCurrencyBalanceHistory { get; set; }
        public ICollection<ClientSiteAction> ClientSiteActionCreatedByAuthUser { get; set; }
        public ICollection<ClientSiteAction> ClientSiteActionUpdatedByAuthUser { get; set; }
        public ICollection<ComplianceClassificationFile> ComplianceClassificationFileUpdatedByAuthUser { get; set; }
        public ICollection<ComplianceClassificationFile> ComplianceClassificationFileUploadedByAuthUser { get; set; }
        public ICollection<ComplianceIsincurrencyValueDate> ComplianceIsincurrencyValueDate { get; set; }
        public ICollection<ComplianceQuestionnaire> ComplianceQuestionnaire { get; set; }
        public ICollection<Currency> CurrencyCreatedByAuthUser { get; set; }
        public ICollection<Currency> CurrencyUpdatedByAuthUser { get; set; }
        public ICollection<FixApatradeCapture> FixApatradeCapture { get; set; }
        public ICollection<FixFxforwardTradeOrder> FixFxforwardTradeOrder { get; set; }
        public ICollection<FxforwardTrade2Opi> FxforwardTrade2Opi { get; set; }
        public ICollection<FxforwardTrade> FxforwardTradeBrokeredByAuthUser { get; set; }
        public ICollection<FxforwardTrade> FxforwardTradeCreatedByAuthUser { get; set; }
        public ICollection<FxforwardTrade> FxforwardTradeFilledByAuthUser { get; set; }
        public ICollection<FxforwardTradeInvoice> FxforwardTradeInvoice { get; set; }
        public ICollection<FxforwardTrade> FxforwardTradeOpiupdatedByAuthUser { get; set; }
        public ICollection<FxforwardTrade> FxforwardTradeUpdatedByAuthUser { get; set; }
        public ICollection<FxforwardTrade> FxforwardTradeVerifiedByAuthUser { get; set; }
        public ICollection<Fxoption> FxoptionCreatedByAuthUser { get; set; }
        public ICollection<Fxoption> FxoptionUpdatedByAuthUser { get; set; }
        public ICollection<Fxoption> FxoptionVerifiedByAuthUser { get; set; }
        public ICollection<Fxswap> Fxswap { get; set; }
        public ICollection<Payment> PaymentAuthorisedByAuthUser { get; set; }
        public ICollection<Payment> PaymentCreatedByAuthUser { get; set; }
        public ICollection<Payment> PaymentSwiftAuth1ByAuthUser { get; set; }
        public ICollection<Payment> PaymentSwiftAuth2ByAuthUser { get; set; }
        public ICollection<Payment> PaymentUpdatedByAuthUser { get; set; }
        public ICollection<ReportProcessedLog> ReportProcessedLog { get; set; }
        public ICollection<ReportQueueToProcess> ReportQueueToProcess { get; set; }
        public ICollection<SuspiciousActivityReport> SuspiciousActivityReportCreatedByAuthUser { get; set; }
        public ICollection<SuspiciousActivityReport> SuspiciousActivityReportIssueClosedByAuthUser { get; set; }
        public ICollection<SuspiciousActivityReport> SuspiciousActivityReportUpdatedByAuthUser { get; set; }
        public ICollection<SwiftincomingMatchedAccount> SwiftincomingMatchedAccountCreatedByAuthUser { get; set; }
        public ICollection<SwiftincomingMatchedAccount> SwiftincomingMatchedAccountUpdatedByAuthUser { get; set; }
        public ICollection<SwiftintegrationService> SwiftintegrationService { get; set; }
        public ICollection<TransactionCommit> TransactionCommit { get; set; }
        public ICollection<UserAuditLogPageViews> UserAuditLogPageViews { get; set; }
        public ICollection<UserChangeRequestApproval> UserChangeRequestApproval { get; set; }
        public ICollection<UserChangeRequest> UserChangeRequestAuthUser { get; set; }
        public ICollection<UserChangeRequest> UserChangeRequestChangedByAuthUser { get; set; }
    }
}
