using System;
using System.Collections.Generic;

namespace Argentex.Core.DataAccess.Entities
{
    public partial class Currency
    {
        public Currency()
        {
            BankAccountCurrencyBalance = new HashSet<BankAccountCurrencyBalance>();
            BankAccountCurrencyBalanceHistory = new HashSet<BankAccountCurrencyBalanceHistory>();
            BankAccountCurrencyDetails = new HashSet<BankAccountCurrencyDetails>();
            BankAccountTransaction = new HashSet<BankAccountTransaction>();
            ClientCompanyComplianceCurrency = new HashSet<ClientCompanyComplianceCurrency>();
            ClientCompanyCurrencyDefaultOpi = new HashSet<ClientCompanyCurrencyDefaultOpi>();
            ClientCompanyOnlineDetailsSkewCurrency1 = new HashSet<ClientCompanyOnlineDetailsSkew>();
            ClientCompanyOnlineDetailsSkewCurrency2 = new HashSet<ClientCompanyOnlineDetailsSkew>();
            ClientCompanyOnlineSpreadAdjustmentCurrency1 = new HashSet<ClientCompanyOnlineSpreadAdjustment>();
            ClientCompanyOnlineSpreadAdjustmentCurrency2 = new HashSet<ClientCompanyOnlineSpreadAdjustment>();
            ClientCompanyOpi = new HashSet<ClientCompanyOpi>();
            ClientCompanyOpitransaction = new HashSet<ClientCompanyOpitransaction>();
            ClientCompanyVirtualAccountCurrencyBalance = new HashSet<ClientCompanyVirtualAccountCurrencyBalance>();
            ClientCompanyVirtualAccountCurrencyBalanceHistory = new HashSet<ClientCompanyVirtualAccountCurrencyBalanceHistory>();
            FxforwardTradeLhsccy = new HashSet<FxforwardTrade>();
            FxforwardTradeRhsccy = new HashSet<FxforwardTrade>();
            FxoptionLhsccy = new HashSet<Fxoption>();
            FxoptionRhsccy = new HashSet<Fxoption>();
            Payment = new HashSet<Payment>();
            SwiftvalidationCurrencyCountry = new HashSet<SwiftvalidationCurrencyCountry>();
            SwiftvalidationCurrencyMessageField = new HashSet<SwiftvalidationCurrencyMessageField>();
            VirtualAccountTransaction = new HashSet<VirtualAccountTransaction>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string SwiftAmountFormat { get; set; }
        public byte[] UpdateTimeStamp { get; set; }
        public int CreatedByAuthUserId { get; set; }
        public int UpdatedByAuthUserId { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public AuthUser CreatedByAuthUser { get; set; }
        public AuthUser UpdatedByAuthUser { get; set; }
        public ICollection<BankAccountCurrencyBalance> BankAccountCurrencyBalance { get; set; }
        public ICollection<BankAccountCurrencyBalanceHistory> BankAccountCurrencyBalanceHistory { get; set; }
        public ICollection<BankAccountCurrencyDetails> BankAccountCurrencyDetails { get; set; }
        public ICollection<BankAccountTransaction> BankAccountTransaction { get; set; }
        public ICollection<ClientCompanyComplianceCurrency> ClientCompanyComplianceCurrency { get; set; }
        public ICollection<ClientCompanyCurrencyDefaultOpi> ClientCompanyCurrencyDefaultOpi { get; set; }
        public ICollection<ClientCompanyOnlineDetailsSkew> ClientCompanyOnlineDetailsSkewCurrency1 { get; set; }
        public ICollection<ClientCompanyOnlineDetailsSkew> ClientCompanyOnlineDetailsSkewCurrency2 { get; set; }
        public ICollection<ClientCompanyOnlineSpreadAdjustment> ClientCompanyOnlineSpreadAdjustmentCurrency1 { get; set; }
        public ICollection<ClientCompanyOnlineSpreadAdjustment> ClientCompanyOnlineSpreadAdjustmentCurrency2 { get; set; }
        public ICollection<ClientCompanyOpi> ClientCompanyOpi { get; set; }
        public ICollection<ClientCompanyOpitransaction> ClientCompanyOpitransaction { get; set; }
        public ICollection<ClientCompanyVirtualAccountCurrencyBalance> ClientCompanyVirtualAccountCurrencyBalance { get; set; }
        public ICollection<ClientCompanyVirtualAccountCurrencyBalanceHistory> ClientCompanyVirtualAccountCurrencyBalanceHistory { get; set; }
        public ICollection<FxforwardTrade> FxforwardTradeLhsccy { get; set; }
        public ICollection<FxforwardTrade> FxforwardTradeRhsccy { get; set; }
        public ICollection<Fxoption> FxoptionLhsccy { get; set; }
        public ICollection<Fxoption> FxoptionRhsccy { get; set; }
        public ICollection<Payment> Payment { get; set; }
        public ICollection<SwiftvalidationCurrencyCountry> SwiftvalidationCurrencyCountry { get; set; }
        public ICollection<SwiftvalidationCurrencyMessageField> SwiftvalidationCurrencyMessageField { get; set; }
        public ICollection<VirtualAccountTransaction> VirtualAccountTransaction { get; set; }
    }
}
