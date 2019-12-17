using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class FXDB1Context : DbContext
    {
        public virtual DbSet<_2fapages> _2fapages { get; set; }
        public virtual DbSet<ActivityTabHourDayRange> ActivityTabHourDayRange { get; set; }
        public virtual DbSet<ActivityTabUserData> ActivityTabUserData { get; set; }
        public virtual DbSet<Amlrisk> Amlrisk { get; set; }
        public virtual DbSet<AppSetting> AppSetting { get; set; }
        public virtual DbSet<AppUser> AppUser { get; set; }
        public virtual DbSet<AppUserEmailAlternative> AppUserEmailAlternative { get; set; }
        public virtual DbSet<AppUserType> AppUserType { get; set; }
        public virtual DbSet<ArgentexAccount> ArgentexAccount { get; set; }
        public virtual DbSet<ArmfxForwardTradeStatusesHistory> ArmfxForwardTradeStatusesHistory { get; set; }
        public virtual DbSet<Armreport> Armreport { get; set; }
        public virtual DbSet<ArmreportField> ArmreportField { get; set; }
        public virtual DbSet<ArmreportFxforwardTrade> ArmreportFxforwardTrade { get; set; }
        public virtual DbSet<ArmreportOutgoingFile> ArmreportOutgoingFile { get; set; }
        public virtual DbSet<ArmreportOutgoingFileContent> ArmreportOutgoingFileContent { get; set; }
        public virtual DbSet<AuthApplication> AuthApplication { get; set; }
        public virtual DbSet<AuthPermission> AuthPermission { get; set; }
        public virtual DbSet<AuthRole> AuthRole { get; set; }
        public virtual DbSet<AuthRolePermission> AuthRolePermission { get; set; }
        public virtual DbSet<AuthUser> AuthUser { get; set; }
        public virtual DbSet<AuthUserPasswordToken> AuthUserPasswordToken { get; set; }
        public virtual DbSet<AuthUserPreviousPasswords> AuthUserPreviousPasswords { get; set; }
        public virtual DbSet<AuthUserRole> AuthUserRole { get; set; }
        public virtual DbSet<BankAccount> BankAccount { get; set; }
        public virtual DbSet<BankAccountCurrencyBalance> BankAccountCurrencyBalance { get; set; }
        public virtual DbSet<BankAccountCurrencyBalanceHistory> BankAccountCurrencyBalanceHistory { get; set; }
        public virtual DbSet<BankAccountCurrencyDetails> BankAccountCurrencyDetails { get; set; }
        public virtual DbSet<BankAccountTransaction> BankAccountTransaction { get; set; }
        public virtual DbSet<Breach> Breach { get; set; }
        public virtual DbSet<BreachInvoice> BreachInvoice { get; set; }
        public virtual DbSet<BreachLevel> BreachLevel { get; set; }
        public virtual DbSet<BreachType> BreachType { get; set; }
        public virtual DbSet<Broker> Broker { get; set; }
        public virtual DbSet<CassRecs> CassRecs { get; set; }
        public virtual DbSet<CassRecsPaymentFile> CassRecsPaymentFile { get; set; }
        public virtual DbSet<CassRecsStatementFile> CassRecsStatementFile { get; set; }
        public virtual DbSet<ClearingCodePrefix> ClearingCodePrefix { get; set; }
        public virtual DbSet<ClientCompany> ClientCompany { get; set; }
        public virtual DbSet<ClientCompanyActivityReport> ClientCompanyActivityReport { get; set; }
        public virtual DbSet<ClientCompanyCategory> ClientCompanyCategory { get; set; }
        public virtual DbSet<ClientCompanyCompliance> ClientCompanyCompliance { get; set; }
        public virtual DbSet<ClientCompanyComplianceCorporateSector> ClientCompanyComplianceCorporateSector { get; set; }
        public virtual DbSet<ClientCompanyComplianceCurrency> ClientCompanyComplianceCurrency { get; set; }
        public virtual DbSet<ClientCompanyComplianceNote> ClientCompanyComplianceNote { get; set; }
        public virtual DbSet<ClientCompanyContact> ClientCompanyContact { get; set; }
        public virtual DbSet<ClientCompanyContactCategory> ClientCompanyContactCategory { get; set; }
        public virtual DbSet<ClientCompanyCreditType> ClientCompanyCreditType { get; set; }
        public virtual DbSet<ClientCompanyCurrencyDefaultOpi> ClientCompanyCurrencyDefaultOpi { get; set; }
        public virtual DbSet<ClientCompanyIbrelationship> ClientCompanyIbrelationship { get; set; }
        public virtual DbSet<ClientCompanyIndustrySector> ClientCompanyIndustrySector { get; set; }
        public virtual DbSet<ClientCompanyLinkedGroup> ClientCompanyLinkedGroup { get; set; }
        public virtual DbSet<ClientCompanyNote> ClientCompanyNote { get; set; }
        public virtual DbSet<ClientCompanyOnlineDetails> ClientCompanyOnlineDetails { get; set; }
        public virtual DbSet<ClientCompanyOnlineDetailsSkew> ClientCompanyOnlineDetailsSkew { get; set; }
        public virtual DbSet<ClientCompanyOnlineSpreadAdjustment> ClientCompanyOnlineSpreadAdjustment { get; set; }
        public virtual DbSet<ClientCompanyOpi> ClientCompanyOpi { get; set; }
        public virtual DbSet<ClientCompanyOpiduplicate> ClientCompanyOpiduplicate { get; set; }
        public virtual DbSet<ClientCompanyOpitransaction> ClientCompanyOpitransaction { get; set; }
        public virtual DbSet<ClientCompanyOptionCount> ClientCompanyOptionCount { get; set; }
        public virtual DbSet<ClientCompanyPipeline> ClientCompanyPipeline { get; set; }
        public virtual DbSet<ClientCompanySalesAppUser> ClientCompanySalesAppUser { get; set; }
        public virtual DbSet<ClientCompanySalesRegion> ClientCompanySalesRegion { get; set; }
        public virtual DbSet<ClientCompanyStatus> ClientCompanyStatus { get; set; }
        public virtual DbSet<ClientCompanyTradeCount> ClientCompanyTradeCount { get; set; }
        public virtual DbSet<ClientCompanyType> ClientCompanyType { get; set; }
        public virtual DbSet<ClientCompanyVirtualAccount> ClientCompanyVirtualAccount { get; set; }
        public virtual DbSet<ClientCompanyVirtualAccountCurrencyBalance> ClientCompanyVirtualAccountCurrencyBalance { get; set; }
        public virtual DbSet<ClientCompanyVirtualAccountCurrencyBalanceHistory> ClientCompanyVirtualAccountCurrencyBalanceHistory { get; set; }
        public virtual DbSet<ClientSiteAction> ClientSiteAction { get; set; }
        public virtual DbSet<ClientSiteAction2ClientCompanyOpi> ClientSiteAction2ClientCompanyOpi { get; set; }
        public virtual DbSet<ClientSiteAction2FixFxforwardTrade> ClientSiteAction2FixFxforwardTrade { get; set; }
        public virtual DbSet<ClientSiteAction2FxforwardTrade2Opi> ClientSiteAction2FxforwardTrade2Opi { get; set; }
        public virtual DbSet<ClientSiteAction2Fxswap> ClientSiteAction2Fxswap { get; set; }
        public virtual DbSet<ClientSiteActionStatus> ClientSiteActionStatus { get; set; }
        public virtual DbSet<ClientSiteActionType> ClientSiteActionType { get; set; }
        public virtual DbSet<Commission> Commission { get; set; }
        public virtual DbSet<CommissionType> CommissionType { get; set; }
        public virtual DbSet<ComplianceClassification> ComplianceClassification { get; set; }
        public virtual DbSet<ComplianceClassificationFile> ComplianceClassificationFile { get; set; }
        public virtual DbSet<ComplianceCorporateSectorFinancial> ComplianceCorporateSectorFinancial { get; set; }
        public virtual DbSet<ComplianceCorporateSectorNonFinancial> ComplianceCorporateSectorNonFinancial { get; set; }
        public virtual DbSet<ComplianceIsincurrencyValueDate> ComplianceIsincurrencyValueDate { get; set; }
        public virtual DbSet<ComplianceNature> ComplianceNature { get; set; }
        public virtual DbSet<ComplianceQuestionnaire> ComplianceQuestionnaire { get; set; }
        public virtual DbSet<ComplianceQuestionnaireAnswer> ComplianceQuestionnaireAnswer { get; set; }
        public virtual DbSet<ComplianceQuestionnaireQuestion> ComplianceQuestionnaireQuestion { get; set; }
        public virtual DbSet<ComplianceReason> ComplianceReason { get; set; }
        public virtual DbSet<ComplianceTradeReason> ComplianceTradeReason { get; set; }
        public virtual DbSet<ContactCategory> ContactCategory { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<CountryClearingCodePrefix> CountryClearingCodePrefix { get; set; }
        public virtual DbSet<CountryGroup> CountryGroup { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<CurrencyFxrate> CurrencyFxrate { get; set; }
        public virtual DbSet<CurrencyPairPricing> CurrencyPairPricing { get; set; }
        public virtual DbSet<CurrencyPairPriceHistory> CurrencyPairPriceHistory { get; set; }
        public virtual DbSet<CurrencyPairValidation> CurrencyPairValidation { get; set; }
        public virtual DbSet<Emirreport> Emirreport { get; set; }
        public virtual DbSet<EmirreportField> EmirreportField { get; set; }
        public virtual DbSet<EmirreportFxforwardTrade> EmirreportFxforwardTrade { get; set; }
        public virtual DbSet<EmirreportIncomingFile> EmirreportIncomingFile { get; set; }
        public virtual DbSet<EmirreportIncomingFileContent> EmirreportIncomingFileContent { get; set; }
        public virtual DbSet<EmirreportOutgoingFile> EmirreportOutgoingFile { get; set; }
        public virtual DbSet<EmirreportOutgoingFileContent> EmirreportOutgoingFileContent { get; set; }
        public virtual DbSet<EmirreportResponseCode> EmirreportResponseCode { get; set; }
        public virtual DbSet<EmirreportTradeResponseError> EmirreportTradeResponseError { get; set; }
        public virtual DbSet<EmirreportType> EmirreportType { get; set; }
        public virtual DbSet<Emirstatus> Emirstatus { get; set; }
        public virtual DbSet<ExpectedFrequency> ExpectedFrequency { get; set; }
        public virtual DbSet<FixApareportField> FixApareportField { get; set; }
        public virtual DbSet<FixApatradeCapture> FixApatradeCapture { get; set; }
        public virtual DbSet<FixApatradeMessage> FixApatradeMessage { get; set; }
        public virtual DbSet<FixFxforwardTradeOrder> FixFxforwardTradeOrder { get; set; }
        public virtual DbSet<FixQuote> FixQuote { get; set; }
        public virtual DbSet<FixQuoteCancelled> FixQuoteCancelled { get; set; }
        public virtual DbSet<FixTradeMessage> FixTradeMessage { get; set; }
        public virtual DbSet<FxforwardTrade> FxforwardTrade { get; set; }
        public virtual DbSet<FxforwardTrade2Opi> FxforwardTrade2Opi { get; set; }
        public virtual DbSet<FxforwardTradeInvoice> FxforwardTradeInvoice { get; set; }
        public virtual DbSet<FxforwardTradeStatus> FxforwardTradeStatus { get; set; }
        public virtual DbSet<FxforwardTradeSwapCount> FxforwardTradeSwapCount { get; set; }
        public virtual DbSet<Fxoption> Fxoption { get; set; }
        public virtual DbSet<FxoptionOutputs> FxoptionOutputs { get; set; }
        public virtual DbSet<FxoptionOutputsTemplate> FxoptionOutputsTemplate { get; set; }
        public virtual DbSet<FxoptionSettlements> FxoptionSettlements { get; set; }
        public virtual DbSet<FxoptionSettlementsTemplate> FxoptionSettlementsTemplate { get; set; }
        public virtual DbSet<FxoptionStatus> FxoptionStatus { get; set; }
        public virtual DbSet<FxoptionType> FxoptionType { get; set; }
        public virtual DbSet<Fxswap> Fxswap { get; set; }
        public virtual DbSet<GlobalSearchScope> GlobalSearchScope { get; set; }
        public virtual DbSet<IntroducingBroker> IntroducingBroker { get; set; }
        public virtual DbSet<LastWorkingDay> LastWorkingDay { get; set; }
        public virtual DbSet<LogAuthUser> LogAuthUser { get; set; }
        public virtual DbSet<LogBankAccountCurrencyDetails> LogBankAccountCurrencyDetails { get; set; }
        public virtual DbSet<LogBreach> LogBreach { get; set; }
        public virtual DbSet<LogBreachInvoice> LogBreachInvoice { get; set; }
        public virtual DbSet<LogCassRecs> LogCassRecs { get; set; }
        public virtual DbSet<LogCassRecsPaymentFile> LogCassRecsPaymentFile { get; set; }
        public virtual DbSet<LogCassRecsStatementFile> LogCassRecsStatementFile { get; set; }
        public virtual DbSet<LogClientCompanyCompliance> LogClientCompanyCompliance { get; set; }
        public virtual DbSet<LogClientCompanyComplianceCorporateSector> LogClientCompanyComplianceCorporateSector { get; set; }
        public virtual DbSet<LogClientCompanyComplianceNote> LogClientCompanyComplianceNote { get; set; }
        public virtual DbSet<LogClientCompanyContact> LogClientCompanyContact { get; set; }
        public virtual DbSet<LogClientCompanyContactCategory> LogClientCompanyContactCategory { get; set; }
        public virtual DbSet<LogClientCompanyLinkedGroup> LogClientCompanyLinkedGroup { get; set; }
        public virtual DbSet<LogClientCompanyOnlineDetails> LogClientCompanyOnlineDetails { get; set; }
        public virtual DbSet<LogClientCompanyOnlineDetailsSkew> LogClientCompanyOnlineDetailsSkew { get; set; }
        public virtual DbSet<LogClientCompanyOpi> LogClientCompanyOpi { get; set; }
        public virtual DbSet<LogClientCompanyOpiduplicate> LogClientCompanyOpiduplicate { get; set; }
        public virtual DbSet<LogClientCompanySalesAppUser> LogClientCompanySalesAppUser { get; set; }
        public virtual DbSet<LogComplianceClassificationFile> LogComplianceClassificationFile { get; set; }
        public virtual DbSet<LogComplianceIsincurrencyValueDate> LogComplianceIsincurrencyValueDate { get; set; }
        public virtual DbSet<LogComplianceQuestionnaire> LogComplianceQuestionnaire { get; set; }
        public virtual DbSet<LogCurrency> LogCurrency { get; set; }
        public virtual DbSet<LogCurrencyPairPriceHistory> LogCurrencyPairPriceHistory { get; set; }
        public virtual DbSet<LogFxforwardTrade> LogFxforwardTrade { get; set; }
        public virtual DbSet<LogFxforwardTradeCcmlimitOverride> LogFxforwardTradeCcmlimitOverride { get; set; }
        public virtual DbSet<LogFxforwardTradeInvoice> LogFxforwardTradeInvoice { get; set; }
        public virtual DbSet<LogFxoption> LogFxoption { get; set; }
        public virtual DbSet<LogPayment> LogPayment { get; set; }
        public virtual DbSet<LogSwiftincomingFile> LogSwiftincomingFile { get; set; }
        public virtual DbSet<LogSwiftincomingFileStatement> LogSwiftincomingFileStatement { get; set; }
        public virtual DbSet<LogSwiftincomingMatchedAccount> LogSwiftincomingMatchedAccount { get; set; }
        public virtual DbSet<LogSwiftintegrationService> LogSwiftintegrationService { get; set; }
        public virtual DbSet<NavMenuItem> NavMenuItem { get; set; }
        public virtual DbSet<NavMenuSection> NavMenuSection { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<PaymentRecReason> PaymentRecReason { get; set; }
        public virtual DbSet<PaymentSwiftoutgoingStatus> PaymentSwiftoutgoingStatus { get; set; }
        public virtual DbSet<PaymentSwiftoutgoingStatusTransitions> PaymentSwiftoutgoingStatusTransitions { get; set; }
        public virtual DbSet<PaymentType> PaymentType { get; set; }
        public virtual DbSet<PipelineAction> PipelineAction { get; set; }
        public virtual DbSet<PipelineActionType> PipelineActionType { get; set; }
        public virtual DbSet<ReportProcessedLog> ReportProcessedLog { get; set; }
        public virtual DbSet<ReportQueueToProcess> ReportQueueToProcess { get; set; }
        public virtual DbSet<ReportStatus> ReportStatus { get; set; }
        public virtual DbSet<ScheduledReportDummyPluginTable> ScheduledReportDummyPluginTable { get; set; }
        public virtual DbSet<SchemaVersions> SchemaVersions { get; set; }
        public virtual DbSet<SuspiciousActivityReport> SuspiciousActivityReport { get; set; }
        public virtual DbSet<SwiftincomingFile> SwiftincomingFile { get; set; }
        public virtual DbSet<SwiftincomingFileProcessingStatus> SwiftincomingFileProcessingStatus { get; set; }
        public virtual DbSet<SwiftincomingFileStatement> SwiftincomingFileStatement { get; set; }
        public virtual DbSet<SwiftincomingFileType> SwiftincomingFileType { get; set; }
        public virtual DbSet<SwiftincomingMatchedAccount> SwiftincomingMatchedAccount { get; set; }
        public virtual DbSet<SwiftintegrationService> SwiftintegrationService { get; set; }
        public virtual DbSet<Swiftmessage> Swiftmessage { get; set; }
        public virtual DbSet<SwiftvalidationCurrencyCountry> SwiftvalidationCurrencyCountry { get; set; }
        public virtual DbSet<SwiftvalidationCurrencyMessageField> SwiftvalidationCurrencyMessageField { get; set; }
        public virtual DbSet<SwiftvalidationField> SwiftvalidationField { get; set; }
        public virtual DbSet<SwiftvalidationFieldComponent> SwiftvalidationFieldComponent { get; set; }
        public virtual DbSet<SwiftvalidationFieldFieldComponent> SwiftvalidationFieldFieldComponent { get; set; }
        public virtual DbSet<SwiftvalidationMessage> SwiftvalidationMessage { get; set; }
        public virtual DbSet<SwiftvalidationMessageField> SwiftvalidationMessageField { get; set; }
        public virtual DbSet<SwiftvalidationOption> SwiftvalidationOption { get; set; }
        public virtual DbSet<SwiftvalidationOptionField> SwiftvalidationOptionField { get; set; }
        public virtual DbSet<SystemEmailSenderAddress> SystemEmailSenderAddress { get; set; }
        public virtual DbSet<TelephoneCountryCode> TelephoneCountryCode { get; set; }
        public virtual DbSet<TradeInstructionMethod> TradeInstructionMethod { get; set; }
        public virtual DbSet<TransactionCommit> TransactionCommit { get; set; }
        public virtual DbSet<UserAuditLogChanges> UserAuditLogChanges { get; set; }
        public virtual DbSet<UserAuditLogPageViews> UserAuditLogPageViews { get; set; }
        public virtual DbSet<UserChangeRequest> UserChangeRequest { get; set; }
        public virtual DbSet<UserChangeRequestApproval> UserChangeRequestApproval { get; set; }
        public virtual DbSet<VirtualAccountTransaction> VirtualAccountTransaction { get; set; }
        public virtual DbSet<VirtualAccountType> VirtualAccountType { get; set; }
        public virtual DbSet<VirtualAccountTypeBankAccount> VirtualAccountTypeBankAccount { get; set; }
        public virtual DbSet<AppUserNotification> AppUserNotification { get; set; }

        // Unable to generate entity type for table 'dbo.LogClientCompanyIndustrySector'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.LogClientCompanyContactCategory'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.LogClientCompany'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.LogClientCompanyComplianceCurrency'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.LogClientCompanyNote'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.AuthLoginEvent'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.LogClientCompanyPipeline'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.AppUserCommission'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.PaymentOutOverride'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.ClientCompanyCommission'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.LogAppUser'. Please see the warning messages.

        public FXDB1Context(DbContextOptions<FXDB1Context> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<_2fapages>(entity =>
            {
                entity.ToTable("2FAPages");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PagePath)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<ActivityTabHourDayRange>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Range)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<ActivityTabUserData>(entity =>
            {
                entity.HasKey(e => new { e.AppUserId, e.DataDatetime, e.HourDayRangeId });

                entity.Property(e => e.AppUserId).HasColumnName("AppUserID");

                entity.Property(e => e.DataDatetime).HasColumnType("date");

                entity.Property(e => e.HourDayRangeId).HasColumnName("HourDayRangeID");

                entity.Property(e => e.DayOfWeek)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.HourDayRange)
                    .WithMany(p => p.ActivityTabUserData)
                    .HasForeignKey(d => d.HourDayRangeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityTabUserData_ActivityTabHourDayRange");
            });

            modelBuilder.Entity<Amlrisk>(entity =>
            {
                entity.ToTable("AMLRisk");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<AppSetting>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.SettingKey)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SettingValue)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppUserTypeId).HasColumnName("AppUserTypeID");

                entity.Property(e => e.AspcreationDate)
                    .HasColumnName("ASPCreationDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Aspnumber)
                    .HasColumnName("ASPNumber")
                    .HasMaxLength(9);

                entity.Property(e => e.AuthUserId).HasColumnName("AuthUserID");

                entity.Property(e => e.BloombergGpi).HasMaxLength(255);

                entity.Property(e => e.Extension).HasMaxLength(50);

                entity.Property(e => e.Forename)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(201)
                    .HasComputedColumnSql("(([Forename]+' ')+[Surname])");

                entity.Property(e => e.Ipaddress)
                    .HasColumnName("IPAddress")
                    .HasMaxLength(500);

                entity.Property(e => e.Is2Famember).HasColumnName("Is2FAMember");

                entity.Property(e => e.LastEmailChangeDate).HasColumnType("datetime");

                entity.Property(e => e.LastTelephoneChangeDate).HasColumnType("datetime");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TelephoneCountryCodeId).HasColumnName("TelephoneCountryCodeID");

                entity.Property(e => e.TelephoneNumber).HasMaxLength(20);

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.UserStartDate).HasColumnType("datetime");

                entity.HasOne(d => d.AppUserType)
                    .WithMany(p => p.AppUser)
                    .HasForeignKey(d => d.AppUserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppUser_AppUserType");

                entity.HasOne(d => d.AuthUser)
                    .WithMany(p => p.AppUser)
                    .HasForeignKey(d => d.AuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppUser_AuthUser");

                entity.HasOne(d => d.TelephoneCountryCode)
                    .WithMany(p => p.AppUser)
                    .HasForeignKey(d => d.TelephoneCountryCodeId)
                    .HasConstraintName("FK_AppUser_TelephoneCountryCode");
            });

            modelBuilder.Entity<AppUserEmailAlternative>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AlternativeEmailAddress)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.AppUserId).HasColumnName("AppUserID");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.AppUserEmailAlternative)
                    .HasForeignKey(d => d.AppUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppUserEmailAlternative_AppUser");
            });

            modelBuilder.Entity<AppUserType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientCompanySalesRegionId).HasColumnName("ClientCompanySalesRegionID");

                entity.Property(e => e.CommissionTypeId).HasColumnName("CommissionTypeID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.HomePage)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.ClientCompanySalesRegion)
                    .WithMany(p => p.AppUserType)
                    .HasForeignKey(d => d.ClientCompanySalesRegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppUserType_ClientCompanySalesRegion");

                entity.HasOne(d => d.CommissionType)
                    .WithMany(p => p.AppUserType)
                    .HasForeignKey(d => d.CommissionTypeId)
                    .HasConstraintName("FK__AppUserTy__Commi__1DD065E0");
            });

            modelBuilder.Entity<ArgentexAccount>(entity =>
            {
                entity.HasIndex(e => e.ChecksumMatchingContent)
                    .HasName("IX_ArgentexAccount_MatchingContent");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ChecksumMatchingContent).HasComputedColumnSql("(checksum([MatchingContent]))");

                entity.Property(e => e.MatchingContent)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<ArmfxForwardTradeStatusesHistory>(entity =>
            {
                entity.ToTable("ARMFxForwardTradeStatusesHistory");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ArmreportId).HasColumnName("ARMReportID");

                entity.Property(e => e.ArmstatusId).HasColumnName("ARMStatusID");

                entity.Property(e => e.ArmstatusUpdatedDateTime)
                    .HasColumnName("ARMStatusUpdatedDateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.ErrorDescription).HasMaxLength(255);

                entity.Property(e => e.FxForwardTradeCode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Armreport)
                    .WithMany(p => p.ArmfxForwardTradeStatusesHistory)
                    .HasForeignKey(d => d.ArmreportId)
                    .HasConstraintName("FK_ARMFxForwardTradeStatusesHistory_ARMReport");

                entity.HasOne(d => d.Armstatus)
                    .WithMany(p => p.ArmfxForwardTradeStatusesHistory)
                    .HasForeignKey(d => d.ArmstatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ARMFxForwardTradeStatusesHistory_ARMStatus");

                entity.HasOne(d => d.FxForwardTradeCodeNavigation)
                    .WithMany(p => p.ArmfxForwardTradeStatusesHistory)
                    .HasForeignKey(d => d.FxForwardTradeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ARMFxForwardTradeStatusesHistory_FXForwardTrade");
            });

            modelBuilder.Entity<Armreport>(entity =>
            {
                entity.ToTable("ARMReport");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ArmreportOutgoingFileId).HasColumnName("ARMReportOutgoingFileID");

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.ArmreportOutgoingFile)
                    .WithMany(p => p.Armreport)
                    .HasForeignKey(d => d.ArmreportOutgoingFileId)
                    .HasConstraintName("FK_ARMReport_ARMReportOutgoingFile");
            });

            modelBuilder.Entity<ArmreportField>(entity =>
            {
                entity.ToTable("ARMReportField");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppSettingKey).HasMaxLength(250);

                entity.Property(e => e.BrokerValue).HasMaxLength(100);

                entity.Property(e => e.ClientValue).HasMaxLength(100);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ArmreportFxforwardTrade>(entity =>
            {
                entity.ToTable("ARMReportFXForwardTrade");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ArmreportId).HasColumnName("ARMReportID");

                entity.Property(e => e.ArmstatusId).HasColumnName("ARMStatusID");

                entity.Property(e => e.FxforwardTradeCode)
                    .IsRequired()
                    .HasColumnName("FXForwardTradeCode")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ReportedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Armreport)
                    .WithMany(p => p.ArmreportFxforwardTrade)
                    .HasForeignKey(d => d.ArmreportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ARMReportFxForwardTrade_ARMReport");

                entity.HasOne(d => d.Armstatus)
                    .WithMany(p => p.ArmreportFxforwardTrade)
                    .HasForeignKey(d => d.ArmstatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ARMReportFxForwardTrade_ARMStatus");

                entity.HasOne(d => d.FxforwardTradeCodeNavigation)
                    .WithMany(p => p.ArmreportFxforwardTrade)
                    .HasForeignKey(d => d.FxforwardTradeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ARMReportFxForwardTrade_FXForwardTrade");
            });

            modelBuilder.Entity<ArmreportOutgoingFile>(entity =>
            {
                entity.ToTable("ARMReportOutgoingFile");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ArmreportOutgoingFileContentId).HasColumnName("ARMReportOutgoingFileContentID");

                entity.Property(e => e.Csvfilename)
                    .IsRequired()
                    .HasColumnName("CSVFilename")
                    .HasMaxLength(255);

                entity.Property(e => e.UploadedDateTime).HasColumnType("datetime");

                entity.Property(e => e.UploadedFilename).HasMaxLength(255);

                entity.HasOne(d => d.ArmreportOutgoingFileContent)
                    .WithMany(p => p.ArmreportOutgoingFile)
                    .HasForeignKey(d => d.ArmreportOutgoingFileContentId)
                    .HasConstraintName("FK_ARMReportOutgoingFile_ARMReportOutgoingFileContent");
            });

            modelBuilder.Entity<ArmreportOutgoingFileContent>(entity =>
            {
                entity.ToTable("ARMReportOutgoingFileContent");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FileContent).IsRequired();
            });

            modelBuilder.Entity<AuthApplication>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.InverseIdNavigation)
                    .HasForeignKey<AuthApplication>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuthApplication_AuthApplication");
            });

            modelBuilder.Entity<AuthPermission>(entity =>
            {
                entity.HasIndex(e => e.Description)
                    .HasName("IX_AuthPermission")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<AuthRole>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<AuthRolePermission>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.PermissionId });

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.PermissionId).HasColumnName("PermissionID");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.AuthRolePermission)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuthRolePermission_AuthPermission");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AuthRolePermission)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuthRolePermission_AuthRole");
            });

            modelBuilder.Entity<AuthUser>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.Comment).HasMaxLength(200);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FailedPasswordAttemptCount).HasDefaultValueSql("((0))");

                entity.Property(e => e.FailedPasswordAttemptWindowStart)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastActivityDate).HasColumnType("datetime");

                entity.Property(e => e.LastLockOutDate).HasColumnType("datetime");

                entity.Property(e => e.LastLoginDate).HasColumnType("datetime");

                entity.Property(e => e.LastPasswordChangeDate).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.AuthUser)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuthUser_AuthApplication");
            });

            modelBuilder.Entity<AuthUserPasswordToken>(entity =>
            {
                entity.HasKey(e => new { e.AuthUserId, e.Token });

                entity.Property(e => e.AuthUserId).HasColumnName("AuthUserID");

                entity.Property(e => e.Token)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExpiryDateTime).HasColumnType("datetime");

                entity.Property(e => e.IsExpired).HasComputedColumnSql("(case when [ExpiryDateTime] IS NULL then (0) when datediff(second,[ExpiryDateTime],getdate())>(0) then (1) else (0) end)");

                entity.HasOne(d => d.AuthUser)
                    .WithMany(p => p.AuthUserPasswordToken)
                    .HasForeignKey(d => d.AuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuthUserPasswordToken_AuthUser");
            });

            modelBuilder.Entity<AuthUserPreviousPasswords>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AuthUserId).HasColumnName("AuthUserID");

                entity.Property(e => e.DateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.AuthUser)
                    .WithMany(p => p.AuthUserPreviousPasswords)
                    .HasForeignKey(d => d.AuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuthUserPreviousPasswords_AuthUser");
            });

            modelBuilder.Entity<AuthUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AuthUserRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuthUserRole_AuthRole");
            });

            modelBuilder.Entity<BankAccount>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<BankAccountCurrencyBalance>(entity =>
            {
                entity.HasKey(e => new { e.BankAccountId, e.CurrencyId });

                entity.Property(e => e.BankAccountId).HasColumnName("BankAccountID");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.Balance)
                    .HasColumnType("decimal(25, 8)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.BalanceDate).HasColumnType("datetime");

                entity.Property(e => e.TransactionCommitId).HasColumnName("TransactionCommitID");

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.HasOne(d => d.BankAccount)
                    .WithMany(p => p.BankAccountCurrencyBalance)
                    .HasForeignKey(d => d.BankAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BankAccountCurrencyBalance_BankAccount");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.BankAccountCurrencyBalance)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BankAccountCurrencyBalance_Currency");

                entity.HasOne(d => d.TransactionCommit)
                    .WithMany(p => p.BankAccountCurrencyBalance)
                    .HasForeignKey(d => d.TransactionCommitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BankAccountCurrencyBalance_TransactionCommit");
            });

            modelBuilder.Entity<BankAccountCurrencyBalanceHistory>(entity =>
            {
                entity.HasKey(e => new { e.BankAccountId, e.CurrencyId, e.TransactionCommitId });

                entity.Property(e => e.BankAccountId).HasColumnName("BankAccountID");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.TransactionCommitId).HasColumnName("TransactionCommitID");

                entity.Property(e => e.Balance)
                    .HasColumnType("decimal(25, 8)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.BalanceDate).HasColumnType("datetime");

                entity.HasOne(d => d.BankAccount)
                    .WithMany(p => p.BankAccountCurrencyBalanceHistory)
                    .HasForeignKey(d => d.BankAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BankAccountCurrencyBalanceHistory_BankAccount");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.BankAccountCurrencyBalanceHistory)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BankAccountCurrencyBalanceHistory_Currency");

                entity.HasOne(d => d.TransactionCommit)
                    .WithMany(p => p.BankAccountCurrencyBalanceHistory)
                    .HasForeignKey(d => d.TransactionCommitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BankAccountCurrencyBalanceHistory_TransactionCommit");
            });

            modelBuilder.Entity<BankAccountCurrencyDetails>(entity =>
            {
                entity.HasKey(e => new { e.BankAccountId, e.CurrencyId });

                entity.Property(e => e.BankAccountId).HasColumnName("BankAccountID");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.BankAccountIban)
                    .HasColumnName("BankAccountIBAN")
                    .HasMaxLength(50);

                entity.Property(e => e.BankAccountName).HasMaxLength(100);

                entity.Property(e => e.BankAccountNumber).HasMaxLength(50);

                entity.Property(e => e.BankAccountSort).HasMaxLength(8);

                entity.Property(e => e.BankAccountSwift).HasMaxLength(11);

                entity.Property(e => e.BankAddress).HasMaxLength(400);

                entity.Property(e => e.BankName).HasMaxLength(100);

                entity.Property(e => e.BeneficiaryAddress).HasMaxLength(400);

                entity.Property(e => e.BeneficiaryName).HasMaxLength(100);

                entity.Property(e => e.ClearingCodePrefixId).HasColumnName("ClearingCodePrefixID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.UpdateDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.HasOne(d => d.BankAccount)
                    .WithMany(p => p.BankAccountCurrencyDetails)
                    .HasForeignKey(d => d.BankAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BankAccountCurrencyDetails_BankAccount");

                entity.HasOne(d => d.ClearingCodePrefix)
                    .WithMany(p => p.BankAccountCurrencyDetails)
                    .HasForeignKey(d => d.ClearingCodePrefixId)
                    .HasConstraintName("FK_BankAccountCurrencyDetails_ClearingPrefix");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.BankAccountCurrencyDetails)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BankAccountCurrencyDetails_Country");

                entity.HasOne(d => d.CreatedByAuthUser)
                    .WithMany(p => p.BankAccountCurrencyDetailsCreatedByAuthUser)
                    .HasForeignKey(d => d.CreatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BankAccountCurrencyDetails_CreatedByAuthUserId");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.BankAccountCurrencyDetails)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BankAccountCurrencyDetails_Currency");

                entity.HasOne(d => d.UpdatedByAuthUser)
                    .WithMany(p => p.BankAccountCurrencyDetailsUpdatedByAuthUser)
                    .HasForeignKey(d => d.UpdatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BankAccountCurrencyDetails_UpdatedByAuthUserId");
            });

            modelBuilder.Entity<BankAccountTransaction>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(25, 8)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.BankAccountId).HasColumnName("BankAccountID");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.FxforwardTradeCode)
                    .HasColumnName("FXForwardTradeCode")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.HasOne(d => d.BankAccount)
                    .WithMany(p => p.BankAccountTransaction)
                    .HasForeignKey(d => d.BankAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BankAccountTransaction_BankAccount");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.BankAccountTransaction)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BankAccountTransaction_Currency");

                entity.HasOne(d => d.FxforwardTradeCodeNavigation)
                    .WithMany(p => p.BankAccountTransaction)
                    .HasForeignKey(d => d.FxforwardTradeCode)
                    .HasConstraintName("FK_BankAccountTransaction_FXForwardTrade");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.BankAccountTransaction)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("FK_BankAccountTransaction_Payment");
            });

            modelBuilder.Entity<Breach>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BreachLevelId).HasColumnName("BreachLevelID");

                entity.Property(e => e.BreachTypeId).HasColumnName("BreachTypeID");

                entity.Property(e => e.ClientCompanyOpiid).HasColumnName("ClientCompanyOPIID");

                entity.Property(e => e.CreatedByAuthUserId).HasColumnName("CreatedByAuthUserID");

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Notes).HasMaxLength(500);

                entity.Property(e => e.OriginalLimit).HasMaxLength(250);

                entity.Property(e => e.OverrideValue).HasMaxLength(250);

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.TradeCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.BreachLevel)
                    .WithMany(p => p.Breach)
                    .HasForeignKey(d => d.BreachLevelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Breach_BreachLevel");

                entity.HasOne(d => d.BreachType)
                    .WithMany(p => p.Breach)
                    .HasForeignKey(d => d.BreachTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Breach_BreachType");

                entity.HasOne(d => d.ClientCompanyOpi)
                    .WithMany(p => p.Breach)
                    .HasForeignKey(d => d.ClientCompanyOpiid)
                    .HasConstraintName("FK_Breach_ClientCompanyOPI");

                entity.HasOne(d => d.CreatedByAuthUser)
                    .WithMany(p => p.BreachCreatedByAuthUser)
                    .HasForeignKey(d => d.CreatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Breach_AuthUser");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Breach)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("FK_Breach_Payment");

                entity.HasOne(d => d.TradeCodeNavigation)
                    .WithMany(p => p.Breach)
                    .HasForeignKey(d => d.TradeCode)
                    .HasConstraintName("FK_Breach_FXForwardTrade");

                entity.HasOne(d => d.UpdatedByAuthUser)
                    .WithMany(p => p.BreachUpdatedByAuthUser)
                    .HasForeignKey(d => d.UpdatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Breach_AuthUser1");
            });

            modelBuilder.Entity<BreachInvoice>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Comment).HasMaxLength(500);

                entity.Property(e => e.DocumentId)
                    .IsRequired()
                    .HasColumnName("DocumentID")
                    .HasMaxLength(100);

                entity.Property(e => e.FileName).HasMaxLength(250);

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UploadedByAuthUserId).HasColumnName("UploadedByAuthUserID");

                entity.Property(e => e.UploadedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Breach)
                    .WithMany(p => p.BreachInvoice)
                    .HasForeignKey(d => d.BreachId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BreachInvoice_Breach");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.InverseIdNavigation)
                    .HasForeignKey<BreachInvoice>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BreachInvoice_BreachInvoice");

                entity.HasOne(d => d.UpdatedByAuthUser)
                    .WithMany(p => p.BreachInvoiceUpdatedByAuthUser)
                    .HasForeignKey(d => d.UpdatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BreachInvoice_AuthUser");

                entity.HasOne(d => d.UploadedByAuthUser)
                    .WithMany(p => p.BreachInvoiceUploadedByAuthUser)
                    .HasForeignKey(d => d.UploadedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BreachInvoiceUploadedBy_AuthUser");
            });

            modelBuilder.Entity<BreachLevel>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<BreachType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DefaultBreachLevelId).HasColumnName("DefaultBreachLevelID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.DefaultBreachLevel)
                    .WithMany(p => p.BreachType)
                    .HasForeignKey(d => d.DefaultBreachLevelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BreachType_BreachLevel");
            });

            modelBuilder.Entity<Broker>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BankAccountBrokerPaymentsInId).HasColumnName("BankAccountBrokerPaymentsInID");

                entity.Property(e => e.BankAccountBrokerPaymentsOutId).HasColumnName("BankAccountBrokerPaymentsOutID");

                entity.Property(e => e.BankAccountClientPaymentsInId).HasColumnName("BankAccountClientPaymentsInID");

                entity.Property(e => e.BankAccountClientPaymentsOutId).HasColumnName("BankAccountClientPaymentsOutID");

                entity.Property(e => e.BankAccountSettlePaymentsInId).HasColumnName("BankAccountSettlePaymentsInID");

                entity.Property(e => e.BankAccountSettlePaymentsOutId).HasColumnName("BankAccountSettlePaymentsOutID");

                entity.Property(e => e.BrokerNoteEmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EmirLei)
                    .HasColumnName("EMIR_LEI")
                    .HasMaxLength(50);

                entity.Property(e => e.MarginBankAccountId).HasColumnName("MarginBankAccountID");

                entity.HasOne(d => d.BankAccountBrokerPaymentsIn)
                    .WithMany(p => p.BrokerBankAccountBrokerPaymentsIn)
                    .HasForeignKey(d => d.BankAccountBrokerPaymentsInId)
                    .HasConstraintName("FK_Broker_BankAccountBrokerPaymentsIn");

                entity.HasOne(d => d.BankAccountBrokerPaymentsOut)
                    .WithMany(p => p.BrokerBankAccountBrokerPaymentsOut)
                    .HasForeignKey(d => d.BankAccountBrokerPaymentsOutId)
                    .HasConstraintName("FK_Broker_BankAccountBrokerPaymentsOut");

                entity.HasOne(d => d.BankAccountClientPaymentsIn)
                    .WithMany(p => p.BrokerBankAccountClientPaymentsIn)
                    .HasForeignKey(d => d.BankAccountClientPaymentsInId)
                    .HasConstraintName("FK_Broker_BankAccountClientPaymentsIn");

                entity.HasOne(d => d.BankAccountClientPaymentsOut)
                    .WithMany(p => p.BrokerBankAccountClientPaymentsOut)
                    .HasForeignKey(d => d.BankAccountClientPaymentsOutId)
                    .HasConstraintName("FK_Broker_BankAccountClientPaymentsOut");
            });

            modelBuilder.Entity<CassRecs>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CassRecsDate).HasColumnType("date");

                entity.Property(e => e.CassRecsStatementFileId).HasColumnName("CassRecsStatementFileID");

                entity.Property(e => e.Check1ByAuthUserId).HasColumnName("Check1ByAuthUserID");

                entity.Property(e => e.Check1UpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Check2ByAuthUserId).HasColumnName("Check2ByAuthUserID");

                entity.Property(e => e.Check2UpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.CompletedByAuthUserId).HasColumnName("CompletedByAuthUserID");

                entity.Property(e => e.CompletedDateTime).HasColumnType("datetime");

                entity.Property(e => e.CurrencyCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.LastNightsClosingLedger).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.CassRecsStatementFile)
                    .WithMany(p => p.CassRecs)
                    .HasForeignKey(d => d.CassRecsStatementFileId)
                    .HasConstraintName("FK_CassRecs_CassRecsStatementFile");

                entity.HasOne(d => d.Check1ByAuthUser)
                    .WithMany(p => p.CassRecsCheck1ByAuthUser)
                    .HasForeignKey(d => d.Check1ByAuthUserId)
                    .HasConstraintName("FK_CassRecs_AuthUser");

                entity.HasOne(d => d.Check2ByAuthUser)
                    .WithMany(p => p.CassRecsCheck2ByAuthUser)
                    .HasForeignKey(d => d.Check2ByAuthUserId)
                    .HasConstraintName("FK_CassRecs_AuthUser1");

                entity.HasOne(d => d.CompletedByAuthUser)
                    .WithMany(p => p.CassRecsCompletedByAuthUser)
                    .HasForeignKey(d => d.CompletedByAuthUserId)
                    .HasConstraintName("FK_CassRecs_CompletedByAuthUser");

                entity.HasOne(d => d.UpdatedByAuthUser)
                    .WithMany(p => p.CassRecsUpdatedByAuthUser)
                    .HasForeignKey(d => d.UpdatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CassRecs_AuthUser2");
            });

            modelBuilder.Entity<CassRecsPaymentFile>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CassRecsDate).HasColumnType("date");

                entity.Property(e => e.DocumentId)
                    .IsRequired()
                    .HasColumnName("DocumentID")
                    .HasMaxLength(100);

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.UploadedByAuthUserId).HasColumnName("UploadedByAuthUserID");

                entity.Property(e => e.UploadedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.UpdatedByAuthUser)
                    .WithMany(p => p.CassRecsPaymentFileUpdatedByAuthUser)
                    .HasForeignKey(d => d.UpdatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CassRecsPaymentFile_AuthUser");

                entity.HasOne(d => d.UploadedByAuthUser)
                    .WithMany(p => p.CassRecsPaymentFileUploadedByAuthUser)
                    .HasForeignKey(d => d.UploadedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CassRecsPaymentFile_AuthUser1");
            });

            modelBuilder.Entity<CassRecsStatementFile>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CassRecsDate).HasColumnType("date");

                entity.Property(e => e.DocumentId)
                    .IsRequired()
                    .HasColumnName("DocumentID")
                    .HasMaxLength(100);

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.UploadedByAuthUserId).HasColumnName("UploadedByAuthUserID");

                entity.Property(e => e.UploadedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<ClearingCodePrefix>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<ClientCompany>(entity =>
            {
                entity.HasIndex(e => e.Crn)
                    .HasName("IX_ClientCompany__CRN")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.HasIndex(e => new { e.ClientCompanyStatusId, e.ClientCompanyTypeId, e.ClientCompanyOptionStatusId, e.ClientCompanyCategoryId, e.DealerAppUserId, e.Id })
                    .HasName("_dta_index_ClientCompany_52_1205579333__K11_K12_K10_K25_K18_K13_K1");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountFormsSentDateTime).HasColumnType("datetime");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.ApprovedDateTime).HasColumnType("datetime");

                entity.Property(e => e.ApprovedOptionDateTime).HasColumnType("datetime");

                entity.Property(e => e.AssignNewTrades).HasDefaultValueSql("((1))");

                entity.Property(e => e.ClientCompanyCategoryId).HasColumnName("ClientCompanyCategoryID");

                entity.Property(e => e.ClientCompanyCreditTypeId)
                    .HasColumnName("ClientCompanyCreditTypeID")
                    .HasDefaultValueSql("((2))");

                entity.Property(e => e.ClientCompanyIndustrySectorId).HasColumnName("ClientCompanyIndustrySectorID");

                entity.Property(e => e.ClientCompanyLinkedGroupId).HasColumnName("ClientCompanyLinkedGroupID");

                entity.Property(e => e.ClientCompanyOptionStatusId).HasColumnName("ClientCompanyOptionStatusID");

                entity.Property(e => e.ClientCompanySalesRegionId).HasColumnName("ClientCompanySalesRegionID");

                entity.Property(e => e.ClientCompanyStatusId).HasColumnName("ClientCompanyStatusID");

                entity.Property(e => e.ClientCompanyTypeId).HasColumnName("ClientCompanyTypeID");

                entity.Property(e => e.Crn)
                    .IsRequired()
                    .HasColumnName("CRN")
                    .HasMaxLength(50);

                entity.Property(e => e.DealerAppUserId).HasColumnName("DealerAppUserID");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.EmirEea)
                    .HasColumnName("EMIR_EEA")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.EmirLei)
                    .HasColumnName("EMIR_LEI")
                    .HasMaxLength(50);

                entity.Property(e => e.FaxNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstTradeDate).HasColumnType("date");

                entity.Property(e => e.ImportantNote).HasMaxLength(2000);

                entity.Property(e => e.IsExcludedFromEmoney).HasColumnName("IsExcludedFromEMoney");

                entity.Property(e => e.IsKyc)
                    .HasColumnName("IsKYC")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsRiskWarning).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsTandCs).HasDefaultValueSql("((0))");

                entity.Property(e => e.LastContractDate).HasColumnType("datetime");

                entity.Property(e => e.MaxCreditLimit).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.MaxOpenGbp).HasColumnName("MaxOpenGBP");

                entity.Property(e => e.MaxTradeSizeGbp).HasColumnName("MaxTradeSizeGBP");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PitchedByAppUserId).HasColumnName("PitchedByAppUserID");

                entity.Property(e => e.PitchedDateTime).HasColumnType("datetime");

                entity.Property(e => e.PostCode).HasMaxLength(50);

                entity.Property(e => e.QualifiedNewTradeCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SpreadsNote).HasMaxLength(2000);

                entity.Property(e => e.TelephoneNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TradingAddress).HasMaxLength(2000);

                entity.Property(e => e.TradingName).HasMaxLength(200);

                entity.Property(e => e.TradingPostCode).HasMaxLength(2000);

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.WebsiteUrl)
                    .IsRequired()
                    .HasColumnName("WebsiteURL")
                    .HasMaxLength(200);

                entity.HasOne(d => d.ClientCompanyCategory)
                    .WithMany(p => p.ClientCompany)
                    .HasForeignKey(d => d.ClientCompanyCategoryId)
                    .HasConstraintName("FK_ClientCompany_ClientCompanyCategory");

                entity.HasOne(d => d.ClientCompanyCreditType)
                    .WithMany(p => p.ClientCompany)
                    .HasForeignKey(d => d.ClientCompanyCreditTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompany_ClientCompanyCreditType");

                entity.HasOne(d => d.ClientCompanyIndustrySector)
                    .WithMany(p => p.ClientCompany)
                    .HasForeignKey(d => d.ClientCompanyIndustrySectorId)
                    .HasConstraintName("FK_ClientCompany_ClientCompanyIndustrySector");

                entity.HasOne(d => d.ClientCompanyLinkedGroup)
                    .WithMany(p => p.ClientCompany)
                    .HasForeignKey(d => d.ClientCompanyLinkedGroupId)
                    .HasConstraintName("FK_ClientCompany_ClientCompanyLinkedGroup");

                entity.HasOne(d => d.ClientCompanySalesRegion)
                    .WithMany(p => p.ClientCompany)
                    .HasForeignKey(d => d.ClientCompanySalesRegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompany_ClientCompanySalesRegion");

                entity.HasOne(d => d.ClientCompanyStatus)
                    .WithMany(p => p.ClientCompany)
                    .HasForeignKey(d => d.ClientCompanyStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompany_ClientCompanyStatus");

                entity.HasOne(d => d.ClientCompanyType)
                    .WithMany(p => p.ClientCompany)
                    .HasForeignKey(d => d.ClientCompanyTypeId)
                    .HasConstraintName("FK_ClientCompany_ClientCompanyType");

                entity.HasOne(d => d.QualifiedNewTradeCodeNavigation)
                    .WithMany(p => p.ClientCompany)
                    .HasForeignKey(d => d.QualifiedNewTradeCode)
                    .HasConstraintName("FK_ClientCompany_FXForwardTrade");
            });

            modelBuilder.Entity<ClientCompanyActivityReport>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientCompanyId).HasColumnName("ClientCompanyID");

                entity.Property(e => e.ClientCompanyName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.LastActivityReportSentByAppUserId).HasColumnName("LastActivityReportSentByAppUserID");

                entity.Property(e => e.LastActivityReportSentDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<ClientCompanyCategory>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ClientCompanyCompliance>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AmlriskId).HasColumnName("AMLRiskID");

                entity.Property(e => e.BalanceSheetGbp)
                    .HasColumnName("BalanceSheetGBP")
                    .HasColumnType("decimal(25, 2)");

                entity.Property(e => e.ClassificationId).HasColumnName("ClassificationID");

                entity.Property(e => e.ClientCompanyId).HasColumnName("ClientCompanyID");

                entity.Property(e => e.ExpectedFrequencyId).HasColumnName("ExpectedFrequencyID");

                entity.Property(e => e.ExpectedMaxTradeSize).HasColumnType("decimal(25, 2)");

                entity.Property(e => e.ExpectedTotalVolume).HasColumnType("decimal(25, 2)");

                entity.Property(e => e.IsMiFid).HasColumnName("IsMiFID");

                entity.Property(e => e.NatureId).HasColumnName("NatureID");

                entity.Property(e => e.OwnFundsGbp)
                    .HasColumnName("OwnFundsGBP")
                    .HasColumnType("decimal(25, 2)");

                entity.Property(e => e.ReasonId).HasColumnName("ReasonID");

                entity.Property(e => e.RefreshDueDateTime).HasColumnType("datetime");

                entity.Property(e => e.RegisteredDomicileCountryId).HasColumnName("RegisteredDomicileCountryID");

                entity.Property(e => e.Ttca).HasColumnName("TTCA");

                entity.Property(e => e.TurnoverGbp)
                    .HasColumnName("TurnoverGBP")
                    .HasColumnType("decimal(25, 2)");

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Amlrisk)
                    .WithMany(p => p.ClientCompanyCompliance)
                    .HasForeignKey(d => d.AmlriskId)
                    .HasConstraintName("FK_ClientCompanyCompliance_AMLRisk");

                entity.HasOne(d => d.Classification)
                    .WithMany(p => p.ClientCompanyCompliance)
                    .HasForeignKey(d => d.ClassificationId)
                    .HasConstraintName("FK_ClientCompanyCompliance_ComplianceClassification");

                entity.HasOne(d => d.ClientCompany)
                    .WithMany(p => p.ClientCompanyCompliance)
                    .HasForeignKey(d => d.ClientCompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyCompliance_ClientCompany");

                entity.HasOne(d => d.ExpectedFrequency)
                    .WithMany(p => p.ClientCompanyCompliance)
                    .HasForeignKey(d => d.ExpectedFrequencyId)
                    .HasConstraintName("FK_ClientCompanyCompliance_ExpectedFrequency");

                entity.HasOne(d => d.Nature)
                    .WithMany(p => p.ClientCompanyCompliance)
                    .HasForeignKey(d => d.NatureId)
                    .HasConstraintName("FK_ClientCompanyCompliance_ComplianceNature");

                entity.HasOne(d => d.Reason)
                    .WithMany(p => p.ClientCompanyCompliance)
                    .HasForeignKey(d => d.ReasonId)
                    .HasConstraintName("FK_ClientCompanyCompliance_ComplianceReason");

                entity.HasOne(d => d.RegisteredDomicileCountry)
                    .WithMany(p => p.ClientCompanyCompliance)
                    .HasForeignKey(d => d.RegisteredDomicileCountryId)
                    .HasConstraintName("FK_ClientCompanyCompliance_Country");
            });

            modelBuilder.Entity<ClientCompanyComplianceCorporateSector>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientCompanyComplianceId).HasColumnName("ClientCompanyComplianceID");

                entity.Property(e => e.ComplianceCorporateSectorFinancialId).HasColumnName("ComplianceCorporateSectorFinancialID");

                entity.Property(e => e.ComplianceCorporateSectorNonFinancialId).HasColumnName("ComplianceCorporateSectorNonFinancialID");

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ClientCompanyCompliance)
                    .WithMany(p => p.ClientCompanyComplianceCorporateSector)
                    .HasForeignKey(d => d.ClientCompanyComplianceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyComplianceCorporateSector_ClientCompanyCompliance");

                entity.HasOne(d => d.ComplianceCorporateSectorFinancial)
                    .WithMany(p => p.ClientCompanyComplianceCorporateSector)
                    .HasForeignKey(d => d.ComplianceCorporateSectorFinancialId)
                    .HasConstraintName("FK_ClientCompanyComplianceCorporateSector_ComplianceCorporateSectorFinancial");

                entity.HasOne(d => d.ComplianceCorporateSectorNonFinancial)
                    .WithMany(p => p.ClientCompanyComplianceCorporateSector)
                    .HasForeignKey(d => d.ComplianceCorporateSectorNonFinancialId)
                    .HasConstraintName("FK_ClientCompanyComplianceCorporateSector_ComplianceCorporateSectorNonFinancial");

                entity.HasOne(d => d.UpdatedByAuthUser)
                    .WithMany(p => p.ClientCompanyComplianceCorporateSector)
                    .HasForeignKey(d => d.UpdatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyComplianceCorporateSector_AuthUser");
            });

            modelBuilder.Entity<ClientCompanyComplianceCurrency>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientCompanyComplianceId).HasColumnName("ClientCompanyComplianceID");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.ClientCompanyCompliance)
                    .WithMany(p => p.ClientCompanyComplianceCurrency)
                    .HasForeignKey(d => d.ClientCompanyComplianceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyComplianceCurrency_ClientCompanyCompliance");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.ClientCompanyComplianceCurrency)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyComplianceCurrency_Currency");

                entity.HasOne(d => d.UpdatedByAuthUser)
                    .WithMany(p => p.ClientCompanyComplianceCurrency)
                    .HasForeignKey(d => d.UpdatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyComplianceCurrency_AuthUser");
            });

            modelBuilder.Entity<ClientCompanyComplianceNote>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AuthUserId).HasColumnName("AuthUserID");

                entity.Property(e => e.ClientCompanyId).HasColumnName("ClientCompanyID");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NoteText).IsRequired();

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.HasOne(d => d.AuthUser)
                    .WithMany(p => p.ClientCompanyComplianceNote)
                    .HasForeignKey(d => d.AuthUserId)
                    .HasConstraintName("FK_ClientCompanyComplianceNote_AuthUser");

                entity.HasOne(d => d.ClientCompany)
                    .WithMany(p => p.ClientCompanyComplianceNote)
                    .HasForeignKey(d => d.ClientCompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyComplianceNote_ClientCompany");
            });

            modelBuilder.Entity<ClientCompanyContact>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AspcreationDate)
                    .HasColumnName("ASPCreationDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Aspnumber)
                    .HasColumnName("ASPNumber")
                    .HasMaxLength(9);

                entity.Property(e => e.AuthUserId).HasColumnName("AuthUserID");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.BloombergGpi).HasMaxLength(255);

                entity.Property(e => e.ClientCompanyId).HasColumnName("ClientCompanyID");

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.Forename)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasMaxLength(162)
                    .HasComputedColumnSql("(((isnull([Title]+' ','')+[Forename])+' ')+[Surname])");

                entity.Property(e => e.LastEmailChangeDate).HasColumnType("datetime");

                entity.Property(e => e.LastTelephoneChangeDate).HasColumnType("datetime");

                entity.Property(e => e.NiNumber).HasMaxLength(50);

                entity.Property(e => e.Position).HasMaxLength(50);

                entity.Property(e => e.RecAmreport).HasColumnName("RecAMReport");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TelephoneDirect).HasMaxLength(50);

                entity.Property(e => e.TelephoneMobile).HasMaxLength(50);

                entity.Property(e => e.TelephoneOther).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(10);

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.AuthUser)
                    .WithMany(p => p.ClientCompanyContactAuthUser)
                    .HasForeignKey(d => d.AuthUserId)
                    .HasConstraintName("FK_ClientCompanyContact_AuthUser");

                entity.HasOne(d => d.ClientCompany)
                    .WithMany(p => p.ClientCompanyContact)
                    .HasForeignKey(d => d.ClientCompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyContact_ClientCompany");

                entity.HasOne(d => d.UpdatedByAuthUser)
                    .WithMany(p => p.ClientCompanyContactUpdatedByAuthUser)
                    .HasForeignKey(d => d.UpdatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyContact_UpdatedByAuthUser");
            });

            modelBuilder.Entity<ClientCompanyContactCategory>(entity =>
            {
                entity.HasKey(e => new { e.ClientCompanyContactId, e.ContactCategoryId });

                entity.Property(e => e.ClientCompanyContactId).HasColumnName("ClientCompanyContactID");

                entity.Property(e => e.ContactCategoryId).HasColumnName("ContactCategoryID");

                entity.Property(e => e.CreatedByAuthUserId).HasColumnName("CreatedByAuthUserID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ClientCompanyCreditType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ClientCompanyCurrencyDefaultOpi>(entity =>
            {
                entity.HasKey(e => new { e.ClientCompanyId, e.CurrencyId });

                entity.ToTable("ClientCompanyCurrencyDefaultOPI");

                entity.Property(e => e.ClientCompanyId).HasColumnName("ClientCompanyID");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.ClientCompanyOpiid).HasColumnName("ClientCompanyOPIID");

                entity.Property(e => e.UpdateAuthUserId).HasColumnName("UpdateAuthUserID");

                entity.Property(e => e.UpdateDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.HasOne(d => d.ClientCompany)
                    .WithMany(p => p.ClientCompanyCurrencyDefaultOpi)
                    .HasForeignKey(d => d.ClientCompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyCurrencyDefaultOPI_ClientCompany");

                entity.HasOne(d => d.ClientCompanyOpi)
                    .WithMany(p => p.ClientCompanyCurrencyDefaultOpi)
                    .HasForeignKey(d => d.ClientCompanyOpiid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyCurrencyDefaultOPI_ClientCompanyOPI");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.ClientCompanyCurrencyDefaultOpi)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyCurrencyDefaultOPI_Currency");

                entity.HasOne(d => d.UpdateAuthUser)
                    .WithMany(p => p.ClientCompanyCurrencyDefaultOpi)
                    .HasForeignKey(d => d.UpdateAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyCurrencyDefaultOPI_AuthUser");
            });

            modelBuilder.Entity<ClientCompanyIbrelationship>(entity =>
            {
                entity.ToTable("ClientCompanyIBRelationship");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientCompanyId).HasColumnName("ClientCompanyID");

                entity.Property(e => e.IntroducingBrokerId).HasColumnName("IntroducingBrokerID");

                entity.Property(e => e.Percentage).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.UpdateDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.HasOne(d => d.ClientCompany)
                    .WithMany(p => p.ClientCompanyIbrelationship)
                    .HasForeignKey(d => d.ClientCompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyIBRelationship_ClientCompany");

                entity.HasOne(d => d.UpdatedByAuthUser)
                    .WithMany(p => p.ClientCompanyIbrelationship)
                    .HasForeignKey(d => d.UpdatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyIBRelationship_IntroducingBroker");
            });

            modelBuilder.Entity<ClientCompanyIndustrySector>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ClientCompanyLinkedGroup>(entity =>
            {
                entity.HasIndex(e => e.Description)
                    .HasName("IX_ClientCompanyLinkedGroup")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.HasOne(d => d.UpdatedByAuthUser)
                    .WithMany(p => p.ClientCompanyLinkedGroup)
                    .HasForeignKey(d => d.UpdatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyLinkedGroup_AuthUser");
            });

            modelBuilder.Entity<ClientCompanyNote>(entity =>
            {
                entity.HasIndex(e => new { e.ClientCompanyId, e.CreateDateTime })
                    .HasName("_dta_index_ClientCompanyNote_52_1605580758__K2_K6");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AuthUserId).HasColumnName("AuthUserID");

                entity.Property(e => e.ClientCompanyId).HasColumnName("ClientCompanyID");

                entity.Property(e => e.CreateDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NoteText).IsRequired();

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.HasOne(d => d.AuthUser)
                    .WithMany(p => p.ClientCompanyNote)
                    .HasForeignKey(d => d.AuthUserId)
                    .HasConstraintName("FK_ClientCompanyNote_AuthUser");

                entity.HasOne(d => d.ClientCompany)
                    .WithMany(p => p.ClientCompanyNote)
                    .HasForeignKey(d => d.ClientCompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyNote_ClientCompany");
            });

            modelBuilder.Entity<ClientCompanyOnlineDetails>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientCompanyId).HasColumnName("ClientCompanyID");

                entity.Property(e => e.Collateral).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.MaxOpen).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.MaxTenor).HasColumnType("datetime");

                entity.Property(e => e.MaxTradeSize).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.ClientCompany)
                    .WithMany(p => p.ClientCompanyOnlineDetails)
                    .HasForeignKey(d => d.ClientCompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompany_ClientCompanyOnlineDetails");
            });

            modelBuilder.Entity<ClientCompanyOnlineDetailsSkew>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientCompanyOnlineDetailsId).HasColumnName("ClientCompanyOnlineDetailsID");

                entity.Property(e => e.Currency1Id).HasColumnName("Currency1ID");

                entity.Property(e => e.Currency2Id).HasColumnName("Currency2ID");

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.ClientCompanyOnlineDetails)
                    .WithMany(p => p.ClientCompanyOnlineDetailsSkew)
                    .HasForeignKey(d => d.ClientCompanyOnlineDetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyOnlineDetailsSkew_ClientCompanyOnlineDetails");

                entity.HasOne(d => d.Currency1)
                    .WithMany(p => p.ClientCompanyOnlineDetailsSkewCurrency1)
                    .HasForeignKey(d => d.Currency1Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyOnlineDetailsSkew_Currency1");

                entity.HasOne(d => d.Currency2)
                    .WithMany(p => p.ClientCompanyOnlineDetailsSkewCurrency2)
                    .HasForeignKey(d => d.Currency2Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyOnlineDetailsSkew_Currency2");
            });

            modelBuilder.Entity<ClientCompanyOnlineSpreadAdjustment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientCompanyOnlineDetailsId).HasColumnName("ClientCompanyOnlineDetailsID");

                entity.Property(e => e.Currency1Id).HasColumnName("Currency1ID");

                entity.Property(e => e.Currency2Id).HasColumnName("Currency2ID");

                entity.Property(e => e.ExpirationDateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.ClientCompanyOnlineDetails)
                    .WithMany(p => p.ClientCompanyOnlineSpreadAdjustment)
                    .HasForeignKey(d => d.ClientCompanyOnlineDetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyOnlineSpreadAdjustment_ClientCompanyOnlineDetails");

                entity.HasOne(d => d.Currency1)
                    .WithMany(p => p.ClientCompanyOnlineSpreadAdjustmentCurrency1)
                    .HasForeignKey(d => d.Currency1Id)
                    .HasConstraintName("FK_ClientCompanyOnlineSpreadAdjustment_Currency1");

                entity.HasOne(d => d.Currency2)
                    .WithMany(p => p.ClientCompanyOnlineSpreadAdjustmentCurrency2)
                    .HasForeignKey(d => d.Currency2Id)
                    .HasConstraintName("FK_ClientCompanyOnlineSpreadAdjustment_Currency2");
            });

            modelBuilder.Entity<ClientCompanyOpi>(entity =>
            {
                entity.ToTable("ClientCompanyOPI");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.AccountNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AuthorisedByAuthUserId).HasColumnName("AuthorisedByAuthUserID");

                entity.Property(e => e.AuthorisedDateTime).HasColumnType("datetime");

                entity.Property(e => e.BankName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.BeneficiaryName).HasMaxLength(250);

                entity.Property(e => e.ClearingCodePrefixId).HasColumnName("ClearingCodePrefixID");

                entity.Property(e => e.ClientCompanyId).HasColumnName("ClientCompanyID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreatedByAuthUserId).HasColumnName("CreatedByAuthUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Iban)
                    .HasColumnName("IBAN")
                    .HasMaxLength(50);

                entity.Property(e => e.Reference)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RejectedByAuthUserId).HasColumnName("RejectedByAuthUserID");

                entity.Property(e => e.RejectedDateTime).HasColumnType("datetime");

                entity.Property(e => e.SortCode).HasMaxLength(50);

                entity.Property(e => e.SwiftCode)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ClearingCodePrefix)
                    .WithMany(p => p.ClientCompanyOpi)
                    .HasForeignKey(d => d.ClearingCodePrefixId)
                    .HasConstraintName("FK_ClientCompanyOPI_ClearingCodePrefix");

                entity.HasOne(d => d.ClientCompany)
                    .WithMany(p => p.ClientCompanyOpi)
                    .HasForeignKey(d => d.ClientCompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyOPI_ClientCompany");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.ClientCompanyOpi)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_ClientCompanyOPI_Country");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.ClientCompanyOpi)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyOPI_Currency");

                entity.HasOne(d => d.RejectedByAuthUser)
                    .WithMany(p => p.ClientCompanyOpi)
                    .HasForeignKey(d => d.RejectedByAuthUserId)
                    .HasConstraintName("FK_ClientCompanyOPI_AuthUser");
            });

            modelBuilder.Entity<ClientCompanyOpiduplicate>(entity =>
            {
                entity.ToTable("ClientCompanyOPIDuplicate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedByAuthUserId).HasColumnName("CreatedByAuthUserID");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DuplicateClientCompanyOpiid).HasColumnName("DuplicateClientCompanyOPIID");

                entity.Property(e => e.IsOk).HasColumnName("IsOK");

                entity.Property(e => e.IsOkupdatedByAuthUserId).HasColumnName("IsOKUpdatedByAuthUserID");

                entity.Property(e => e.IsOkupdatedDateTime)
                    .HasColumnName("isOKUpdatedDateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(250);

                entity.Property(e => e.OriginalClientCompanyOpiid).HasColumnName("OriginalClientCompanyOPIID");

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.HasOne(d => d.CreatedByAuthUser)
                    .WithMany(p => p.ClientCompanyOpiduplicateCreatedByAuthUser)
                    .HasForeignKey(d => d.CreatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyOPIDuplicate_AuthUser1");

                entity.HasOne(d => d.DuplicateClientCompanyOpi)
                    .WithMany(p => p.ClientCompanyOpiduplicateDuplicateClientCompanyOpi)
                    .HasForeignKey(d => d.DuplicateClientCompanyOpiid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyOPIDuplicate_ClientCompanyOPI");

                entity.HasOne(d => d.IsOkupdatedByAuthUser)
                    .WithMany(p => p.ClientCompanyOpiduplicateIsOkupdatedByAuthUser)
                    .HasForeignKey(d => d.IsOkupdatedByAuthUserId)
                    .HasConstraintName("FK_ClientCompanyOPIDuplicate_AuthUser");

                entity.HasOne(d => d.OriginalClientCompanyOpi)
                    .WithMany(p => p.ClientCompanyOpiduplicateOriginalClientCompanyOpi)
                    .HasForeignKey(d => d.OriginalClientCompanyOpiid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyOPIDuplicate_ClientCompanyOPI1");
            });

            modelBuilder.Entity<ClientCompanyOpitransaction>(entity =>
            {
                entity.ToTable("ClientCompanyOPITransaction");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.ClientCompanyOpiid).HasColumnName("ClientCompanyOPIID");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.OpiaccountName)
                    .HasColumnName("OPIAccountName")
                    .HasMaxLength(100);

                entity.Property(e => e.OpiaccountNumber)
                    .HasColumnName("OPIAccountNumber")
                    .HasMaxLength(50);

                entity.Property(e => e.OpibankAddress).HasColumnName("OPIBankAddress");

                entity.Property(e => e.OpibankName)
                    .HasColumnName("OPIBankName")
                    .HasMaxLength(100);

                entity.Property(e => e.OpibeneficiaryAddress).HasColumnName("OPIBeneficiaryAddress");

                entity.Property(e => e.OpibeneficiaryName)
                    .HasColumnName("OPIBeneficiaryName")
                    .HasMaxLength(250);

                entity.Property(e => e.OpicountryId).HasColumnName("OPICountryID");

                entity.Property(e => e.Opidescription)
                    .HasColumnName("OPIDescription")
                    .HasMaxLength(50);

                entity.Property(e => e.OpidetailsUpdated).HasColumnName("OPIDetailsUpdated");

                entity.Property(e => e.Opiiban)
                    .HasColumnName("OPIIBAN")
                    .HasMaxLength(50);

                entity.Property(e => e.Opireference)
                    .HasColumnName("OPIReference")
                    .HasMaxLength(50);

                entity.Property(e => e.OpisortCode)
                    .HasColumnName("OPISortCode")
                    .HasMaxLength(50);

                entity.Property(e => e.OpiswiftCode)
                    .HasColumnName("OPISwiftCode")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.HasOne(d => d.ClientCompanyOpi)
                    .WithMany(p => p.ClientCompanyOpitransaction)
                    .HasForeignKey(d => d.ClientCompanyOpiid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyOPITransaction_ClientCompanyOPI");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.ClientCompanyOpitransaction)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyOPITransaction_Currency");

                entity.HasOne(d => d.Opicountry)
                    .WithMany(p => p.ClientCompanyOpitransaction)
                    .HasForeignKey(d => d.OpicountryId)
                    .HasConstraintName("FK_ClientCompanyOPITransaction_Country");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.ClientCompanyOpitransaction)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyOPITransaction_Payment");
            });

            modelBuilder.Entity<ClientCompanyOptionCount>(entity =>
            {
                entity.HasKey(e => e.ClientCompanyId);

                entity.Property(e => e.ClientCompanyId)
                    .HasColumnName("ClientCompanyID")
                    .ValueGeneratedNever();

                entity.Property(e => e.OptionCount).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.ClientCompany)
                    .WithOne(p => p.ClientCompanyOptionCount)
                    .HasForeignKey<ClientCompanyOptionCount>(d => d.ClientCompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyOptionCount_ClientCompany");
            });

            modelBuilder.Entity<ClientCompanyPipeline>(entity =>
            {
                entity.HasKey(e => e.ClientCompanyId);

                entity.Property(e => e.ClientCompanyId)
                    .HasColumnName("ClientCompanyID")
                    .ValueGeneratedNever();

                entity.Property(e => e.LastCall).HasColumnType("datetime");

                entity.Property(e => e.LastEmail).HasColumnType("datetime");

                entity.Property(e => e.LastEmailFrom).HasMaxLength(256);

                entity.Property(e => e.LastEmailTo).HasMaxLength(256);

                entity.Property(e => e.LastLongCall).HasColumnType("datetime");

                entity.Property(e => e.NextActionDueDate).HasColumnType("date");

                entity.Property(e => e.NextActionUpdated).HasColumnType("datetime");

                entity.Property(e => e.NextPipelineActionId).HasColumnName("NextPipelineActionID");

                entity.Property(e => e.NextTradeDate).HasColumnType("datetime");

                entity.Property(e => e.Progress).HasDefaultValueSql("((0))");

                entity.Property(e => e.TotalCalls).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdateAuthUserId).HasColumnName("UpdateAuthUserID");

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.HasOne(d => d.ClientCompany)
                    .WithOne(p => p.ClientCompanyPipeline)
                    .HasForeignKey<ClientCompanyPipeline>(d => d.ClientCompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanySalesPipeline_ClientCompany");

                entity.HasOne(d => d.NextPipelineAction)
                    .WithMany(p => p.ClientCompanyPipeline)
                    .HasForeignKey(d => d.NextPipelineActionId)
                    .HasConstraintName("FK_ClientCompanySalesPipeline_SalesPipelineAction");

                entity.HasOne(d => d.UpdateAuthUser)
                    .WithMany(p => p.ClientCompanyPipeline)
                    .HasForeignKey(d => d.UpdateAuthUserId)
                    .HasConstraintName("FK_ClientCompanySalesPipeline_AuthUser");
            });

            modelBuilder.Entity<ClientCompanySalesAppUser>(entity =>
            {
                entity.HasKey(e => new { e.ClientCompanyId, e.SalesPersonAppUserId });

                entity.Property(e => e.ClientCompanyId).HasColumnName("ClientCompanyID");

                entity.Property(e => e.SalesPersonAppUserId).HasColumnName("SalesPersonAppUserID");

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ClientCompany)
                    .WithMany(p => p.ClientCompanySalesAppUser)
                    .HasForeignKey(d => d.ClientCompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanySalesAppUser_ClientCompany");

                entity.HasOne(d => d.SalesPersonAppUser)
                    .WithMany(p => p.ClientCompanySalesAppUser)
                    .HasForeignKey(d => d.SalesPersonAppUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanySalesAppUser_AppUser");

                entity.HasOne(d => d.UpdatedByAuthUser)
                    .WithMany(p => p.ClientCompanySalesAppUser)
                    .HasForeignKey(d => d.UpdatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanySalesAppUser_AuthUser");
            });

            modelBuilder.Entity<ClientCompanySalesRegion>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PermissionId).HasColumnName("PermissionID");
            });

            modelBuilder.Entity<ClientCompanyStatus>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ClientCompanyTradeCount>(entity =>
            {
                entity.HasKey(e => e.ClientCompanyId);

                entity.Property(e => e.ClientCompanyId)
                    .HasColumnName("ClientCompanyID")
                    .ValueGeneratedNever();

                entity.Property(e => e.TradeCount).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.ClientCompany)
                    .WithOne(p => p.ClientCompanyTradeCount)
                    .HasForeignKey<ClientCompanyTradeCount>(d => d.ClientCompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyTradeCount_ClientCompany");
            });

            modelBuilder.Entity<ClientCompanyType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ClientCompanyVirtualAccount>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientCompanyId).HasColumnName("ClientCompanyID");

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.VirtualAccountTypeId).HasColumnName("VirtualAccountTypeID");

                entity.HasOne(d => d.ClientCompany)
                    .WithMany(p => p.ClientCompanyVirtualAccount)
                    .HasForeignKey(d => d.ClientCompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyVirtualAccount_ClientCompany");

                entity.HasOne(d => d.VirtualAccountType)
                    .WithMany(p => p.ClientCompanyVirtualAccount)
                    .HasForeignKey(d => d.VirtualAccountTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyVirtualAccount_VirtualAccountType");
            });

            modelBuilder.Entity<ClientCompanyVirtualAccountCurrencyBalance>(entity =>
            {
                entity.HasKey(e => new { e.ClientCompanyVirtualAccountId, e.CurrencyId });

                entity.Property(e => e.ClientCompanyVirtualAccountId).HasColumnName("ClientCompanyVirtualAccountID");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.Balance)
                    .HasColumnType("decimal(25, 8)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.BalanceDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TransactionCommitId).HasColumnName("TransactionCommitID");

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.HasOne(d => d.ClientCompanyVirtualAccount)
                    .WithMany(p => p.ClientCompanyVirtualAccountCurrencyBalance)
                    .HasForeignKey(d => d.ClientCompanyVirtualAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyVirtualAccountCurrencyBalance_ClientCompanyVirtualAccount");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.ClientCompanyVirtualAccountCurrencyBalance)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyVirtualAccountCurrencyBalance_Currency");
            });

            modelBuilder.Entity<ClientCompanyVirtualAccountCurrencyBalanceHistory>(entity =>
            {
                entity.HasKey(e => new { e.ClientCompanyVirtualAccountId, e.CurrencyId, e.TransactionCommitId });

                entity.Property(e => e.ClientCompanyVirtualAccountId).HasColumnName("ClientCompanyVirtualAccountID");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.TransactionCommitId).HasColumnName("TransactionCommitID");

                entity.Property(e => e.Balance).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.BalanceDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.HasOne(d => d.ClientCompanyVirtualAccount)
                    .WithMany(p => p.ClientCompanyVirtualAccountCurrencyBalanceHistory)
                    .HasForeignKey(d => d.ClientCompanyVirtualAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyVirtualAccountCurrencyBalanceHistory_ClientCompanyVirtualAccount");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.ClientCompanyVirtualAccountCurrencyBalanceHistory)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyVirtualAccountCurrencyBalanceHistory_Currency");

                entity.HasOne(d => d.TransactionCommit)
                    .WithMany(p => p.ClientCompanyVirtualAccountCurrencyBalanceHistory)
                    .HasForeignKey(d => d.TransactionCommitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientCompanyVirtualAccountCurrencyBalanceHistory_TransactionCommit");

                entity.HasOne(d => d.UpdatedByAuthUser)
                    .WithMany(p => p.ClientCompanyVirtualAccountCurrencyBalanceHistory)
                    .HasForeignKey(d => d.UpdatedByAuthUserId)
                    .HasConstraintName("FK_ClientCompanyVirtualAccountCurrencyBalanceHistory_AuthUser");
            });

            modelBuilder.Entity<ClientSiteAction>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientSiteActionStatusId).HasColumnName("ClientSiteActionStatusID");

                entity.Property(e => e.ClientSiteActionTypeId).HasColumnName("ClientSiteActionTypeID");

                entity.Property(e => e.CreatedByAuthUserId).HasColumnName("CreatedByAuthUserID");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Details)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdatedTimestamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.HasOne(d => d.ClientSiteActionStatus)
                    .WithMany(p => p.ClientSiteAction)
                    .HasForeignKey(d => d.ClientSiteActionStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientSiteAction_ClientSiteActionStatus");

                entity.HasOne(d => d.ClientSiteActionType)
                    .WithMany(p => p.ClientSiteAction)
                    .HasForeignKey(d => d.ClientSiteActionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientSiteAction_ClientSiteActionType");

                entity.HasOne(d => d.CreatedByAuthUser)
                    .WithMany(p => p.ClientSiteActionCreatedByAuthUser)
                    .HasForeignKey(d => d.CreatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientSiteAction_AuthUser_Client");

                entity.HasOne(d => d.UpdatedByAuthUser)
                    .WithMany(p => p.ClientSiteActionUpdatedByAuthUser)
                    .HasForeignKey(d => d.UpdatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientSiteAction_AuthUser_Trader");
            });

            modelBuilder.Entity<ClientSiteAction2ClientCompanyOpi>(entity =>
            {
                entity.ToTable("ClientSiteAction2ClientCompanyOPI");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientCompanyOpiid).HasColumnName("ClientCompanyOPIID");

                entity.Property(e => e.ClientSiteActionId).HasColumnName("ClientSiteActionID");

                entity.HasOne(d => d.ClientCompanyOpi)
                    .WithMany(p => p.ClientSiteAction2ClientCompanyOpi)
                    .HasForeignKey(d => d.ClientCompanyOpiid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientSiteAction2ClientCompanyOPI_ClientCompanyOPI");

                entity.HasOne(d => d.ClientSiteAction)
                    .WithMany(p => p.ClientSiteAction2ClientCompanyOpi)
                    .HasForeignKey(d => d.ClientSiteActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientSiteAction2ClientCompanyOPI_ClientSiteAction");
            });

            modelBuilder.Entity<ClientSiteAction2FixFxforwardTrade>(entity =>
            {
                entity.ToTable("ClientSiteAction2FixFXForwardTrade");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientSiteActionId).HasColumnName("ClientSiteActionID");

                entity.Property(e => e.FxforwardTradeCode)
                    .IsRequired()
                    .HasColumnName("FXForwardTradeCode")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.ClientSiteAction)
                    .WithMany(p => p.ClientSiteAction2FixFxforwardTrade)
                    .HasForeignKey(d => d.ClientSiteActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientSiteAction2FixFXForwardTrade_ClientSiteAction");

                entity.HasOne(d => d.FxforwardTradeCodeNavigation)
                    .WithMany(p => p.ClientSiteAction2FixFxforwardTrade)
                    .HasForeignKey(d => d.FxforwardTradeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientSiteAction2FixFXForwardTrade_FXForwardTrade");
            });

            modelBuilder.Entity<ClientSiteAction2FxforwardTrade2Opi>(entity =>
            {
                entity.ToTable("ClientSiteAction2FXForwardTrade2OPI");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientSiteActionId).HasColumnName("ClientSiteActionID");

                entity.Property(e => e.FxforwardTrade2Opiid).HasColumnName("FXForwardTrade2OPIID");

                entity.HasOne(d => d.ClientSiteAction)
                    .WithMany(p => p.ClientSiteAction2FxforwardTrade2Opi)
                    .HasForeignKey(d => d.ClientSiteActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientSiteAction2FXForwardTrade2OPI_ClientSiteAction");

                entity.HasOne(d => d.FxforwardTrade2Opi)
                    .WithMany(p => p.ClientSiteAction2FxforwardTrade2Opi)
                    .HasForeignKey(d => d.FxforwardTrade2Opiid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientSiteAction2FXForwardTrade2OPI_FXForwardTrade2OPI");
            });

            modelBuilder.Entity<ClientSiteAction2Fxswap>(entity =>
            {
                entity.ToTable("ClientSiteAction2FXSwap");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientSiteActionId).HasColumnName("ClientSiteActionID");

                entity.Property(e => e.FxswapId).HasColumnName("FXSwapID");
            });

            modelBuilder.Entity<ClientSiteActionStatus>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Details)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ClientSiteActionType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Details)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Commission>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppUserId).HasColumnName("AppUserID");

                entity.Property(e => e.Commission1).HasColumnName("Commission");

                entity.Property(e => e.CommissionTypeId).HasColumnName("CommissionTypeID");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.Commission)
                    .HasForeignKey(d => d.AppUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Commission_AppUser");

                entity.HasOne(d => d.CommissionType)
                    .WithMany(p => p.Commission)
                    .HasForeignKey(d => d.CommissionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Commission_CommissionType");
            });

            modelBuilder.Entity<CommissionType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DefaultCommissionRate).HasDefaultValueSql("((0.15))");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ComplianceClassification>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Sequence).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<ComplianceClassificationFile>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientCompanyComplianceId).HasColumnName("ClientCompanyComplianceID");

                entity.Property(e => e.ComplianceClassificationId).HasColumnName("ComplianceClassificationID");

                entity.Property(e => e.DocumentId)
                    .IsRequired()
                    .HasColumnName("DocumentID")
                    .HasMaxLength(100);

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.UploadedByAuthUserId).HasColumnName("UploadedByAuthUserID");

                entity.Property(e => e.UploadedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.ClientCompanyCompliance)
                    .WithMany(p => p.ComplianceClassificationFile)
                    .HasForeignKey(d => d.ClientCompanyComplianceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ComplianceClassificationFile_ClientCompanyCompliance");

                entity.HasOne(d => d.ComplianceClassification)
                    .WithMany(p => p.ComplianceClassificationFile)
                    .HasForeignKey(d => d.ComplianceClassificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ComplianceClassificationFile_ComplianceClassification");

                entity.HasOne(d => d.UpdatedByAuthUser)
                    .WithMany(p => p.ComplianceClassificationFileUpdatedByAuthUser)
                    .HasForeignKey(d => d.UpdatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ComplianceClassificationFile_AuthUser");

                entity.HasOne(d => d.UploadedByAuthUser)
                    .WithMany(p => p.ComplianceClassificationFileUploadedByAuthUser)
                    .HasForeignKey(d => d.UploadedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ComplianceClassificationFile_AuthUser1");
            });

            modelBuilder.Entity<ComplianceCorporateSectorFinancial>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Sequence).HasDefaultValueSql("((0))");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(3);
            });

            modelBuilder.Entity<ComplianceCorporateSectorNonFinancial>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Sequence).HasDefaultValueSql("((0))");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(3);
            });

            modelBuilder.Entity<ComplianceIsincurrencyValueDate>(entity =>
            {
                entity.ToTable("ComplianceISINCurrencyValueDate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CurrencyPair)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Isin)
                    .IsRequired()
                    .HasColumnName("ISIN")
                    .HasMaxLength(12);

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.ValueDate).HasColumnType("date");

                entity.HasOne(d => d.UpdatedByAuthUser)
                    .WithMany(p => p.ComplianceIsincurrencyValueDate)
                    .HasForeignKey(d => d.UpdatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ComplianceISIN_AuthUser");
            });

            modelBuilder.Entity<ComplianceNature>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.EmirValue)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.Sequence).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<ComplianceQuestionnaire>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientCompanyComplianceId).HasColumnName("ClientCompanyComplianceID");

                entity.Property(e => e.ComplianceQuestionnaireAnswerId).HasColumnName("ComplianceQuestionnaireAnswerID");

                entity.Property(e => e.ComplianceQuestionnaireQuestionId).HasColumnName("ComplianceQuestionnaireQuestionID");

                entity.Property(e => e.IsFirstTimeSelect)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.ClientCompanyCompliance)
                    .WithMany(p => p.ComplianceQuestionnaire)
                    .HasForeignKey(d => d.ClientCompanyComplianceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ComplianceQuestionnaire_ClientCompanyCompliance");

                entity.HasOne(d => d.ComplianceQuestionnaireAnswer)
                    .WithMany(p => p.ComplianceQuestionnaire)
                    .HasForeignKey(d => d.ComplianceQuestionnaireAnswerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ComplianceQuestionnaire_ComplianceQuestionnaireAnswer");

                entity.HasOne(d => d.ComplianceQuestionnaireQuestion)
                    .WithMany(p => p.ComplianceQuestionnaire)
                    .HasForeignKey(d => d.ComplianceQuestionnaireQuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ComplianceQuestionnaire_ComplianceQuestionnaireQuestion");

                entity.HasOne(d => d.UpdatedByAuthUser)
                    .WithMany(p => p.ComplianceQuestionnaire)
                    .HasForeignKey(d => d.UpdatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ComplianceQuestionnaire_AuthUser");
            });

            modelBuilder.Entity<ComplianceQuestionnaireAnswer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ComplianceQuestionnaireQuestionId).HasColumnName("ComplianceQuestionnaireQuestionID");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.ComplianceQuestionnaireQuestion)
                    .WithMany(p => p.ComplianceQuestionnaireAnswer)
                    .HasForeignKey(d => d.ComplianceQuestionnaireQuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ComplianceQuestionnaireAnswer_ComplianceQuestionnaireQuestion");
            });

            modelBuilder.Entity<ComplianceQuestionnaireQuestion>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ComplianceReason>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Sequence).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<ComplianceTradeReason>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ContactCategory>(entity =>
            {
                entity.HasIndex(e => e.Description)
                    .HasName("IX_ContactCategory")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CodeIso2)
                    .IsRequired()
                    .HasColumnName("CodeISO2")
                    .HasMaxLength(2);

                entity.Property(e => e.CodeIso3)
                    .IsRequired()
                    .HasColumnName("CodeISO3")
                    .HasMaxLength(3);

                entity.Property(e => e.CodeIso3numeric).HasColumnName("CodeISO3Numeric");

                entity.Property(e => e.CountryGroupId).HasColumnName("CountryGroupID");

                entity.Property(e => e.FormalName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.IsEea).HasColumnName("IsEEA");

                entity.Property(e => e.LengthIban).HasColumnName("LengthIBAN");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PhoneCode).HasMaxLength(25);

                entity.Property(e => e.RegexBban)
                    .HasColumnName("RegexBBAN")
                    .HasMaxLength(250);

                entity.HasOne(d => d.CountryGroup)
                    .WithMany(p => p.Country)
                    .HasForeignKey(d => d.CountryGroupId)
                    .HasConstraintName("FK_Country_CountryGroup");
            });

            modelBuilder.Entity<CountryClearingCodePrefix>(entity =>
            {
                entity.HasKey(e => new { e.CountryId, e.ClearingCodePrefixId });

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.ClearingCodePrefixId).HasColumnName("ClearingCodePrefixID");

                entity.HasOne(d => d.ClearingCodePrefix)
                    .WithMany(p => p.CountryClearingCodePrefix)
                    .HasForeignKey(d => d.ClearingCodePrefixId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CountryClearingCodePrefix_ClearingCodePrefix");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryClearingCodePrefix)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CountryClearingCodePrefix_Country");
            });

            modelBuilder.Entity<CountryGroup>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SwiftAmountFormat)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('####.00')");

                entity.Property(e => e.UpdateDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.HasOne(d => d.CreatedByAuthUser)
                    .WithMany(p => p.CurrencyCreatedByAuthUser)
                    .HasForeignKey(d => d.CreatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Currency_CreatedByAuthUserId");

                entity.HasOne(d => d.UpdatedByAuthUser)
                    .WithMany(p => p.CurrencyUpdatedByAuthUser)
                    .HasForeignKey(d => d.UpdatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Currency_UpdatedByAuthUserId");
            });

            modelBuilder.Entity<CurrencyFxrate>(entity =>
            {
                entity.HasKey(e => new { e.LhsCcyid, e.RhsCcyid });

                entity.ToTable("CurrencyFXRate");

                entity.Property(e => e.LhsCcyid).HasColumnName("lhsCCYID");

                entity.Property(e => e.RhsCcyid).HasColumnName("rhsCCYID");

                entity.Property(e => e.Rate)
                    .HasColumnName("rate")
                    .HasColumnType("decimal(25, 8)");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<CurrencyPairPricing>(entity =>
            {
                entity.ToTable("viewCurrencyPairPricing");
                entity.HasKey(e => e.CurrencyPair);
                entity.Property(e => e.Rate).HasColumnName("Rate");
                entity.Property(e => e.RateTimeStamp).HasColumnName("RateTimeStamp");
                entity.Property(e => e.FeedTimeStamp).HasColumnName("FeedTimeStamp");
                entity.Property(e => e.RateCurrencyPair).HasColumnName("RateCurrecyPair");
            });

            modelBuilder.Entity<CurrencyPairPriceHistory>(entity =>
            {
                entity.HasKey(e => new { e.CurrencyPair, e.PriceDate });

                entity.Property(e => e.CurrencyPair)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.PriceDate).HasColumnType("date");

                entity.Property(e => e.Price).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<CurrencyPairValidation>(entity =>
            {
                entity.HasIndex(e => e.CurrencyPair)
                    .HasName("UQ__Currency__FA4F09C27CDB6CCB")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CurrencyPair)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Emirreport>(entity =>
            {
                entity.ToTable("EMIRReport");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.EmirreportOutgoingFileId).HasColumnName("EMIRReportOutgoingFileID");

                entity.HasOne(d => d.EmirreportOutgoingFile)
                    .WithMany(p => p.Emirreport)
                    .HasForeignKey(d => d.EmirreportOutgoingFileId)
                    .HasConstraintName("FK_EMIRReport_EMIRReportOutgoingFile");
            });

            modelBuilder.Entity<EmirreportField>(entity =>
            {
                entity.ToTable("EMIRReportField");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppSettingKey).HasMaxLength(50);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.EmirreportTypeId).HasColumnName("EMIRReportTypeID");

                entity.Property(e => e.FieldCode)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FieldName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FieldValue).HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.EmirreportType)
                    .WithMany(p => p.EmirreportField)
                    .HasForeignKey(d => d.EmirreportTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMIRReportField_EMIRReportType");
            });

            modelBuilder.Entity<EmirreportFxforwardTrade>(entity =>
            {
                entity.ToTable("EMIRReportFXForwardTrade");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EmirreportId).HasColumnName("EMIRReportID");

                entity.Property(e => e.EmirreportTypeId).HasColumnName("EMIRReportTypeID");

                entity.Property(e => e.EmirstatusId).HasColumnName("EMIRStatusID");

                entity.Property(e => e.EmirstatusUpdatedDateTime)
                    .HasColumnName("EMIRStatusUpdatedDateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.FxforwardTradeCode)
                    .IsRequired()
                    .HasColumnName("FXForwardTradeCode")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Emirreport)
                    .WithMany(p => p.EmirreportFxforwardTrade)
                    .HasForeignKey(d => d.EmirreportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMIRReportFxForwardTrade_EMIRReport");

                entity.HasOne(d => d.EmirreportType)
                    .WithMany(p => p.EmirreportFxforwardTrade)
                    .HasForeignKey(d => d.EmirreportTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMIRReportFxForwardTrade_EMIRReportType");

                entity.HasOne(d => d.Emirstatus)
                    .WithMany(p => p.EmirreportFxforwardTrade)
                    .HasForeignKey(d => d.EmirstatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMIRReportFxForwardTrade_EMIRStatus");

                entity.HasOne(d => d.FxforwardTradeCodeNavigation)
                    .WithMany(p => p.EmirreportFxforwardTrade)
                    .HasForeignKey(d => d.FxforwardTradeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMIRReportFxForwardTrade_FXForwardTrade");
            });

            modelBuilder.Entity<EmirreportIncomingFile>(entity =>
            {
                entity.ToTable("EMIRReportIncomingFile");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.EmirreportId).HasColumnName("EMIRReportID");

                entity.Property(e => e.EmirreportIncomingFileContentId).HasColumnName("EMIRReportIncomingFileContentID");

                entity.Property(e => e.Xmlfilename)
                    .HasColumnName("XMLFilename")
                    .HasMaxLength(255);

                entity.Property(e => e.Zipfilename)
                    .IsRequired()
                    .HasColumnName("ZIPFilename")
                    .HasMaxLength(255);

                entity.HasOne(d => d.Emirreport)
                    .WithMany(p => p.EmirreportIncomingFile)
                    .HasForeignKey(d => d.EmirreportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMIRReportIncomingFile_EMIRReport");

                entity.HasOne(d => d.EmirreportIncomingFileContent)
                    .WithMany(p => p.EmirreportIncomingFile)
                    .HasForeignKey(d => d.EmirreportIncomingFileContentId)
                    .HasConstraintName("FK_EMIRReportIncomingFile_EMIRReportIncomingFileContent");
            });

            modelBuilder.Entity<EmirreportIncomingFileContent>(entity =>
            {
                entity.ToTable("EMIRReportIncomingFileContent");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FileContent).IsRequired();
            });

            modelBuilder.Entity<EmirreportOutgoingFile>(entity =>
            {
                entity.ToTable("EMIRReportOutgoingFile");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EmirreportOutgoingFileContentId).HasColumnName("EMIRReportOutgoingFileContentID");

                entity.Property(e => e.UploadedDateTime).HasColumnType("datetime");

                entity.Property(e => e.UploadedFilename).HasMaxLength(255);

                entity.Property(e => e.Xmlfilename)
                    .IsRequired()
                    .HasColumnName("XMLFilename")
                    .HasMaxLength(255);

                entity.HasOne(d => d.EmirreportOutgoingFileContent)
                    .WithMany(p => p.EmirreportOutgoingFile)
                    .HasForeignKey(d => d.EmirreportOutgoingFileContentId)
                    .HasConstraintName("FK_EMIRReportOutgoingFile_EMIRReportOutgoingFileContent");
            });

            modelBuilder.Entity<EmirreportOutgoingFileContent>(entity =>
            {
                entity.ToTable("EMIRReportOutgoingFileContent");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FileContent).IsRequired();
            });

            modelBuilder.Entity<EmirreportResponseCode>(entity =>
            {
                entity.ToTable("EMIRReportResponseCode");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ErrorMessage)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EmirreportTradeResponseError>(entity =>
            {
                entity.ToTable("EMIRReportTradeResponseError");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EmirreportFxforwardTradeId).HasColumnName("EMIRReportFXForwardTradeID");

                entity.Property(e => e.EmirreportResponseCodeId).HasColumnName("EMIRReportResponseCodeID");

                entity.Property(e => e.ResponseMessage).HasMaxLength(500);

                entity.Property(e => e.Source)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.EmirreportFxforwardTrade)
                    .WithMany(p => p.EmirreportTradeResponseError)
                    .HasForeignKey(d => d.EmirreportFxforwardTradeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMIRReportTradeResponseError_EMIRReportFXForwardTrade");

                entity.HasOne(d => d.EmirreportResponseCode)
                    .WithMany(p => p.EmirreportTradeResponseError)
                    .HasForeignKey(d => d.EmirreportResponseCodeId)
                    .HasConstraintName("FK_EMIRReportTradeResponseError_EMIRReportResponseCode");
            });

            modelBuilder.Entity<EmirreportType>(entity =>
            {
                entity.ToTable("EMIRReportType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Emirstatus>(entity =>
            {
                entity.ToTable("EMIRStatus");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ExpectedFrequency>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Value).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<FixApareportField>(entity =>
            {
                entity.ToTable("FixAPAReportField");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppSettingKey).HasMaxLength(250);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Value).HasMaxLength(100);
            });

            modelBuilder.Entity<FixApatradeCapture>(entity =>
            {
                entity.ToTable("FixAPATradeCapture");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApastatusId).HasColumnName("APAStatusID");

                entity.Property(e => e.ApastatusUpdatedDateTime)
                    .HasColumnName("APAStatusUpdatedDateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.AuthUserId).HasColumnName("AuthUserID");

                entity.Property(e => e.BloombergTradeId)
                    .HasColumnName("BloombergTradeID")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ErrorMessage)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PublishDateTime).HasColumnType("datetime");

                entity.Property(e => e.RejectReason)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.TradeCode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TradeReportId)
                    .HasColumnName("TradeReportID")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.Apastatus)
                    .WithMany(p => p.FixApatradeCapture)
                    .HasForeignKey(d => d.ApastatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FixAPATradeCapture_APAStatus");

                entity.HasOne(d => d.AuthUser)
                    .WithMany(p => p.FixApatradeCapture)
                    .HasForeignKey(d => d.AuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FixAPATradeCapture_AuthUser");

                entity.HasOne(d => d.TradeCodeNavigation)
                    .WithMany(p => p.FixApatradeCapture)
                    .HasForeignKey(d => d.TradeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FixAPATradeCapture_FxForwardTrade");
            });

            modelBuilder.Entity<FixApatradeMessage>(entity =>
            {
                entity.ToTable("FixAPATradeMessage");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FixMessage)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.MessageDate).HasColumnType("datetime");

                entity.Property(e => e.TradeCode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.TradeCodeNavigation)
                    .WithMany(p => p.FixApatradeMessage)
                    .HasForeignKey(d => d.TradeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FixAPATradeMessage_FxForwardTrade");
            });

            modelBuilder.Entity<FixFxforwardTradeOrder>(entity =>
            {
                entity.ToTable("FixFXForwardTradeOrder");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BarclaysAssignedId)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.BarclaysTradeId)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ErrorMessage)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.FxforwardCode)
                    .IsRequired()
                    .HasColumnName("FXForwardCode")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.RejectReason)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.FxforwardCodeNavigation)
                    .WithMany(p => p.FixFxforwardTradeOrder)
                    .HasForeignKey(d => d.FxforwardCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FixTradeOrder_FXForwardTrade");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FixFxforwardTradeOrder)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FixTradeOrder_AuthUser");
            });

            modelBuilder.Entity<FixQuote>(entity =>
            {
                entity.HasKey(e => e.QuoteId);

                entity.Property(e => e.QuoteId)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.TradeId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);                
            });

            modelBuilder.Entity<FixQuoteCancelled>(entity =>
            {
                entity.HasKey(e => e.QuoteId);

                entity.Property(e => e.QuoteId)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<FixTradeMessage>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FixMessage)
                    .IsRequired()
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.MessageDate).HasColumnType("datetime");

                entity.Property(e => e.TradeCode)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FxforwardTrade>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("FXForwardTrade");

                entity.HasIndex(e => new { e.ClientCompanyId, e.Deleted, e.ContractDate })
                    .HasName("_dta_index_FXForwardTrade_52_562101043__K7_K31_K13");

                entity.Property(e => e.Code)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ApastatusId).HasColumnName("APAStatusID");

                entity.Property(e => e.ApastatusUpdatedDateTime)
                    .HasColumnName("APAStatusUpdatedDateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.Armreported).HasColumnName("ARMReported");

                entity.Property(e => e.ArmstatusId).HasColumnName("ARMStatusID");

                entity.Property(e => e.ArmstatusUpdatedDateTime)
                    .HasColumnName("ARMStatusUpdatedDateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.AuthorisedByClientCompanyContactId).HasColumnName("AuthorisedByClientCompanyContactID");

                entity.Property(e => e.BdpforwardPoints)
                    .HasColumnName("BDPForwardPoints")
                    .HasColumnType("decimal(25, 8)");

                entity.Property(e => e.BrokenDatePrice).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.BrokerId).HasColumnName("BrokerID");

                entity.Property(e => e.BrokerLhsamt)
                    .HasColumnName("BrokerLHSAmt")
                    .HasColumnType("decimal(25, 8)");

                entity.Property(e => e.BrokerRate).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.BrokerRhsamt)
                    .HasColumnName("BrokerRHSAmt")
                    .HasColumnType("decimal(25, 8)");

                entity.Property(e => e.BrokeredDate).HasColumnType("datetime");

                entity.Property(e => e.ClientCompanyId).HasColumnName("ClientCompanyID");

                entity.Property(e => e.ClientCompanyOpiid).HasColumnName("ClientCompanyOPIID");

                entity.Property(e => e.ClientLhsamt)
                    .HasColumnName("ClientLHSAmt")
                    .HasColumnType("decimal(25, 8)");

                entity.Property(e => e.ClientRate).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.ClientRhsamt)
                    .HasColumnName("ClientRHSAmt")
                    .HasColumnType("decimal(25, 8)");

                entity.Property(e => e.CollateralPerc).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.CommPaidOutDate).HasColumnType("date");

                entity.Property(e => e.ComplianceIsin)
                    .HasColumnName("Compliance_ISIN")
                    .HasMaxLength(12);

                entity.Property(e => e.ComplianceTradeReasonId).HasColumnName("ComplianceTradeReasonID");

                entity.Property(e => e.ContractDate).HasColumnType("date");

                entity.Property(e => e.ContractNoteSentToClientDateTime).HasColumnType("datetime");

                entity.Property(e => e.ContractNoteSentToMyselfDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedByAuthUserId).HasColumnName("CreatedByAuthUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrencyPair)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveredDate).HasColumnType("datetime");

                entity.Property(e => e.EmirDelegatedReported).HasColumnName("EMIR_DelegatedReported");

                entity.Property(e => e.EmirReported).HasColumnName("EMIR_Reported");

                entity.Property(e => e.EmirReportedDateTime)
                    .HasColumnName("EMIR_ReportedDateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.EmirUti)
                    .HasColumnName("EMIR_UTI")
                    .HasMaxLength(100);

                entity.Property(e => e.EmirdelegatedSubmissionId)
                    .HasColumnName("EMIRDelegatedSubmissionID")
                    .HasMaxLength(50);

                entity.Property(e => e.EmirstatusId).HasColumnName("EMIRStatusID");

                entity.Property(e => e.EmirstatusUpdatedDateTime)
                    .HasColumnName("EMIRStatusUpdatedDateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.EmirsubmissionId)
                    .HasColumnName("EMIRSubmissionID")
                    .HasMaxLength(50);

                entity.Property(e => e.FilledDateTime).HasColumnType("datetime");

                entity.Property(e => e.FxforwardTradeStatusId).HasColumnName("FXForwardTradeStatusID");

                entity.Property(e => e.IsApareportable)
                    .IsRequired()
                    .HasColumnName("IsAPAReportable")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsArmreportable)
                    .IsRequired()
                    .HasColumnName("IsARMReportable")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsComplianceRegulated)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsComplianceSupported)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsEmirreportable)
                    .IsRequired()
                    .HasColumnName("IsEMIRReportable")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsOrder).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsRhsmajor).HasColumnName("IsRHSMajor");

                entity.Property(e => e.Lhsccyid).HasColumnName("LHSCCYID");

                entity.Property(e => e.MarkToMarketValue).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.MarkToMarketValueUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.MarketSideUti)
                    .HasColumnName("MarketSideUTI")
                    .HasMaxLength(100);

                entity.Property(e => e.OpenValueDate).HasColumnType("date");

                entity.Property(e => e.OpiupdatedByAuthUserId).HasColumnName("OPIUpdatedByAuthUserId");

                entity.Property(e => e.OpiupdatedDateTime)
                    .HasColumnName("OPIUpdatedDateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.PrevDayMarktoMarket).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.PrevDayMarktoMarketUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.PrevailingRate2).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.Profit).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.ProfitConsolidatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.ProfitConsolidatedValue).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.ProfitGbprate)
                    .HasColumnName("ProfitGBPRate")
                    .HasColumnType("decimal(25, 8)");

                entity.Property(e => e.Reference).HasMaxLength(20);

                entity.Property(e => e.RemainingClientLhsamt)
                    .HasColumnName("RemainingClientLHSAmt")
                    .HasColumnType("decimal(25, 8)");

                entity.Property(e => e.RemainingClientRhsamt)
                    .HasColumnName("RemainingClientRHSAmt")
                    .HasColumnType("decimal(25, 8)");

                entity.Property(e => e.Rhsccyid).HasColumnName("RHSCCYID");

                entity.Property(e => e.SettledDate).HasColumnType("datetime");

                entity.Property(e => e.TradeInstructionMethodId).HasColumnName("TradeInstructionMethodID");

                entity.Property(e => e.TransactionCommitId).HasColumnName("TransactionCommitID");

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ValueDate).HasColumnType("date");

                entity.Property(e => e.VerifiedByAuthUserId).HasColumnName("VerifiedByAuthUserID");

                entity.HasOne(d => d.Apastatus)
                    .WithMany(p => p.FxforwardTradeApastatus)
                    .HasForeignKey(d => d.ApastatusId)
                    .HasConstraintName("FK_FXForwardTrade_APAStatus");

                entity.HasOne(d => d.Armstatus)
                    .WithMany(p => p.FxforwardTradeArmstatus)
                    .HasForeignKey(d => d.ArmstatusId)
                    .HasConstraintName("FK_FXForwardTrade_ARMStatus");

                entity.HasOne(d => d.AuthorisedByClientCompanyContact)
                    .WithMany(p => p.FxforwardTrade)
                    .HasForeignKey(d => d.AuthorisedByClientCompanyContactId)
                    .HasConstraintName("FK_FXForwardTrade_ClientCompanyContact");

                entity.HasOne(d => d.Broker)
                    .WithMany(p => p.FxforwardTrade)
                    .HasForeignKey(d => d.BrokerId)
                    .HasConstraintName("FK_FXForwardTrade_Broker");

                entity.HasOne(d => d.BrokeredByAuthUser)
                    .WithMany(p => p.FxforwardTradeBrokeredByAuthUser)
                    .HasForeignKey(d => d.BrokeredByAuthUserId)
                    .HasConstraintName("FK_FXForwardTrade_BrokeredByAuthUser");

                entity.HasOne(d => d.ClientCompanyNavigation)
                    .WithMany(p => p.FxforwardTrade)
                    .HasForeignKey(d => d.ClientCompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FXForwardTrade_ClientCompany");

                entity.HasOne(d => d.ClientCompanyOpi)
                    .WithMany(p => p.FxforwardTrade)
                    .HasForeignKey(d => d.ClientCompanyOpiid)
                    .HasConstraintName("FK_FXForwardTrade_ClientCompanyOPI");

                entity.HasOne(d => d.ComplianceTradeReason)
                    .WithMany(p => p.FxforwardTrade)
                    .HasForeignKey(d => d.ComplianceTradeReasonId)
                    .HasConstraintName("FK_FXForwardTrade_ComplianceTradeReason");

                entity.HasOne(d => d.CreatedByAuthUser)
                    .WithMany(p => p.FxforwardTradeCreatedByAuthUser)
                    .HasForeignKey(d => d.CreatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FXForwardTrade_AuthUser");

                entity.HasOne(d => d.Emirstatus)
                    .WithMany(p => p.FxforwardTradeEmirstatus)
                    .HasForeignKey(d => d.EmirstatusId)
                    .HasConstraintName("FK_FXForwardTrade_EMIRStatus");

                entity.HasOne(d => d.FilledByAuthUser)
                    .WithMany(p => p.FxforwardTradeFilledByAuthUser)
                    .HasForeignKey(d => d.FilledByAuthUserId)
                    .HasConstraintName("FK_FXForwardTrade_FilledByAuthUser");

                entity.HasOne(d => d.FxforwardTradeStatus)
                    .WithMany(p => p.FxforwardTrade)
                    .HasForeignKey(d => d.FxforwardTradeStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FXForwardTrade_FXForwardTradeStatus");

                entity.HasOne(d => d.Lhsccy)
                    .WithMany(p => p.FxforwardTradeLhsccy)
                    .HasForeignKey(d => d.Lhsccyid)
                    .HasConstraintName("FK_FXForwardTrade_Currency");

                entity.HasOne(d => d.OpiupdatedByAuthUser)
                    .WithMany(p => p.FxforwardTradeOpiupdatedByAuthUser)
                    .HasForeignKey(d => d.OpiupdatedByAuthUserId)
                    .HasConstraintName("FK_FXForwardTrade_OPIUpdatedByAuthUser");

                entity.HasOne(d => d.Rhsccy)
                    .WithMany(p => p.FxforwardTradeRhsccy)
                    .HasForeignKey(d => d.Rhsccyid)
                    .HasConstraintName("FK_FXForwardTrade_Currency1");

                entity.HasOne(d => d.TradeInstructionMethod)
                    .WithMany(p => p.FxforwardTrade)
                    .HasForeignKey(d => d.TradeInstructionMethodId)
                    .HasConstraintName("FK_FXForwardTrade_TradeInstructionMethod");

                entity.HasOne(d => d.TransactionCommit)
                    .WithMany(p => p.FxforwardTrade)
                    .HasForeignKey(d => d.TransactionCommitId)
                    .HasConstraintName("FK_FXForwardTrade_TransactionCommit");

                entity.HasOne(d => d.UpdatedByAuthUser)
                    .WithMany(p => p.FxforwardTradeUpdatedByAuthUser)
                    .HasForeignKey(d => d.UpdatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FXForwardTrade_AuthUser1");

                entity.HasOne(d => d.VerifiedByAuthUser)
                    .WithMany(p => p.FxforwardTradeVerifiedByAuthUser)
                    .HasForeignKey(d => d.VerifiedByAuthUserId)
                    .HasConstraintName("FK_FXForwardTrade_AuthUser2");
            });

            modelBuilder.Entity<FxforwardTrade2Opi>(entity =>
            {
                entity.ToTable("FXForwardTrade2OPI");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.ClientCompanyOpiid).HasColumnName("ClientCompanyOPIID");

                entity.Property(e => e.CreatedByAuthUserId).HasColumnName("CreatedByAuthUserID");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Details).HasMaxLength(100);

                entity.Property(e => e.FxforwardTradeCode)
                    .IsRequired()
                    .HasColumnName("FXForwardTradeCode")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TradeValueDate).HasColumnType("datetime");

                entity.HasOne(d => d.ClientCompanyOpi)
                    .WithMany(p => p.FxforwardTrade2Opi)
                    .HasForeignKey(d => d.ClientCompanyOpiid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FXForwardTrade2OPI_ClientCompanyOPI");

                entity.HasOne(d => d.CreatedByAuthUser)
                    .WithMany(p => p.FxforwardTrade2Opi)
                    .HasForeignKey(d => d.CreatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FXForwardTrade2OPI_AuthUser");

                entity.HasOne(d => d.FxforwardTradeCodeNavigation)
                    .WithMany(p => p.FxforwardTrade2Opi)
                    .HasForeignKey(d => d.FxforwardTradeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FXForwardTrade2OPI_FXForwardTrade");
            });

            modelBuilder.Entity<FxforwardTradeInvoice>(entity =>
            {
                entity.ToTable("FXForwardTradeInvoice");

                entity.HasIndex(e => new { e.TradeCode, e.FileName })
                    .HasName("IDX_TradeCodeFilename")
                    .IsUnique()
                    .HasFilter("([FileName] IS NOT NULL)");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Comment).HasMaxLength(500);

                entity.Property(e => e.DocumentId).HasColumnName("DocumentID");

                entity.Property(e => e.FileName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.TradeCode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UploadedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.InverseIdNavigation)
                    .HasForeignKey<FxforwardTradeInvoice>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FXForwardTradeInvoice_FXForwardTradeInvoice");

                entity.HasOne(d => d.TradeCodeNavigation)
                    .WithMany(p => p.FxforwardTradeInvoice)
                    .HasForeignKey(d => d.TradeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FXForwardTradeInvoice_FXForwardTrade");

                entity.HasOne(d => d.UpdatedByAuthUser)
                    .WithMany(p => p.FxforwardTradeInvoice)
                    .HasForeignKey(d => d.UpdatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FXForwardTradeInvoice_AuthUser");
            });

            modelBuilder.Entity<FxforwardTradeStatus>(entity =>
            {
                entity.ToTable("FXForwardTradeStatus");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FxforwardTradeSwapCount>(entity =>
            {
                entity.HasKey(e => e.FxforwardTradeCode);

                entity.ToTable("FXForwardTradeSwapCount");

                entity.Property(e => e.FxforwardTradeCode)
                    .HasColumnName("FXForwardTradeCode")
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.SwapCount).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Fxoption>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("FXOption");

                entity.Property(e => e.Code)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AuthorisedByClientCompanyContactId).HasColumnName("AuthorisedByClientCompanyContactID");

                entity.Property(e => e.Barrier).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.BestCaseRate).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.BrokerId).HasColumnName("BrokerID");

                entity.Property(e => e.ClientCompanyId).HasColumnName("ClientCompanyID");

                entity.Property(e => e.ClientCompanyOpiid).HasColumnName("ClientCompanyOPIID");

                entity.Property(e => e.ClientLhsamt)
                    .HasColumnName("ClientLHSAmt")
                    .HasColumnType("decimal(25, 8)");

                entity.Property(e => e.ClientLhsamtNotional)
                    .HasColumnName("ClientLHSAmtNotional")
                    .HasColumnType("decimal(25, 8)");

                entity.Property(e => e.ClientRhsamt)
                    .HasColumnName("ClientRHSAmt")
                    .HasColumnType("decimal(25, 8)");

                entity.Property(e => e.ClientRhsamtNotional)
                    .HasColumnName("ClientRHSAmtNotional")
                    .HasColumnType("decimal(25, 8)");

                entity.Property(e => e.CommPaidOutDate).HasColumnType("date");

                entity.Property(e => e.ContractDate).HasColumnType("date");

                entity.Property(e => e.CreatedByAuthUserId).HasColumnName("CreatedByAuthUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrencyPair)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.DeliveredDate).HasColumnType("datetime");

                entity.Property(e => e.ExpiryDate).HasColumnType("date");

                entity.Property(e => e.ExtBarrier).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.ExtBarrierDate).HasColumnType("datetime");

                entity.Property(e => e.ExtStrike).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.ExtValueDate).HasColumnType("datetime");

                entity.Property(e => e.ForwardRate).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.FxoptionSettlementId).HasColumnName("FXOptionSettlementID");

                entity.Property(e => e.FxoptionStatusId).HasColumnName("FXOptionStatusID");

                entity.Property(e => e.FxoptionTypeId).HasColumnName("FXOptionTypeID");

                entity.Property(e => e.GraphImgTemplateFile).HasMaxLength(150);

                entity.Property(e => e.IsBuy).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsExpired).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsExtended).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsKnockedIn).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsKnockedOut).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsLeveraged).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsRhsmajour).HasColumnName("IsRHSMajour");

                entity.Property(e => e.KnockInRate).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.KnockOutRate).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.LevBarrier).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.LevBarrierDate).HasColumnType("datetime");

                entity.Property(e => e.LevNotional).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.LevStrike).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.LevValueDate).HasColumnType("datetime");

                entity.Property(e => e.Lhsccyid).HasColumnName("LHSCCYID");

                entity.Property(e => e.OptionTrigger).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.OptionTriggerProtecLvl).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.ParentCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PercentagePart).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.Premium).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.Profit).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.ProtectedLevel).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.Rhsccyid).HasColumnName("RHSCCYID");

                entity.Property(e => e.SettledDate).HasColumnType("datetime");

                entity.Property(e => e.SettlementTradeId).HasColumnName("SettlementTradeID");

                entity.Property(e => e.TradeInstructionMethodId).HasColumnName("TradeInstructionMethodID");

                entity.Property(e => e.TransactionCommitId).HasColumnName("TransactionCommitID");

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ValueDate).HasColumnType("date");

                entity.Property(e => e.Verified).HasDefaultValueSql("((0))");

                entity.Property(e => e.VerifiedByAuthUserId).HasColumnName("VerifiedByAuthUserID");

                entity.Property(e => e.WorstCaseRate).HasColumnType("decimal(25, 8)");

                entity.HasOne(d => d.AuthorisedByClientCompanyContact)
                    .WithMany(p => p.Fxoption)
                    .HasForeignKey(d => d.AuthorisedByClientCompanyContactId)
                    .HasConstraintName("FK_FXOption_ClientCompanyContact");

                entity.HasOne(d => d.Broker)
                    .WithMany(p => p.Fxoption)
                    .HasForeignKey(d => d.BrokerId)
                    .HasConstraintName("FK_FXOption_Broker");

                entity.HasOne(d => d.ClientCompany)
                    .WithMany(p => p.Fxoption)
                    .HasForeignKey(d => d.ClientCompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FXOption_ClientCompany");

                entity.HasOne(d => d.ClientCompanyOpi)
                    .WithMany(p => p.Fxoption)
                    .HasForeignKey(d => d.ClientCompanyOpiid)
                    .HasConstraintName("FK_FXOption_ClientCompanyOPI");

                entity.HasOne(d => d.CreatedByAuthUser)
                    .WithMany(p => p.FxoptionCreatedByAuthUser)
                    .HasForeignKey(d => d.CreatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FXOption_AuthUser");

                entity.HasOne(d => d.FxoptionStatus)
                    .WithMany(p => p.Fxoption)
                    .HasForeignKey(d => d.FxoptionStatusId)
                    .HasConstraintName("FK_FXOption_FXOptionStatus");

                entity.HasOne(d => d.Lhsccy)
                    .WithMany(p => p.FxoptionLhsccy)
                    .HasForeignKey(d => d.Lhsccyid)
                    .HasConstraintName("FK_FXOption_Currency");

                entity.HasOne(d => d.Rhsccy)
                    .WithMany(p => p.FxoptionRhsccy)
                    .HasForeignKey(d => d.Rhsccyid)
                    .HasConstraintName("FK_FXOption_Currency1");

                entity.HasOne(d => d.TradeInstructionMethod)
                    .WithMany(p => p.Fxoption)
                    .HasForeignKey(d => d.TradeInstructionMethodId)
                    .HasConstraintName("FK_FXOption_TradeInstructionMethod");

                entity.HasOne(d => d.TransactionCommit)
                    .WithMany(p => p.Fxoption)
                    .HasForeignKey(d => d.TransactionCommitId)
                    .HasConstraintName("FK_FXOption_TransactionCommit");

                entity.HasOne(d => d.UpdatedByAuthUser)
                    .WithMany(p => p.FxoptionUpdatedByAuthUser)
                    .HasForeignKey(d => d.UpdatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FXOption_AuthUser1");

                entity.HasOne(d => d.VerifiedByAuthUser)
                    .WithMany(p => p.FxoptionVerifiedByAuthUser)
                    .HasForeignKey(d => d.VerifiedByAuthUserId)
                    .HasConstraintName("FK_FXOption_AuthUser2");
            });

            modelBuilder.Entity<FxoptionOutputs>(entity =>
            {
                entity.ToTable("FXOptionOutputs");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AuthUserId).HasColumnName("AuthUserID");

                entity.Property(e => e.ExternalTradeCode).HasMaxLength(50);

                entity.Property(e => e.FxoptionCode)
                    .HasColumnName("FXOptionCode")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FxoptionOutputsTemplateId).HasColumnName("FXOptionOutputsTemplateID");

                entity.Property(e => e.Outputs).HasMaxLength(150);
            });

            modelBuilder.Entity<FxoptionOutputsTemplate>(entity =>
            {
                entity.ToTable("FXOptionOutputsTemplate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FxoptionTypeId).HasColumnName("FXOptionTypeID");

                entity.Property(e => e.Template).HasMaxLength(150);
            });

            modelBuilder.Entity<FxoptionSettlements>(entity =>
            {
                entity.ToTable("FXOptionSettlements");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AuthorisedByClientCompanyContactId).HasColumnName("AuthorisedByClientCompanyContactID");

                entity.Property(e => e.BrokerId).HasColumnName("BrokerID");

                entity.Property(e => e.ClientCompanyId).HasColumnName("ClientCompanyID");

                entity.Property(e => e.ClientLhsamt)
                    .HasColumnName("ClientLHSAmt")
                    .HasColumnType("decimal(25, 8)");

                entity.Property(e => e.ClientRate).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.ClientRhsamt)
                    .HasColumnName("ClientRHSAmt")
                    .HasColumnType("decimal(25, 8)");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.ContractDate).HasColumnType("date");

                entity.Property(e => e.CreatedByAuthUserId).HasColumnName("CreatedByAuthUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrencyPair)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(150);

                entity.Property(e => e.FxoptionCode)
                    .HasColumnName("FXOptionCode")
                    .HasMaxLength(50);

                entity.Property(e => e.FxoptionSettlementsTemplateId).HasColumnName("FXOptionSettlementsTemplateID");

                entity.Property(e => e.IsRhsmajour).HasColumnName("IsRHSMajour");

                entity.Property(e => e.IsSettled).HasDefaultValueSql("((0))");

                entity.Property(e => e.Lhsccyid).HasColumnName("LHSCCYID");

                entity.Property(e => e.Notional).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.Rhsccyid).HasColumnName("RHSCCYID");

                entity.Property(e => e.TradeInstructionMethodId).HasColumnName("TradeInstructionMethodID");

                entity.Property(e => e.ValueDate).HasColumnType("date");
            });

            modelBuilder.Entity<FxoptionSettlementsTemplate>(entity =>
            {
                entity.ToTable("FXOptionSettlementsTemplate");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientRate).HasMaxLength(50);

                entity.Property(e => e.FxoptionTypeId).HasColumnName("FXOptionTypeID");

                entity.Property(e => e.Notional).HasMaxLength(50);

                entity.Property(e => e.Template).HasMaxLength(150);

                entity.Property(e => e.TradeCodeSuffix).HasMaxLength(10);
            });

            modelBuilder.Entity<FxoptionStatus>(entity =>
            {
                entity.ToTable("FXOptionStatus");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FxoptionType>(entity =>
            {
                entity.ToTable("FXOptionType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExtOptionTypeId).HasColumnName("ExtOptionTypeID");

                entity.Property(e => e.LevOptionTypeId).HasColumnName("LevOptionTypeID");

                entity.Property(e => e.TermSheetImg)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.VisibleInputs)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Fxswap>(entity =>
            {
                entity.ToTable("FXSwap");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedAuthUserId).HasColumnName("CreatedAuthUserID");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeliveryLegTradeCode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ParentTradeCode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ReversalLegTradeCode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.HasOne(d => d.CreatedAuthUser)
                    .WithMany(p => p.Fxswap)
                    .HasForeignKey(d => d.CreatedAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FXSwap_CreatedAuthUser");

                entity.HasOne(d => d.DeliveryLegTradeCodeNavigation)
                    .WithMany(p => p.FxswapDeliveryLegTradeCodeNavigation)
                    .HasForeignKey(d => d.DeliveryLegTradeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FXSwap_DeliveryLegFXForwardTrade");

                entity.HasOne(d => d.ParentTradeCodeNavigation)
                    .WithMany(p => p.FxswapParentTradeCodeNavigation)
                    .HasForeignKey(d => d.ParentTradeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FXSwap_ParentFXForwardTrade");

                entity.HasOne(d => d.ReversalLegTradeCodeNavigation)
                    .WithMany(p => p.FxswapReversalLegTradeCodeNavigation)
                    .HasForeignKey(d => d.ReversalLegTradeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FXSwap_ReversalLegFXForwardTrade");
            });

            modelBuilder.Entity<GlobalSearchScope>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StoredProcName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<IntroducingBroker>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(101)
                    .HasComputedColumnSql("(([Name]+' ')+isnull([Surname],''))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Surname).HasMaxLength(50);

                entity.Property(e => e.Telephone).HasMaxLength(50);

                entity.Property(e => e.UpdateAuthUserId).HasColumnName("UpdateAuthUserID");

                entity.Property(e => e.UpdateDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();
            });

            modelBuilder.Entity<LastWorkingDay>(entity =>
            {
                entity.HasKey(e => new { e.Year, e.Month });

                entity.Property(e => e.LastWorkingDay1)
                    .HasColumnName("LastWorkingDay")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<LogAuthUser>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.Comment).HasMaxLength(200);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FailedPasswordAttemptWindowStart).HasColumnType("datetime");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LastActivityDate).HasColumnType("datetime");

                entity.Property(e => e.LastLockOutDate).HasColumnType("datetime");

                entity.Property(e => e.LastLoginDate).HasColumnType("datetime");

                entity.Property(e => e.LastPasswordChangeDate).HasColumnType("datetime");

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<LogBankAccountCurrencyDetails>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.BankAccountIban)
                    .HasColumnName("BankAccountIBAN")
                    .HasMaxLength(50);

                entity.Property(e => e.BankAccountId).HasColumnName("BankAccountID");

                entity.Property(e => e.BankAccountName).HasMaxLength(100);

                entity.Property(e => e.BankAccountNumber).HasMaxLength(50);

                entity.Property(e => e.BankAccountSort).HasMaxLength(8);

                entity.Property(e => e.BankAccountSwift).HasMaxLength(11);

                entity.Property(e => e.BankAddress).HasMaxLength(400);

                entity.Property(e => e.BankName).HasMaxLength(100);

                entity.Property(e => e.BeneficiaryAddress).HasMaxLength(400);

                entity.Property(e => e.BeneficiaryName).HasMaxLength(100);

                entity.Property(e => e.ClearingCodePrefixId).HasColumnName("ClearingCodePrefixID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('CREATED')");

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<LogBreach>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.BreachLevelId).HasColumnName("BreachLevelID");

                entity.Property(e => e.BreachTypeId).HasColumnName("BreachTypeID");

                entity.Property(e => e.ClientCompanyOpiid).HasColumnName("ClientCompanyOPIID");

                entity.Property(e => e.CreatedByAuthUserId).HasColumnName("CreatedByAuthUserID");

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Notes).HasMaxLength(500);

                entity.Property(e => e.OriginalLimit).HasMaxLength(250);

                entity.Property(e => e.OverrideValue).HasMaxLength(250);

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.TradeCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<LogBreachInvoice>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.Comment).HasMaxLength(500);

                entity.Property(e => e.DocumentId)
                    .IsRequired()
                    .HasColumnName("DocumentID")
                    .HasMaxLength(100);

                entity.Property(e => e.FileName).HasMaxLength(250);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.UploadedByAuthUserId).HasColumnName("UploadedByAuthUserID");

                entity.Property(e => e.UploadedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<LogCassRecs>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.CassRecsDate).HasColumnType("date");

                entity.Property(e => e.CassRecsStatementFileId).HasColumnName("CassRecsStatementFileID");

                entity.Property(e => e.Check1ByAuthUserId).HasColumnName("Check1ByAuthUserID");

                entity.Property(e => e.Check1UpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Check2ByAuthUserId).HasColumnName("Check2ByAuthUserID");

                entity.Property(e => e.Check2UpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.CompletedByAuthUserId).HasColumnName("CompletedByAuthUserID");

                entity.Property(e => e.CompletedDateTime).HasColumnType("datetime");

                entity.Property(e => e.CurrencyCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LastNightsClosingLedger).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<LogCassRecsPaymentFile>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.CassRecsDate).HasColumnType("date");

                entity.Property(e => e.DocumentId)
                    .IsRequired()
                    .HasColumnName("DocumentID")
                    .HasMaxLength(100);

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.UploadedByAuthUserId).HasColumnName("UploadedByAuthUserID");

                entity.Property(e => e.UploadedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<LogCassRecsStatementFile>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.CassRecsDate).HasColumnType("date");

                entity.Property(e => e.DocumentId)
                    .IsRequired()
                    .HasColumnName("DocumentID")
                    .HasMaxLength(100);

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UploadedByAuthUserId).HasColumnName("UploadedByAuthUserID");

                entity.Property(e => e.UploadedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<LogClientCompanyCompliance>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.AmlriskId).HasColumnName("AMLRiskID");

                entity.Property(e => e.BalanceSheetGbp)
                    .HasColumnName("BalanceSheetGBP")
                    .HasColumnType("decimal(25, 2)");

                entity.Property(e => e.ClassificationId).HasColumnName("ClassificationID");

                entity.Property(e => e.ClientCompanyId).HasColumnName("ClientCompanyID");

                entity.Property(e => e.ExpectedFrequencyId).HasColumnName("ExpectedFrequencyID");

                entity.Property(e => e.ExpectedMaxTradeSize).HasColumnType("decimal(25, 2)");

                entity.Property(e => e.ExpectedTotalVolume).HasColumnType("decimal(25, 2)");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IsMiFid).HasColumnName("IsMiFID");

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NatureId).HasColumnName("NatureID");

                entity.Property(e => e.OwnFundsGbp)
                    .HasColumnName("OwnFundsGBP")
                    .HasColumnType("decimal(25, 2)");

                entity.Property(e => e.ReasonId).HasColumnName("ReasonID");

                entity.Property(e => e.RefreshDueDateTime).HasColumnType("datetime");

                entity.Property(e => e.RegisteredDomicileCountryId).HasColumnName("RegisteredDomicileCountryID");

                entity.Property(e => e.Ttca).HasColumnName("TTCA");

                entity.Property(e => e.TurnoverGbp)
                    .HasColumnName("TurnoverGBP")
                    .HasColumnType("decimal(25, 2)");

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .HasMaxLength(8);

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<LogClientCompanyComplianceCorporateSector>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.ClientCompanyComplianceId).HasColumnName("ClientCompanyComplianceID");

                entity.Property(e => e.ComplianceCorporateSectorFinancialId).HasColumnName("ComplianceCorporateSectorFinancialID");

                entity.Property(e => e.ComplianceCorporateSectorNonFinancialId).HasColumnName("ComplianceCorporateSectorNonFinancialID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<LogClientCompanyComplianceNote>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.AuthUserId).HasColumnName("AuthUserID");

                entity.Property(e => e.ClientCompanyId).HasColumnName("ClientCompanyID");

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NoteText).IsRequired();

                entity.Property(e => e.Title).HasMaxLength(100);
            });

            modelBuilder.Entity<LogClientCompanyContact>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.AspcreationDate)
                    .HasColumnName("ASPCreationDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Aspnumber)
                    .HasColumnName("ASPNumber")
                    .HasMaxLength(9);

                entity.Property(e => e.AuthUserId).HasColumnName("AuthUserID");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.BloombergGpi).HasMaxLength(255);

                entity.Property(e => e.ClientCompanyId).HasColumnName("ClientCompanyID");

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.Forename)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasMaxLength(162)
                    .HasComputedColumnSql("(((isnull([Title]+' ','')+[Forename])+' ')+[Surname])");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LastEmailChangeDate).HasColumnType("datetime");

                entity.Property(e => e.LastTelephoneChangeDate).HasColumnType("datetime");

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.NiNumber).HasMaxLength(50);

                entity.Property(e => e.Position).HasMaxLength(50);

                entity.Property(e => e.RecAmreport).HasColumnName("RecAMReport");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TelephoneDirect).HasMaxLength(50);

                entity.Property(e => e.TelephoneMobile).HasMaxLength(50);

                entity.Property(e => e.TelephoneOther).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(10);

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<LogClientCompanyContactCategory>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.ClientCompanyContactId).HasColumnName("ClientCompanyContactID");

                entity.Property(e => e.ContactCategoryId).HasColumnName("ContactCategoryID");

                entity.Property(e => e.CreatedByAuthUserId).HasColumnName("CreatedByAuthUserID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('UPDATE')");
            });

            modelBuilder.Entity<LogClientCompanyLinkedGroup>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LastUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");
            });

            modelBuilder.Entity<LogClientCompanyOnlineDetails>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.ClientCompanyId).HasColumnName("ClientCompanyID");

                entity.Property(e => e.Collateral).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('UPDATE')");

                entity.Property(e => e.MaxOpen).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.MaxTenor).HasColumnType("datetime");

                entity.Property(e => e.MaxTradeSize).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<LogClientCompanyOnlineDetailsSkew>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.ClientCompanyOnlineDetailsId).HasColumnName("ClientCompanyOnlineDetailsID");

                entity.Property(e => e.Currency1Id).HasColumnName("Currency1ID");

                entity.Property(e => e.Currency2Id).HasColumnName("Currency2ID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('UPDATE')");

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<LogClientCompanyOpi>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.ToTable("LogClientCompanyOPI");

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.AccountName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.AccountNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AuthorisedByAuthUserId).HasColumnName("AuthorisedByAuthUserID");

                entity.Property(e => e.AuthorisedDateTime).HasColumnType("datetime");

                entity.Property(e => e.BankName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.BeneficiaryName).HasMaxLength(250);

                entity.Property(e => e.ClearingCodePrefixId).HasColumnName("ClearingCodePrefixID");

                entity.Property(e => e.ClientCompanyId).HasColumnName("ClientCompanyID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreatedByAuthUserId).HasColumnName("CreatedByAuthUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Iban)
                    .HasColumnName("IBAN")
                    .HasMaxLength(50);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Reference)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RejectedByAuthUserId).HasColumnName("RejectedByAuthUserID");

                entity.Property(e => e.RejectedDateTime).HasColumnType("datetime");

                entity.Property(e => e.SortCode).HasMaxLength(50);

                entity.Property(e => e.SwiftCode)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<LogClientCompanyOpiduplicate>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.ToTable("LogClientCompanyOPIDuplicate");

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.CreatedByAuthUserId).HasColumnName("CreatedByAuthUserID");

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.DuplicateClientCompanyOpiid).HasColumnName("DuplicateClientCompanyOPIID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IsOk).HasColumnName("IsOK");

                entity.Property(e => e.IsOkupdatedByAuthUserId).HasColumnName("IsOKUpdatedByAuthUserID");

                entity.Property(e => e.IsOkupdatedDateTime)
                    .HasColumnName("isOKUpdatedDateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Note).HasMaxLength(250);

                entity.Property(e => e.OriginalClientCompanyOpiid).HasColumnName("OriginalClientCompanyOPIID");
            });

            modelBuilder.Entity<LogClientCompanySalesAppUser>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.ClientCompanyId).HasColumnName("ClientCompanyID");

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SalesPersonAppUserId).HasColumnName("SalesPersonAppUserID");

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<LogComplianceClassificationFile>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.ClientCompanyComplianceId).HasColumnName("ClientCompanyComplianceID");

                entity.Property(e => e.ComplianceClassificationId).HasColumnName("ComplianceClassificationID");

                entity.Property(e => e.DocumentId)
                    .IsRequired()
                    .HasColumnName("DocumentID")
                    .HasMaxLength(100);

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.UploadedByAuthUserId).HasColumnName("UploadedByAuthUserID");

                entity.Property(e => e.UploadedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<LogComplianceIsincurrencyValueDate>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.ToTable("LogComplianceISINCurrencyValueDate");

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.CurrencyPair)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Isin)
                    .IsRequired()
                    .HasColumnName("ISIN")
                    .HasMaxLength(12);

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.ValueDate).HasColumnType("date");
            });

            modelBuilder.Entity<LogComplianceQuestionnaire>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.ClientCompanyComplianceId).HasColumnName("ClientCompanyComplianceID");

                entity.Property(e => e.ComplianceQuestionnaireAnswerId).HasColumnName("ComplianceQuestionnaireAnswerID");

                entity.Property(e => e.ComplianceQuestionnaireQuestionId).HasColumnName("ComplianceQuestionnaireQuestionID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<LogCurrency>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('CREATED')");

                entity.Property(e => e.SwiftAmountFormat)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<LogCurrencyPairPriceHistory>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.CurrencyPair)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.PriceDate).HasColumnType("date");

                entity.Property(e => e.UpdateTimeStamp).HasMaxLength(8);

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<LogFxforwardTrade>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.ToTable("LogFXForwardTrade");

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.AuthorisedByClientCompanyContactId).HasColumnName("AuthorisedByClientCompanyContactID");

                entity.Property(e => e.BdpforwardPoints)
                    .HasColumnName("BDPForwardPoints")
                    .HasColumnType("decimal(25, 8)");

                entity.Property(e => e.BrokenDatePrice).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.BrokerId).HasColumnName("BrokerID");

                entity.Property(e => e.BrokerLhsamt)
                    .HasColumnName("BrokerLHSAmt")
                    .HasColumnType("decimal(25, 8)");

                entity.Property(e => e.BrokerRate).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.BrokerRhsamt)
                    .HasColumnName("BrokerRHSAmt")
                    .HasColumnType("decimal(25, 8)");

                entity.Property(e => e.BrokeredDate).HasColumnType("datetime");

                entity.Property(e => e.ClientCompanyId).HasColumnName("ClientCompanyID");

                entity.Property(e => e.ClientCompanyOpiid).HasColumnName("ClientCompanyOPIID");

                entity.Property(e => e.ClientLhsamt)
                    .HasColumnName("ClientLHSAmt")
                    .HasColumnType("decimal(25, 8)");

                entity.Property(e => e.ClientRate).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.ClientRhsamt)
                    .HasColumnName("ClientRHSAmt")
                    .HasColumnType("decimal(25, 8)");

                entity.Property(e => e.Code)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CollateralPerc).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.CommPaidOutDate).HasColumnType("date");

                entity.Property(e => e.ComplianceIsin)
                    .HasColumnName("Compliance_ISIN")
                    .HasMaxLength(12);

                entity.Property(e => e.ComplianceTradeReasonId).HasColumnName("ComplianceTradeReasonID");

                entity.Property(e => e.ContractDate).HasColumnType("date");

                entity.Property(e => e.ContractNoteSentToClientDateTime).HasColumnType("datetime");

                entity.Property(e => e.ContractNoteSentToMyselfDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedByAuthUserId).HasColumnName("CreatedByAuthUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrencyPair)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveredDate).HasColumnType("datetime");

                entity.Property(e => e.EmirReported).HasColumnName("EMIR_Reported");

                entity.Property(e => e.EmirReportedDateTime)
                    .HasColumnName("EMIR_ReportedDateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.EmirUti)
                    .HasColumnName("EMIR_UTI")
                    .HasMaxLength(104);

                entity.Property(e => e.EmirdelegatedSubmissionId)
                    .HasColumnName("EMIRDelegatedSubmissionID")
                    .HasMaxLength(50);

                entity.Property(e => e.EmirsubmissionId)
                    .HasColumnName("EMIRSubmissionID")
                    .HasMaxLength(50);

                entity.Property(e => e.FilledDateTime).HasColumnType("datetime");

                entity.Property(e => e.FxforwardTradeStatusId).HasColumnName("FXForwardTradeStatusID");

                entity.Property(e => e.IsRhsmajor).HasColumnName("IsRHSMajor");

                entity.Property(e => e.Lhsccyid).HasColumnName("LHSCCYID");

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MarkToMarketValue).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.MarkToMarketValueUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.MarketSideUti)
                    .HasColumnName("MarketSideUTI")
                    .HasMaxLength(100);

                entity.Property(e => e.OpenValueDate).HasColumnType("date");

                entity.Property(e => e.OpiupdatedByAuthUserId).HasColumnName("OPIUpdatedByAuthUserId");

                entity.Property(e => e.OpiupdatedDateTime)
                    .HasColumnName("OPIUpdatedDateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.PrevDayMarktoMarket).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.PrevDayMarktoMarketUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.PrevailingRate2).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.Profit).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.ProfitConsolidatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.ProfitConsolidatedValue).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.ProfitGbprate)
                    .HasColumnName("ProfitGBPRate")
                    .HasColumnType("decimal(25, 8)");

                entity.Property(e => e.Reference).HasMaxLength(20);

                entity.Property(e => e.RemainingClientLhsamt)
                    .HasColumnName("RemainingClientLHSAmt")
                    .HasColumnType("decimal(25, 8)");

                entity.Property(e => e.RemainingClientRhsamt)
                    .HasColumnName("RemainingClientRHSAmt")
                    .HasColumnType("decimal(25, 8)");

                entity.Property(e => e.Rhsccyid).HasColumnName("RHSCCYID");

                entity.Property(e => e.SettledDate).HasColumnType("datetime");

                entity.Property(e => e.TradeInstructionMethodId).HasColumnName("TradeInstructionMethodID");

                entity.Property(e => e.TransactionCommitId).HasColumnName("TransactionCommitID");

                entity.Property(e => e.UpdateTimeStamp).HasMaxLength(8);

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.ValueDate).HasColumnType("date");

                entity.Property(e => e.VerifiedByAuthUserId).HasColumnName("VerifiedByAuthUserID");
            });

            modelBuilder.Entity<LogFxforwardTradeCcmlimitOverride>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.ToTable("LogFXForwardTradeCCMLimitOverride");

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.ClosedByAppUserId).HasColumnName("ClosedByAppUserID");

                entity.Property(e => e.ClosedDateTime).HasColumnType("datetime");

                entity.Property(e => e.ClosedNotes).HasMaxLength(500);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LimitName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OriginalLimit)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.OverrideByAppUserId).HasColumnName("OverrideByAppUserID");

                entity.Property(e => e.OverrideDateTime).HasColumnType("datetime");

                entity.Property(e => e.OverrideValue)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.TradeCode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<LogFxforwardTradeInvoice>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.ToTable("LogFXForwardTradeInvoice");

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.Comment).HasMaxLength(500);

                entity.Property(e => e.DocumentId).HasColumnName("DocumentID");

                entity.Property(e => e.FileName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.TradeCode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .HasMaxLength(8);

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.UploadedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<LogFxoption>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.ToTable("LogFXOption");

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.AuthorisedByClientCompanyContactId).HasColumnName("AuthorisedByClientCompanyContactID");

                entity.Property(e => e.Barrier).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.BestCaseRate).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.BrokerId).HasColumnName("BrokerID");

                entity.Property(e => e.ClientCompanyId).HasColumnName("ClientCompanyID");

                entity.Property(e => e.ClientCompanyOpiid).HasColumnName("ClientCompanyOPIID");

                entity.Property(e => e.ClientLhsamt)
                    .HasColumnName("ClientLHSAmt")
                    .HasColumnType("decimal(25, 8)");

                entity.Property(e => e.ClientLhsamtNotional)
                    .HasColumnName("ClientLHSAmtNotional")
                    .HasColumnType("decimal(25, 8)");

                entity.Property(e => e.ClientRhsamt)
                    .HasColumnName("ClientRHSAmt")
                    .HasColumnType("decimal(25, 8)");

                entity.Property(e => e.ClientRhsamtNotional)
                    .HasColumnName("ClientRHSAmtNotional")
                    .HasColumnType("decimal(25, 8)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CommPaidOutDate).HasColumnType("date");

                entity.Property(e => e.ContractDate).HasColumnType("date");

                entity.Property(e => e.CreatedByAuthUserId).HasColumnName("CreatedByAuthUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrencyPair)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveredDate).HasColumnType("datetime");

                entity.Property(e => e.ExpiryDate).HasColumnType("date");

                entity.Property(e => e.ExtBarrier).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.ExtBarrierDate).HasColumnType("datetime");

                entity.Property(e => e.ExtStrike).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.ExtValueDate).HasColumnType("datetime");

                entity.Property(e => e.ForwardRate).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.FxoptionSettlementId).HasColumnName("FXOptionSettlementID");

                entity.Property(e => e.FxoptionStatusId).HasColumnName("FXOptionStatusID");

                entity.Property(e => e.FxoptionTypeId).HasColumnName("FXOptionTypeID");

                entity.Property(e => e.GraphImgTemplateFile).HasMaxLength(300);

                entity.Property(e => e.IsRhsmajour).HasColumnName("IsRHSMajour");

                entity.Property(e => e.KnockInRate).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.KnockOutRate).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.LevBarrier).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.LevBarrierDate).HasColumnType("datetime");

                entity.Property(e => e.LevNotional).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.LevStrike).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.LevValueDate).HasColumnType("datetime");

                entity.Property(e => e.Lhsccyid).HasColumnName("LHSCCYID");

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OptionTrigger).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.OptionTriggerProtecLvl).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.ParentCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PercentagePart).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.Premium).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.Profit).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.ProtectedLevel).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.Rhsccyid).HasColumnName("RHSCCYID");

                entity.Property(e => e.SettledDate).HasColumnType("datetime");

                entity.Property(e => e.SettlementTradeId).HasColumnName("SettlementTradeID");

                entity.Property(e => e.TradeInstructionMethodId).HasColumnName("TradeInstructionMethodID");

                entity.Property(e => e.TransactionCommitId).HasColumnName("TransactionCommitID");

                entity.Property(e => e.UpdateTimeStamp).HasMaxLength(8);

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.ValueDate).HasColumnType("date");

                entity.Property(e => e.VerifiedByAuthUserId).HasColumnName("VerifiedByAuthUserID");

                entity.Property(e => e.WorstCaseRate).HasColumnType("decimal(25, 8)");
            });

            modelBuilder.Entity<LogPayment>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.Amount).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.ApplicableRate).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.AppliedDateTime).HasColumnType("datetime");

                entity.Property(e => e.AuthorisedByAuthUserId).HasColumnName("AuthorisedByAuthUserID");

                entity.Property(e => e.AuthorisedDateTime).HasColumnType("datetime");

                entity.Property(e => e.ClientCompanyId).HasColumnName("ClientCompanyID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedByAuthUserId).HasColumnName("CreatedByAuthUserID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreditBankAccountId).HasColumnName("CreditBankAccountID");

                entity.Property(e => e.CreditClientCompanyOpiid).HasColumnName("CreditClientCompanyOPIID");

                entity.Property(e => e.CreditClientCompanyVirtualAccountId).HasColumnName("CreditClientCompanyVirtualAccountID");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.DebitBankAccountId).HasColumnName("DebitBankAccountID");

                entity.Property(e => e.DebitClientCompanyVirtualAccountId).HasColumnName("DebitClientCompanyVirtualAccountID");

                entity.Property(e => e.FxforwardTradeCode)
                    .HasColumnName("FXForwardTradeCode")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IsSwiftpayment)
                    .IsRequired()
                    .HasColumnName("IsSWIFTPayment")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentRecReasonId).HasColumnName("PaymentRecReasonID");

                entity.Property(e => e.PaymentSwiftoutgoingStatusId).HasColumnName("PaymentSWIFTOutgoingStatusID");

                entity.Property(e => e.PaymentTypeId).HasColumnName("PaymentTypeID");

                entity.Property(e => e.Reference).HasMaxLength(255);

                entity.Property(e => e.SwiftAuth1ByAuthUserId).HasColumnName("SwiftAuth1ByAuthUserID");

                entity.Property(e => e.SwiftAuth1DateTime).HasColumnType("datetime");

                entity.Property(e => e.SwiftAuth2ByAuthUserId).HasColumnName("SwiftAuth2ByAuthUserID");

                entity.Property(e => e.SwiftAuth2DateTime).HasColumnType("datetime");

                entity.Property(e => e.TransactionCommitId).HasColumnName("TransactionCommitID");

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdateTimeStamp).HasMaxLength(8);

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.ValueDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<LogSwiftincomingFile>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.ToTable("LogSWIFTIncomingFile");

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Filename)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LaufileContent).HasColumnName("LAUFileContent");

                entity.Property(e => e.Laufilename)
                    .HasColumnName("LAUFilename")
                    .HasMaxLength(250);

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SwiftincomingFileProcessingStatusId).HasColumnName("SWIFTIncomingFileProcessingStatusID");

                entity.Property(e => e.SwiftincomingFileTypeId).HasColumnName("SWIFTIncomingFileTypeID");
            });

            modelBuilder.Entity<LogSwiftincomingFileStatement>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.ToTable("LogSWIFTIncomingFileStatement");

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.DisplayError).HasMaxLength(500);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MatchingContent).HasMaxLength(500);

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.ProcessingError).HasMaxLength(1500);

                entity.Property(e => e.RawContentLine61)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.RawContentLine86)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.SwiftincomingFileId).HasColumnName("SWIFTIncomingFileID");
            });

            modelBuilder.Entity<LogSwiftincomingMatchedAccount>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.ToTable("LogSWIFTIncomingMatchedAccount");

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.ClientCompanyId).HasColumnName("ClientCompanyID");

                entity.Property(e => e.CreatedByAuthUserId).HasColumnName("CreatedByAuthUserID");

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MatchingContent)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .HasMaxLength(8);

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<LogSwiftintegrationService>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.ToTable("LogSWIFTIntegrationService");

                entity.Property(e => e.LogId).HasColumnName("LogID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LastStatusChangeByAuthUserId).HasColumnName("LastStatusChangeByAuthUserID");

                entity.Property(e => e.LastStatusChangeDateTime).HasColumnType("datetime");

                entity.Property(e => e.LogAction)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<NavMenuItem>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AuthPermissionId).HasColumnName("AuthPermissionID");

                entity.Property(e => e.DisplayText)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NavMenuSectionId).HasColumnName("NavMenuSectionID");

                entity.Property(e => e.NavigateUrl)
                    .IsRequired()
                    .HasColumnName("NavigateURL")
                    .HasMaxLength(255);

                entity.HasOne(d => d.AuthPermission)
                    .WithMany(p => p.NavMenuItem)
                    .HasForeignKey(d => d.AuthPermissionId)
                    .HasConstraintName("FK_NavMenuItem_AuthPermission");

                entity.HasOne(d => d.NavMenuSection)
                    .WithMany(p => p.NavMenuItem)
                    .HasForeignKey(d => d.NavMenuSectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NavMenuItem_NavMenuSection");
            });

            modelBuilder.Entity<NavMenuSection>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.ApplicableRate).HasColumnType("decimal(25, 8)");

                entity.Property(e => e.AppliedDateTime).HasColumnType("datetime");

                entity.Property(e => e.AuthorisedByAuthUserId).HasColumnName("AuthorisedByAuthUserID");

                entity.Property(e => e.AuthorisedDateTime).HasColumnType("datetime");

                entity.Property(e => e.ClientCompanyId).HasColumnName("ClientCompanyID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedByAuthUserId).HasColumnName("CreatedByAuthUserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.FxforwardTradeCode)
                    .HasColumnName("FXForwardTradeCode")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IsDebitedForMfidaccounts).HasColumnName("IsDebitedForMFIDAccounts");

                entity.Property(e => e.IsSwiftpayment)
                    .IsRequired()
                    .HasColumnName("IsSWIFTPayment")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NotifyClient).HasDefaultValueSql("((0))");

                entity.Property(e => e.PaymentRecReasonId).HasColumnName("PaymentRecReasonID");

                entity.Property(e => e.PaymentSwiftoutgoingStatusId)
                    .HasColumnName("PaymentSWIFTOutgoingStatusID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PaymentTypeId).HasColumnName("PaymentTypeID");

                entity.Property(e => e.Reference).HasMaxLength(255);

                entity.Property(e => e.SwiftAuth1ByAuthUserId).HasColumnName("SwiftAuth1ByAuthUserID");

                entity.Property(e => e.SwiftAuth1DateTime).HasColumnType("datetime");

                entity.Property(e => e.SwiftAuth2ByAuthUserId).HasColumnName("SwiftAuth2ByAuthUserID");

                entity.Property(e => e.SwiftAuth2DateTime).HasColumnType("datetime");

                entity.Property(e => e.TransactionCommitId).HasColumnName("TransactionCommitID");

                entity.Property(e => e.UpdateDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.ValueDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.AuthorisedByAuthUser)
                    .WithMany(p => p.PaymentAuthorisedByAuthUser)
                    .HasForeignKey(d => d.AuthorisedByAuthUserId)
                    .HasConstraintName("FK_Payment_AuthUser2");

                entity.HasOne(d => d.ClientCompany)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.ClientCompanyId)
                    .HasConstraintName("FK_Payment_ClientCompany");

                entity.HasOne(d => d.CreatedByAuthUser)
                    .WithMany(p => p.PaymentCreatedByAuthUser)
                    .HasForeignKey(d => d.CreatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payment_AuthUser");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payment_Currency");

                entity.HasOne(d => d.FxforwardTradeCodeNavigation)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.FxforwardTradeCode)
                    .HasConstraintName("FK_Payment_FXForwardTrade");

                entity.HasOne(d => d.PaymentRecReason)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.PaymentRecReasonId)
                    .HasConstraintName("FK_Payment_PaymentRecReason");

                entity.HasOne(d => d.PaymentSwiftoutgoingStatus)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.PaymentSwiftoutgoingStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payment_PaymentSWIFTOutgoingStatus");

                entity.HasOne(d => d.PaymentType)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.PaymentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payment_PaymentType");

                entity.HasOne(d => d.SwiftAuth1ByAuthUser)
                    .WithMany(p => p.PaymentSwiftAuth1ByAuthUser)
                    .HasForeignKey(d => d.SwiftAuth1ByAuthUserId)
                    .HasConstraintName("FK_Payment_SwiftAuth1ByAuthUserID");

                entity.HasOne(d => d.SwiftAuth2ByAuthUser)
                    .WithMany(p => p.PaymentSwiftAuth2ByAuthUser)
                    .HasForeignKey(d => d.SwiftAuth2ByAuthUserId)
                    .HasConstraintName("FK_Payment_SwiftAuth2ByAuthUserID");

                entity.HasOne(d => d.TransactionCommit)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.TransactionCommitId)
                    .HasConstraintName("FK_Payment_TransactionCommit");

                entity.HasOne(d => d.UpdatedByAuthUser)
                    .WithMany(p => p.PaymentUpdatedByAuthUser)
                    .HasForeignKey(d => d.UpdatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payment_AuthUser1");
            });

            modelBuilder.Entity<PaymentRecReason>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PaymentSwiftoutgoingStatus>(entity =>
            {
                entity.ToTable("PaymentSWIFTOutgoingStatus");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<PaymentSwiftoutgoingStatusTransitions>(entity =>
            {
                entity.HasKey(e => new { e.FromStatusId, e.ToStatusId });

                entity.ToTable("PaymentSWIFTOutgoingStatusTransitions");

                entity.Property(e => e.FromStatusId).HasColumnName("FromStatusID");

                entity.Property(e => e.ToStatusId).HasColumnName("ToStatusID");

                entity.Property(e => e.CreateDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<PaymentType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PipelineAction>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((1))");

                entity.Property(e => e.PipelineActionTypeId).HasColumnName("PipelineActionTypeID");

                entity.HasOne(d => d.PipelineActionType)
                    .WithMany(p => p.PipelineAction)
                    .HasForeignKey(d => d.PipelineActionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PipelineAction_PipelineActionType");
            });

            modelBuilder.Entity<PipelineActionType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ReportProcessedLog>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AuthUserId).HasColumnName("AuthUserID");

                entity.Property(e => e.EndDateTime).HasColumnType("datetime");

                entity.Property(e => e.ExceptionInfo).HasMaxLength(200);

                entity.Property(e => e.FunctionName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(700);

                entity.Property(e => e.Parameters)
                    .IsRequired()
                    .HasColumnType("xml");

                entity.Property(e => e.ReportStatusId).HasColumnName("ReportStatusID");

                entity.Property(e => e.Result).HasColumnType("xml");

                entity.Property(e => e.ResultPage)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StartDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.AuthUser)
                    .WithMany(p => p.ReportProcessedLog)
                    .HasForeignKey(d => d.AuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportProcessedLog_AuthUser");

                entity.HasOne(d => d.ReportStatus)
                    .WithMany(p => p.ReportProcessedLog)
                    .HasForeignKey(d => d.ReportStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportProcessedLog_ReportStatus");
            });

            modelBuilder.Entity<ReportQueueToProcess>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AuthUserId).HasColumnName("AuthUserID");

                entity.Property(e => e.FunctionName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(700);

                entity.Property(e => e.Parameters)
                    .IsRequired()
                    .HasColumnType("xml");

                entity.Property(e => e.ReportStatusId).HasColumnName("ReportStatusID");

                entity.Property(e => e.ResultPage)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StartDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.AuthUser)
                    .WithMany(p => p.ReportQueueToProcess)
                    .HasForeignKey(d => d.AuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportQueueToProcess_AuthUser");

                entity.HasOne(d => d.ReportStatus)
                    .WithMany(p => p.ReportQueueToProcess)
                    .HasForeignKey(d => d.ReportStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportQueueToProcess_ReportStatus");
            });

            modelBuilder.Entity<ReportStatus>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<ScheduledReportDummyPluginTable>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SchemaVersions>(entity =>
            {
                entity.Property(e => e.Applied).HasColumnType("datetime");

                entity.Property(e => e.ScriptName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<SuspiciousActivityReport>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AcknowledgementReceivedDateTime).HasColumnType("datetime");

                entity.Property(e => e.ClientName).HasMaxLength(200);

                entity.Property(e => e.Conlusions).IsUnicode(false);

                entity.Property(e => e.ConsentNcareceivedDescription)
                    .HasColumnName("ConsentNCAReceivedDescription")
                    .IsUnicode(false);

                entity.Property(e => e.CreateDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CustomerInformation).IsUnicode(false);

                entity.Property(e => e.DateTimeReceivedByMlro)
                    .HasColumnName("DateTimeReceivedByMLRO")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.DocumentsInvestigatedInformation).IsUnicode(false);

                entity.Property(e => e.IsReportMadeToNca).HasColumnName("IsReportMadeToNCA");

                entity.Property(e => e.IssueClosedDateTime).HasColumnType("datetime");

                entity.Property(e => e.NcareportDateTime)
                    .HasColumnName("NCAReportDateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.PaymentCode).HasMaxLength(50);

                entity.Property(e => e.ReasonNcareportNotMade)
                    .HasColumnName("ReasonNCAReportNotMade")
                    .IsUnicode(false);

                entity.Property(e => e.ResearchUnderTakenDescription).IsUnicode(false);

                entity.Property(e => e.TradeCode).HasMaxLength(100);

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdateTimestamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.HasOne(d => d.CreatedByAuthUser)
                    .WithMany(p => p.SuspiciousActivityReportCreatedByAuthUser)
                    .HasForeignKey(d => d.CreatedByAuthUserId)
                    .HasConstraintName("FK_SuspiciousActivityReport_AuthUser_CreatedBy");

                entity.HasOne(d => d.IssueClosedByAuthUser)
                    .WithMany(p => p.SuspiciousActivityReportIssueClosedByAuthUser)
                    .HasForeignKey(d => d.IssueClosedByAuthUserId)
                    .HasConstraintName("FK_SuspiciousActivityReport_AuthUser_IssueClosedBy");

                entity.HasOne(d => d.UpdatedByAuthUser)
                    .WithMany(p => p.SuspiciousActivityReportUpdatedByAuthUser)
                    .HasForeignKey(d => d.UpdatedByAuthUserId)
                    .HasConstraintName("FK_SuspiciousActivityReport_AuthUser_UpdatedBy");
            });

            modelBuilder.Entity<SwiftincomingFile>(entity =>
            {
                entity.ToTable("SWIFTIncomingFile");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Filename)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.LaufileContent).HasColumnName("LAUFileContent");

                entity.Property(e => e.Laufilename)
                    .HasColumnName("LAUFilename")
                    .HasMaxLength(250);

                entity.Property(e => e.SwiftincomingFileProcessingStatusId).HasColumnName("SWIFTIncomingFileProcessingStatusID");

                entity.Property(e => e.SwiftincomingFileTypeId).HasColumnName("SWIFTIncomingFileTypeID");

                entity.HasOne(d => d.SwiftincomingFileProcessingStatus)
                    .WithMany(p => p.SwiftincomingFile)
                    .HasForeignKey(d => d.SwiftincomingFileProcessingStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SWIFTIncomingFile_SWIFTIncomingFileProcessingStatus");

                entity.HasOne(d => d.SwiftincomingFileType)
                    .WithMany(p => p.SwiftincomingFile)
                    .HasForeignKey(d => d.SwiftincomingFileTypeId)
                    .HasConstraintName("FK_SWIFTIncomingFile_SWIFTIncomingFileType");
            });

            modelBuilder.Entity<SwiftincomingFileProcessingStatus>(entity =>
            {
                entity.ToTable("SWIFTIncomingFileProcessingStatus");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<SwiftincomingFileStatement>(entity =>
            {
                entity.ToTable("SWIFTIncomingFileStatement");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DisplayError).HasMaxLength(500);

                entity.Property(e => e.FilePartNumber).HasDefaultValueSql("((1))");

                entity.Property(e => e.MatchingContent).HasMaxLength(500);

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.ProcessingError).HasMaxLength(1500);

                entity.Property(e => e.RawContentLine61)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.RawContentLine86)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.SwiftincomingFileId).HasColumnName("SWIFTIncomingFileID");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.SwiftincomingFileStatement)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("FK_SWIFTIncomingFileStatement_Payment");

                entity.HasOne(d => d.SwiftincomingFile)
                    .WithMany(p => p.SwiftincomingFileStatement)
                    .HasForeignKey(d => d.SwiftincomingFileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SWIFTIncomingFileStatement_SWIFTIncomingFile");
            });

            modelBuilder.Entity<SwiftincomingFileType>(entity =>
            {
                entity.ToTable("SWIFTIncomingFileType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<SwiftincomingMatchedAccount>(entity =>
            {
                entity.ToTable("SWIFTIncomingMatchedAccount");

                entity.HasIndex(e => e.ChecksumMatchingContent)
                    .HasName("IX_SWIFTIncomingMatchedAccount_MatchingContent");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ChecksumMatchingContent).HasComputedColumnSql("(checksum([MatchingContent]))");

                entity.Property(e => e.ClientCompanyId).HasColumnName("ClientCompanyID");

                entity.Property(e => e.CreatedByAuthUserId).HasColumnName("CreatedByAuthUserID");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MatchingContent)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.ClientCompany)
                    .WithMany(p => p.SwiftincomingMatchedAccount)
                    .HasForeignKey(d => d.ClientCompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SWIFTIncomingMatchedAccount_ClientCompany");

                entity.HasOne(d => d.CreatedByAuthUser)
                    .WithMany(p => p.SwiftincomingMatchedAccountCreatedByAuthUser)
                    .HasForeignKey(d => d.CreatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SWIFTIncomingMatchedAccount_CreatedByAuthUser");

                entity.HasOne(d => d.UpdatedByAuthUser)
                    .WithMany(p => p.SwiftincomingMatchedAccountUpdatedByAuthUser)
                    .HasForeignKey(d => d.UpdatedByAuthUserId)
                    .HasConstraintName("FK_SWIFTIncomingMatchedAccount_UpdatedByAuthUser");
            });

            modelBuilder.Entity<SwiftintegrationService>(entity =>
            {
                entity.ToTable("SWIFTIntegrationService");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LastStatusChangeByAuthUserId).HasColumnName("LastStatusChangeByAuthUserID");

                entity.Property(e => e.LastStatusChangeDateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.HasOne(d => d.LastStatusChangeByAuthUser)
                    .WithMany(p => p.SwiftintegrationService)
                    .HasForeignKey(d => d.LastStatusChangeByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SWIFTIntegrationService_AuthUser");
            });

            modelBuilder.Entity<Swiftmessage>(entity =>
            {
                entity.ToTable("SWIFTMessage");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(50);

                entity.Property(e => e.HitErrorCode).HasMaxLength(100);

                entity.Property(e => e.HitUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Laufile).HasColumnName("LAUFile");

                entity.Property(e => e.LaufileName)
                    .HasColumnName("LAUFileName")
                    .HasMaxLength(50);

                entity.Property(e => e.NakErrorCode).HasMaxLength(100);

                entity.Property(e => e.NakUpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.SenderReference)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.Xmlfile).HasColumnName("XMLFile");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Swiftmessage)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SWIFTMessage_Payment");
            });

            modelBuilder.Entity<SwiftvalidationCurrencyCountry>(entity =>
            {
                entity.HasKey(e => new { e.CurrencyId, e.CountryId, e.OptionId });

                entity.ToTable("SWIFTValidationCurrencyCountry");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.OptionId).HasColumnName("OptionID");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.SwiftvalidationCurrencyCountry)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SWIFTValidationCurrencyCountry_Country");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.SwiftvalidationCurrencyCountry)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SWIFTValidationCurrencyCountry_Currency");

                entity.HasOne(d => d.Option)
                    .WithMany(p => p.SwiftvalidationCurrencyCountry)
                    .HasForeignKey(d => d.OptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SWIFTValidationCurrencyCountry_SWIFTValidationOption");
            });

            modelBuilder.Entity<SwiftvalidationCurrencyMessageField>(entity =>
            {
                entity.HasKey(e => new { e.CurrencyId, e.MessageId, e.MessageFieldId });

                entity.ToTable("SWIFTValidationCurrencyMessageField");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.MessageFieldId).HasColumnName("MessageFieldID");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.SwiftvalidationCurrencyMessageField)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SWIFTValidationCurrencyMessageField_Currency");

                entity.HasOne(d => d.MessageField)
                    .WithMany(p => p.SwiftvalidationCurrencyMessageField)
                    .HasForeignKey(d => d.MessageFieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SWIFTValidationCurrencyMessageField_SWIFTValidationMessageField");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.SwiftvalidationCurrencyMessageField)
                    .HasForeignKey(d => d.MessageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SWIFTValidationCurrencyMessageField_SWIFTValidationMessage");
            });

            modelBuilder.Entity<SwiftvalidationField>(entity =>
            {
                entity.ToTable("SWIFTValidationField");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.PaymentTypeId).HasColumnName("PaymentTypeID");

                entity.Property(e => e.Tag)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.PaymentType)
                    .WithMany(p => p.SwiftvalidationField)
                    .HasForeignKey(d => d.PaymentTypeId)
                    .HasConstraintName("FK_SWIFTValidationField_PaymentTypeID");
            });

            modelBuilder.Entity<SwiftvalidationFieldComponent>(entity =>
            {
                entity.ToTable("SWIFTValidationFieldComponent");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SwiftvalidationFieldFieldComponent>(entity =>
            {
                entity.HasKey(e => new { e.FieldId, e.FieldComponentId });

                entity.ToTable("SWIFTValidationFieldFieldComponent");

                entity.Property(e => e.FieldId).HasColumnName("FieldID");

                entity.Property(e => e.FieldComponentId).HasColumnName("FieldComponentID");

                entity.Property(e => e.LineNumber).HasDefaultValueSql("((1))");

                entity.Property(e => e.Sequence).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.FieldComponent)
                    .WithMany(p => p.SwiftvalidationFieldFieldComponent)
                    .HasForeignKey(d => d.FieldComponentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SWIFTValidationFieldFieldComponent_SWIFTValidationFieldComponent");

                entity.HasOne(d => d.Field)
                    .WithMany(p => p.SwiftvalidationFieldFieldComponent)
                    .HasForeignKey(d => d.FieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SWIFTValidationFieldFieldComponent_SWIFTValidationField");
            });

            modelBuilder.Entity<SwiftvalidationMessage>(entity =>
            {
                entity.ToTable("SWIFTValidationMessage");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<SwiftvalidationMessageField>(entity =>
            {
                entity.ToTable("SWIFTValidationMessageField");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<SwiftvalidationOption>(entity =>
            {
                entity.ToTable("SWIFTValidationOption");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Sequence).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<SwiftvalidationOptionField>(entity =>
            {
                entity.HasKey(e => new { e.OptionId, e.FieldId });

                entity.ToTable("SWIFTValidationOptionField");

                entity.Property(e => e.OptionId).HasColumnName("OptionID");

                entity.Property(e => e.FieldId).HasColumnName("FieldID");

                entity.Property(e => e.Sequence).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Field)
                    .WithMany(p => p.SwiftvalidationOptionField)
                    .HasForeignKey(d => d.FieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SWIFTValidationOptionField_SWIFTValidationField");

                entity.HasOne(d => d.Option)
                    .WithMany(p => p.SwiftvalidationOptionField)
                    .HasForeignKey(d => d.OptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SWIFTValidationOptionField_SWIFTValidationOption");
            });

            modelBuilder.Entity<SystemEmailSenderAddress>(entity =>
            {
                entity.HasIndex(e => e.EmailKeyName)
                    .HasName("U_EmailKeyName")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EmailAddressValue).HasMaxLength(500);

                entity.Property(e => e.EmailKeyName).HasMaxLength(255);
            });

            modelBuilder.Entity<TelephoneCountryCode>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Iso)
                    .IsRequired()
                    .HasColumnName("ISO")
                    .HasMaxLength(3);

                entity.Property(e => e.Iso3)
                    .HasColumnName("ISO3")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.Nicename)
                    .IsRequired()
                    .HasMaxLength(80);
            });

            modelBuilder.Entity<TradeInstructionMethod>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TransactionCommit>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AuthUserId).HasColumnName("AuthUserID");

                entity.Property(e => e.CommitDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.AuthUser)
                    .WithMany(p => p.TransactionCommit)
                    .HasForeignKey(d => d.AuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransactionCommit_AuthUser");
            });

            modelBuilder.Entity<UserAuditLogChanges>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ActionType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Data).IsRequired();

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.IpAddress)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserRole)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserAuditLogPageViews>(entity =>
            {
                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.IpAddress)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PageViewName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.AuthUser)
                    .WithMany(p => p.UserAuditLogPageViews)
                    .HasForeignKey(d => d.AuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAuditLogPageViews_AuthUser");
            });

            modelBuilder.Entity<UserChangeRequest>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AuthUserId).HasColumnName("AuthUserID");

                entity.Property(e => e.ChangeDateTime).HasColumnType("datetime");

                entity.Property(e => e.ChangeStatus)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ChangeValueType)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.ChangedByAuthUserId).HasColumnName("ChangedByAuthUserID");

                entity.Property(e => e.CurrentValue)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ProposedValue)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.AuthUser)
                    .WithMany(p => p.UserChangeRequestAuthUser)
                    .HasForeignKey(d => d.AuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserChangeRequest_AuthUser");

                entity.HasOne(d => d.ChangedByAuthUser)
                    .WithMany(p => p.UserChangeRequestChangedByAuthUser)
                    .HasForeignKey(d => d.ChangedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserChangeRequest_AuthUser1");
            });

            modelBuilder.Entity<UserChangeRequestApproval>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApprovedByAuthUserId).HasColumnName("ApprovedByAuthUserID");

                entity.Property(e => e.ApprovedDateTime).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UserChangeRequestId).HasColumnName("UserChangeRequestID");

                entity.HasOne(d => d.ApprovedByAuthUser)
                    .WithMany(p => p.UserChangeRequestApproval)
                    .HasForeignKey(d => d.ApprovedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserChangeRequestApproval_AuthUser");

                entity.HasOne(d => d.UserChangeRequest)
                    .WithMany(p => p.UserChangeRequestApproval)
                    .HasForeignKey(d => d.UserChangeRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserChangeRequestApproval_UserChangeRequest");
            });

            modelBuilder.Entity<VirtualAccountTransaction>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(25, 8)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.FxforwardTradeCode)
                    .HasColumnName("FXForwardTradeCode")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.UpdateTimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.VirtualAccountId).HasColumnName("VirtualAccountID");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.VirtualAccountTransaction)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VirtualAccountTransaction_Currency");

                entity.HasOne(d => d.FxforwardTradeCodeNavigation)
                    .WithMany(p => p.VirtualAccountTransaction)
                    .HasForeignKey(d => d.FxforwardTradeCode)
                    .HasConstraintName("FK_VirtualAccountTransaction_FXForwardTrade");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.VirtualAccountTransaction)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("FK_VirtualAccountTransaction_Payment");

                entity.HasOne(d => d.VirtualAccount)
                    .WithMany(p => p.VirtualAccountTransaction)
                    .HasForeignKey(d => d.VirtualAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VirtualAccountTransaction_ClientCompanyVirtualAccount");
            });

            modelBuilder.Entity<VirtualAccountType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IsPaymentAllowed)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<VirtualAccountTypeBankAccount>(entity =>
            {
                entity.HasKey(e => new { e.VirtualAccountTypeId, e.BankAccountId });

                entity.Property(e => e.VirtualAccountTypeId).HasColumnName("VirtualAccountTypeID");

                entity.Property(e => e.BankAccountId).HasColumnName("BankAccountID");

                entity.HasOne(d => d.BankAccount)
                    .WithMany(p => p.VirtualAccountTypeBankAccount)
                    .HasForeignKey(d => d.BankAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VirtualAccountTypeBankAccount_BankAccount");

                entity.HasOne(d => d.VirtualAccountType)
                    .WithMany(p => p.VirtualAccountTypeBankAccount)
                    .HasForeignKey(d => d.VirtualAccountTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VirtualAccountTypeBankAccount_VirtualAccountType");
            });

            modelBuilder.Entity<ClientSiteAction>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientSiteActionStatusId).HasColumnName("ClientSiteActionStatusID");

                entity.Property(e => e.ClientSiteActionTypeId).HasColumnName("ClientSiteActionTypeID");

                entity.Property(e => e.CreatedByAuthUserId).HasColumnName("CreatedByAuthUserID");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Details)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedByAuthUserId).HasColumnName("UpdatedByAuthUserID");

                entity.Property(e => e.UpdatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdatedTimestamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.HasOne(d => d.ClientSiteActionStatus)
                    .WithMany(p => p.ClientSiteAction)
                    .HasForeignKey(d => d.ClientSiteActionStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientSiteAction_ClientSiteActionStatus");

                entity.HasOne(d => d.ClientSiteActionType)
                    .WithMany(p => p.ClientSiteAction)
                    .HasForeignKey(d => d.ClientSiteActionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientSiteAction_ClientSiteActionType");

                entity.HasOne(d => d.CreatedByAuthUser)
                    .WithMany(p => p.ClientSiteActionCreatedByAuthUser)
                    .HasForeignKey(d => d.CreatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientSiteAction_AuthUser_Client");

                entity.HasOne(d => d.UpdatedByAuthUser)
                    .WithMany(p => p.ClientSiteActionUpdatedByAuthUser)
                    .HasForeignKey(d => d.UpdatedByAuthUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientSiteAction_AuthUser_Trader");
            });

            modelBuilder.Entity<ClientSiteAction2ClientCompanyOpi>(entity =>
            {
                entity.ToTable("ClientSiteAction2ClientCompanyOPI");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientCompanyOpiid).HasColumnName("ClientCompanyOPIID");

                entity.Property(e => e.ClientSiteActionId).HasColumnName("ClientSiteActionID");

                entity.HasOne(d => d.ClientCompanyOpi)
                    .WithMany(p => p.ClientSiteAction2ClientCompanyOpi)
                    .HasForeignKey(d => d.ClientCompanyOpiid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientSiteAction2ClientCompanyOPI_ClientCompanyOPI");

                entity.HasOne(d => d.ClientSiteAction)
                    .WithMany(p => p.ClientSiteAction2ClientCompanyOpi)
                    .HasForeignKey(d => d.ClientSiteActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientSiteAction2ClientCompanyOPI_ClientSiteAction");
            });

            modelBuilder.Entity<ClientSiteAction2FixFxforwardTrade>(entity =>
            {
                entity.ToTable("ClientSiteAction2FixFXForwardTrade");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientSiteActionId).HasColumnName("ClientSiteActionID");

                entity.Property(e => e.FxforwardTradeCode)
                    .IsRequired()
                    .HasColumnName("FXForwardTradeCode")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.ClientSiteAction)
                    .WithMany(p => p.ClientSiteAction2FixFxforwardTrade)
                    .HasForeignKey(d => d.ClientSiteActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientSiteAction2FixFXForwardTrade_ClientSiteAction");

                entity.HasOne(d => d.FxforwardTradeCodeNavigation)
                    .WithMany(p => p.ClientSiteAction2FixFxforwardTrade)
                    .HasForeignKey(d => d.FxforwardTradeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientSiteAction2FixFXForwardTrade_FXForwardTrade");
            });

            modelBuilder.Entity<ClientSiteAction2FxforwardTrade2Opi>(entity =>
            {
                entity.ToTable("ClientSiteAction2FXForwardTrade2OPI");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientSiteActionId).HasColumnName("ClientSiteActionID");

                entity.Property(e => e.FxforwardTrade2Opiid).HasColumnName("FXForwardTrade2OPIID");

                entity.HasOne(d => d.ClientSiteAction)
                    .WithMany(p => p.ClientSiteAction2FxforwardTrade2Opi)
                    .HasForeignKey(d => d.ClientSiteActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientSiteAction2FXForwardTrade2OPI_ClientSiteAction");

                entity.HasOne(d => d.FxforwardTrade2Opi)
                    .WithMany(p => p.ClientSiteAction2FxforwardTrade2Opi)
                    .HasForeignKey(d => d.FxforwardTrade2Opiid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientSiteAction2FXForwardTrade2OPI_FXForwardTrade2OPI");
            });

            modelBuilder.Entity<ClientSiteAction2Fxswap>(entity =>
            {
                entity.ToTable("ClientSiteAction2FXSwap");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClientSiteActionId).HasColumnName("ClientSiteActionID");

                entity.Property(e => e.FxswapId).HasColumnName("FXSwapID");

                entity.HasOne(d => d.ClientSiteAction)
                    .WithMany(p => p.ClientSiteAction2Fxswap)
                    .HasForeignKey(d => d.ClientSiteActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientSiteAction2FXSwap_ClientSiteAction");

                entity.HasOne(d => d.Fxswap)
                    .WithMany(p => p.ClientSiteAction2Fxswap)
                    .HasForeignKey(d => d.FxswapId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientSiteAction2FXSwap_FXSwap");
            });

            modelBuilder.Entity<ClientSiteActionStatus>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Details)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ClientSiteActionType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Details)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AppUserNotification>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppUserId)
                    .HasColumnName("AppUserID")
                    .IsRequired();

                entity.Property(e => e.ClientCompanyId)
                    .HasColumnName("ClientCompanyID")
                    .IsRequired();

                entity.Property(e => e.TradeNotifications)
                    .IsRequired();
                entity.Property(e => e.InwardPaymentNotifications)
                    .IsRequired();
                entity.Property(e => e.OutwardPaymentNotifications)
                    .IsRequired();
                entity.Property(e => e.SettlementRequests)
                    .IsRequired();
            });
        }
    }
}
