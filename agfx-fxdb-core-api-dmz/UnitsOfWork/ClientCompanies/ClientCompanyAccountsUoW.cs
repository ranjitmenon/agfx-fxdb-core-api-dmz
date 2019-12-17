using Argentex.Core.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Synetec.Data.UnitOfWork.BaseUnitOfWork;
using Synetec.Data.UnitOfWork.GenericRepo;
using System.Collections.Generic;
using System.Linq;

namespace Argentex.Core.UnitsOfWork.ClientCompanies
{
    public class ClientCompanyAccountsUoW : BaseUow, IClientCompanyAccountsUoW
    {
        private IGenericRepo<ClientCompanyOpi> _clientCompanyOpiRepository;
        private IGenericRepo<ClientCompany> _clientCompanyRepository;
        private IGenericRepo<Currency> _currencyRepository;
        private IGenericRepo<ClearingCodePrefix> _clearingCodePrefixRepository;
        private IGenericRepo<ClientCompanyCurrencyDefaultOpi> _clientCompanyCurrencyDefaultOpi;
        private IGenericRepo<ClientCompanyVirtualAccount> _clientCompanyVirtualAccountRepository;
        private IGenericRepo<VirtualAccountType> _virtualAccountTypeRepository;
        private IGenericRepo<VirtualAccountTypeBankAccount> _virtualAccountTypeBankAccountRepository;
        private IGenericRepo<FxforwardTrade2Opi> _fxforwardTrade2OpiRepository;
        private IGenericRepo<FxforwardTrade> _tradeRepository;


        public ClientCompanyAccountsUoW(FXDB1Context context) : base(context)
        {
        }

        public IGenericRepo<ClientCompany> ClientCompanyRepository => _clientCompanyRepository =
            _clientCompanyRepository ?? new GenericRepo<ClientCompany>(Context);

        public IGenericRepo<ClientCompanyOpi> ClientCompanyOpiRepository => _clientCompanyOpiRepository =
            _clientCompanyOpiRepository ?? new GenericRepo<ClientCompanyOpi>(Context);

        public IGenericRepo<Currency> CurrencyRepository =>
            _currencyRepository = _currencyRepository ?? new GenericRepo<Currency>(Context);

        private IGenericRepo<ClearingCodePrefix> ClearingCodePrefixRepository =>
            _clearingCodePrefixRepository = _clearingCodePrefixRepository ?? new GenericRepo<ClearingCodePrefix>(Context);

        private IGenericRepo<ClientCompanyCurrencyDefaultOpi> ClientCompanyCurrencyDefaultOpi =>
            _clientCompanyCurrencyDefaultOpi = _clientCompanyCurrencyDefaultOpi ??
                                               new GenericRepo<ClientCompanyCurrencyDefaultOpi>(Context);

        private IGenericRepo<ClientCompanyVirtualAccount> ClientCompanyVirtualAccountRepository =>
            _clientCompanyVirtualAccountRepository = _clientCompanyVirtualAccountRepository ?? new GenericRepo<ClientCompanyVirtualAccount>(Context);


        private IGenericRepo<VirtualAccountType> VirtualAccountTypeRepository =>
            _virtualAccountTypeRepository = _virtualAccountTypeRepository ?? new GenericRepo<VirtualAccountType>(Context);
        
        private IGenericRepo<VirtualAccountTypeBankAccount> VirtualAccountTypeBankAccountRepository =>
            _virtualAccountTypeBankAccountRepository = _virtualAccountTypeBankAccountRepository ?? new GenericRepo<VirtualAccountTypeBankAccount>(Context);

        private IGenericRepo<FxforwardTrade2Opi> FxforwardTrade2OpiRepository =>
           _fxforwardTrade2OpiRepository = _fxforwardTrade2OpiRepository ?? new GenericRepo<FxforwardTrade2Opi>(Context);

        private IGenericRepo<FxforwardTrade> TradeRepository =>
            _tradeRepository = _tradeRepository ?? new GenericRepo<FxforwardTrade>(Context);

        public IEnumerable<ClientCompanyOpi> GetClientCompanyAccounts(int clientCompanyId)
        {
            return ClientCompanyOpiRepository
                .GetQueryable(x => x.ClientCompanyId == clientCompanyId);
        }

        public IQueryable<ClientCompanyOpi> GetClientCompanyAccountQueryable(int clientCompanyOpiId)
        {
            return ClientCompanyOpiRepository
                .GetQueryable(x => x.Id == clientCompanyOpiId);
        }

