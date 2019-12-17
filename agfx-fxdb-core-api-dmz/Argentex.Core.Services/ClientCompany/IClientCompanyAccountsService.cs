using Argentex.Core.Service.Models.ClearingCodePrefix;
using Argentex.Core.Service.Models.ClientCompany;
using System;
using System.Collections.Generic;

namespace Argentex.Core.Service.ClientCompanies
{
    public interface IClientCompanyAccountsService : IDisposable
    {
        IEnumerable<ClientCompanyAccountModel> GetClientCompanyAccounts(int clientCompanyId);
        ClientCompanyAccountModel GetClientCompanyAccount(int clientCompanyOpiId);
        void AddSettlementAccount(SettlementAccountModel settlementAccount);
        IEnumerable<ClearingCodePrefixModel> GetClearingCodePrefixes();
        void EditSettlementAccount(SettlementAccountModel settlementAccount);
        void SetAccountAsDefault(SetDefaultAccountModel model);
        IEnumerable<FXForwardTrade2OPIModel> GetTradeOPIs(string tradeCode);
        void AddTradeOPI(FXForwardTrade2OPIModel model);
        void DeleteSettlementAccount(int clientCompanyOpiId, int authUserName);
        int GetNumberOfAssociatedTrades(int clientCompanyOpiId);
    }
}
