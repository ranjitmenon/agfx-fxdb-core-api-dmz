using Argentex.Core.DataAccess.Entities;
using Synetec.Data.UnitOfWork.BaseUnitOfWork;
using Synetec.Data.UnitOfWork.GenericRepo;
using System.Collections.Generic;
using System.Linq;

namespace Argentex.Core.UnitsOfWork.ClientCompanies
{
    public interface IClientCompanyAccountsUoW : IBaseUow
    {
        IGenericRepo<ClientCompany> ClientCompanyRepository { get; }
        IGenericRepo<ClientCompanyOpi> ClientCompanyOpiRepository { get; }
        IGenericRepo<Currency> CurrencyRepository { get; }
        IEnumerable<ClientCompanyOpi> GetClientCompanyAccounts(int clientCompanyId);
        IQueryable<ClientCompanyOpi> GetClientCompanyAccountQueryable(int clientCompanyOpiId);
        void AddClientCompanyOpi(ClientCompanyOpi clientCompanyOpi);
        IQueryable<ClearingCodePrefix> GetClearingPrefixCodes();
        ClientCompanyOpi GetClientCompanyAccount(int opiId);
        void UpdateAccount(ClientCompanyOpi clientCompanyOpi);
        IQueryable<ClientCompanyCurrencyDefaultOpi> GetClientCompanyDefaultAccount(int clientCompanyId, int currencyId);
        void RemoveDefaultAccount(ClientCompanyCurrencyDefaultOpi account);
        void AddDefaultAccount(ClientCompanyCurrencyDefaultOpi defaultAccount);
        IEnumerable<VirtualAccountType> GetVirtualAccountType(string description);
        IEnumerable<ClientCompanyVirtualAccount> GetClientCompanyVirtualAccount(ClientCompany company, VirtualAccountType vat);
        IEnumerable<VirtualAccountTypeBankAccount> GetVirtualAccountTypeBankAccount(VirtualAccountType vat);
        IQueryable<FxforwardTrade2Opi> GetTradeOPIs(string tradeCode);
        void AddTradeOPI(FxforwardTrade2Opi fxforwardTrade2Opi);
        IList<long> GetSettlementIDs(int clientCompanyOpiId);
        int GetAssociatedTradesCount(int clientCompanyOpiId, int statusDeliveredID);
    }
}