        public IQueryable<ClearingCodePrefix> GetClearingPrefixCodes()
        {
            return ClearingCodePrefixRepository
                .GetQueryable();
        }

        public ClientCompanyOpi GetClientCompanyAccount(int opiId)
        {
            return ClientCompanyOpiRepository.GetByPrimaryKey(opiId);
        }

        public void UpdateAccount(ClientCompanyOpi clientCompanyOpi)
        {
            ClientCompanyOpiRepository.Update(clientCompanyOpi);
            SaveContext();
        }

        public IQueryable<ClientCompanyCurrencyDefaultOpi> GetClientCompanyDefaultAccount(int clientCompanyId, int currencyId)
        {
            return ClientCompanyCurrencyDefaultOpi.GetQueryable(x => x.ClientCompanyId == clientCompanyId && x.CurrencyId == currencyId, orderBy: null, includeProperties: "ClientCompany,ClientCompanyOpi");
        }
        
        public void RemoveDefaultAccount(ClientCompanyCurrencyDefaultOpi account)
        {
            ClientCompanyCurrencyDefaultOpi.Delete(account);
            SaveContext();
        }

        public void AddDefaultAccount(ClientCompanyCurrencyDefaultOpi defaultAccount)
        {
            ClientCompanyCurrencyDefaultOpi.Insert(defaultAccount);
            SaveContext();
        }

        public void AddClientCompanyOpi(ClientCompanyOpi clientCompanyOpi)
        {
            ClientCompanyOpiRepository.Insert(clientCompanyOpi);

            SaveContext();
        }

        public IEnumerable<VirtualAccountType> GetVirtualAccountType(string description)
        {
            return VirtualAccountTypeRepository
                .GetQueryable(x => x.Description == description);
        }

        public IEnumerable<ClientCompanyVirtualAccount> GetClientCompanyVirtualAccount(ClientCompany company, VirtualAccountType vat)
        {
            return ClientCompanyVirtualAccountRepository
                .GetQueryable(x => x.ClientCompanyId == company.Id && x.VirtualAccountTypeId == vat.Id);
        }

        public IEnumerable<VirtualAccountTypeBankAccount> GetVirtualAccountTypeBankAccount(VirtualAccountType vat)
        {
            return VirtualAccountTypeBankAccountRepository
                .GetQueryable(x => x.VirtualAccountTypeId == vat.Id, orderBy: null, includeProperties: "BankAccount");
        }

        public IQueryable<FxforwardTrade2Opi> GetTradeOPIs(string tradeCode)
        {
            var query = FxforwardTrade2OpiRepository.GetQueryable(x => x.FxforwardTradeCode == tradeCode);

            query = query.Include(x => x.ClientCompanyOpi)
                .Include(x => x.CreatedByAuthUser)
                .Include(x => x.CreatedByAuthUser.Application)
                .Include(x => x.CreatedByAuthUser.AppUser) //for the Argentex user                
                .Include(x => x.CreatedByAuthUser.ClientCompanyContactAuthUser) //for the Client user
                .Include(x => x.FxforwardTradeCodeNavigation)
                .Include(x => x.FxforwardTradeCodeNavigation.Lhsccy)
                .Include(x => x.FxforwardTradeCodeNavigation.Rhsccy);
            
            return query;
        }

        public void AddTradeOPI(FxforwardTrade2Opi fxforwardTrade2Opi)
        {
            FxforwardTrade2OpiRepository.Insert(fxforwardTrade2Opi);
            SaveContext();
        }

        public int GetAssociatedTradesCount(int clientCompanyOpiId, int statusDeliveredID)
        {
            var associatedOpiSettlements = FxforwardTrade2OpiRepository
               .GetQueryable(e => e.ClientCompanyOpiid == clientCompanyOpiId
               && e.FxforwardTradeCodeNavigation.FxforwardTradeStatusId != statusDeliveredID)
               .Select(x => x.FxforwardTradeCode).Distinct().ToList();

            var associatedTrades = TradeRepository
                .GetQueryable(t => t.ClientCompanyOpiid == clientCompanyOpiId
                && t.FxforwardTradeStatusId != statusDeliveredID && !t.Code.Contains("/RL"))
                .Select(x => x.Code).Distinct().ToList();

            return associatedOpiSettlements.Union(associatedTrades).Count();
        }

        public IList<long> GetSettlementIDs(int clientCompanyOpiId)
        {
            return FxforwardTrade2OpiRepository.GetQueryable(x => x.ClientCompanyOpiid == clientCompanyOpiId).Select(x => x.Id).ToList();
        }
    }
}
