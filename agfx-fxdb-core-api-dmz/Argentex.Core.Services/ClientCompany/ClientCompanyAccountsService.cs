using System;
using System.Collections.Generic;
using System.Linq;
using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Service.ClientSiteAction;
using Argentex.Core.Service.Enums;
using Argentex.Core.Service.Exceptions;
using Argentex.Core.Service.Models.ClearingCodePrefix;
using Argentex.Core.Service.Models.ClientCompany;
using Argentex.Core.Service.Settlements;
using Argentex.Core.UnitsOfWork.ClientCompanies;

namespace Argentex.Core.Service.ClientCompanies
{
    public class ClientCompanyAccountsService : IClientCompanyAccountsService
    {
        private readonly IClientCompanyAccountsUoW _clientCompanyAccountsUow;
        private readonly IClientSiteActionService _clientSiteActionService;
        private readonly ISettlementService _settlementService;

        private bool _disposed;

        public ClientCompanyAccountsService(IClientCompanyAccountsUoW clientCompanyAccountsUoW,
            IClientSiteActionService clientSiteActionService,
            ISettlementService settlementService)
        {
            _clientCompanyAccountsUow = clientCompanyAccountsUoW;
            _clientSiteActionService = clientSiteActionService;
            _settlementService = settlementService;
        }

        public IEnumerable<ClientCompanyAccountModel> GetClientCompanyAccounts(int clientCompanyId)
        {
            var clientCompany = _clientCompanyAccountsUow.ClientCompanyRepository.GetByPrimaryKey(clientCompanyId);

            if (clientCompany == null)
                throw new ClientCompanyNotFoundException($"Client company with id {clientCompanyId} does not exist");

            var clientCompanyAccounts =
                _clientCompanyAccountsUow.ClientCompanyOpiRepository
                .Get(x => x.ClientCompanyId == clientCompanyId && x.Authorised && !x.Rejected && !x.IsDeleted);

            return clientCompanyAccounts.Any()
                ? clientCompanyAccounts.Select(x => new ClientCompanyAccountModel
                {
                    ClientCompanyId = x.ClientCompanyId,
                    ClientCompanyOpiId = x.Id,
                    AccountName = x.AccountName,
                    AccountNumber = !string.IsNullOrWhiteSpace(x.AccountNumber) ? x.AccountNumber : x.Iban,
                    Currency = _clientCompanyAccountsUow.CurrencyRepository.GetByPrimaryKey(x.CurrencyId)?.Code
                }).OrderBy(x => x.AccountName).ToList() 
                : new List<ClientCompanyAccountModel>();
        }

        public ClientCompanyAccountModel GetClientCompanyAccount(int clientCompanyOpiId)
        {
            return _clientCompanyAccountsUow
                .GetClientCompanyAccountQueryable(clientCompanyOpiId)
                .Select(x => new ClientCompanyAccountModel
                {
                    ClientCompanyOpiId = x.Id,
                    ClientCompanyId = x.ClientCompanyId,
                    CurrencyId = x.CurrencyId,
                    Currency = x.Currency.Code,
                    CountryId = x.CountryId ?? 0,
                    Country = x.Country.Name,
                    Description = x.Description,
                    BankName = x.BankName,
                    BankAddress = x.BankAddress,
                    ClearingCodePrefixId = x.ClearingCodePrefixId ?? 0,
                    AccountNumber = x.AccountNumber,
                    AccountName = x.AccountName,
                    SortCode = x.SortCode,
                    SwiftCode = x.SwiftCode,
                    Iban = x.Iban,
                    IsDefault = x.ClientCompanyCurrencyDefaultOpi
                                .Select(y => y.ClientCompanyOpiid == clientCompanyOpiId)
                                .SingleOrDefault(),
                    Approved = x.Authorised,
                    BeneficiaryName = x.BeneficiaryName,
                    BeneficiaryAddress = x.BeneficiaryAddress,
                    Reference = x.Reference,
                    UpdatedByAuthUserId = x.UpdatedByAuthUserId
                }).FirstOrDefault();

        }

        public void AddSettlementAccount(SettlementAccountModel settlementAccount)
        {
            var clientCompanyOpi = MapClientCompanyOpi(
                new ClientCompanyOpi
                {
                    CreatedDate = DateTime.UtcNow,
                    IsOwnAccount = false,
                    Rejected = false,
                    Authorised = false,
                    IsCompanyAccount = false
                }, 
                settlementAccount);

            _clientCompanyAccountsUow.AddClientCompanyOpi(clientCompanyOpi);
            _clientSiteActionService.LogActionNewOpi(settlementAccount.UpdatedByAuthUserId, clientCompanyOpi.Id);
        }

        public IEnumerable<ClearingCodePrefixModel> GetClearingCodePrefixes()
        {
            return _clientCompanyAccountsUow.GetClearingPrefixCodes()
                .Select(x => new ClearingCodePrefixModel
                {
                    Id = x.Id,
                    Code = x.Code
                })
                .ToList();
        }

        public void EditSettlementAccount(SettlementAccountModel settlementAccount)
        {
            var opi = _clientCompanyAccountsUow.GetClientCompanyAccount(settlementAccount.ClientCompanyOpiId);
            var mappedChanges = MapClientCompanyOpi(opi, settlementAccount);
            _clientCompanyAccountsUow.UpdateAccount(mappedChanges);
        }

        /// <summary>
        /// Mark Client Company Account as deleted by setting IsDeleted property to True
        /// and add "DELETED" to the name
        /// </summary>
        /// <param name="clientCompanyOpiId"></param>
        /// <param name="authUserId"></param>
        public void DeleteSettlementAccount(int clientCompanyOpiId, int authUserId)
        {
            var opi = _clientCompanyAccountsUow.GetClientCompanyAccount(clientCompanyOpiId);
            opi.IsDeleted = true;
            opi.AccountName = $"{opi.AccountName} DELETED";
            opi.UpdatedByAuthUserId = authUserId;
            
            // Mark Settlement Account as deleted
            _clientCompanyAccountsUow.UpdateAccount(opi);

            _clientCompanyAccountsUow.GetSettlementIDs(clientCompanyOpiId).ToList()
                .ForEach(x => _settlementService.DeleteAssignedSettlements(x));
        }

        public int GetNumberOfAssociatedTrades(int clientCompanyOpiId)
        {
            return _clientCompanyAccountsUow.GetAssociatedTradesCount(clientCompanyOpiId, (int)TradeStatus.Delivered);
        }

        public void SetAccountAsDefault(SetDefaultAccountModel model)
        {
            var opi = _clientCompanyAccountsUow.GetClientCompanyAccount(model.ClientCompanyOpiId);
            var defaultAccountsForCurrency =
                _clientCompanyAccountsUow.GetClientCompanyDefaultAccount(opi.ClientCompanyId, opi.CurrencyId).ToList();

            foreach (var account in defaultAccountsForCurrency)
            {
                _clientCompanyAccountsUow.RemoveDefaultAccount(account);
            }

            var defaultAccount = CreateDefaultAccountEntity(opi, model.AuthUserId);
            _clientCompanyAccountsUow.AddDefaultAccount(defaultAccount);
        }

        public IEnumerable<FXForwardTrade2OPIModel> GetTradeOPIs(string tradeCode)
        {
            const string ClientApplicationName = "ArgentexClient";

            var query = _clientCompanyAccountsUow.GetTradeOPIs(tradeCode);

            /*
             CreatedByAuthUserName:
             If the user is a client we get the name from the ClientCompanyContact
             If the user is an Argentex user we get the name from the AppUser
            */

            return query.Select(x => new FXForwardTrade2OPIModel
            {
                ID = x.Id,
                FXForwardTradeCode = x.FxforwardTradeCodeNavigation.Code,
                AccountID = x.ClientCompanyOpi.Id,
                AccountName = x.ClientCompanyOpi.AccountName,
                Amount = x.Amount,
                CurrencyCode = x.FxforwardTradeCodeNavigation.IsBuy ?
                    x.FxforwardTradeCodeNavigation.Lhsccy.Code :
                    x.FxforwardTradeCodeNavigation.Rhsccy.Code,
                Details = x.Details,
                CreatedByAuthUserID = x.CreatedByAuthUser.Id,
                CreatedByAuthUserName = x.CreatedByAuthUser.Application.Description == ClientApplicationName ?
                    GetClientUserName(x.CreatedByAuthUser.ClientCompanyContactAuthUser) :
                    GetArgentexUserName(x.CreatedByAuthUser.AppUser),
                CreatedDateTime = x.CreatedDateTime,
                IsClient = x.CreatedByAuthUser.Application.Description == ClientApplicationName
            }).ToList();
        }

        public void AddTradeOPI(FXForwardTrade2OPIModel model)
        {
            FxforwardTrade2Opi fxforwardTrade2Opi = MapFxforwardTrade2Opi(new FxforwardTrade2Opi(), model);            
            _clientCompanyAccountsUow.AddTradeOPI(fxforwardTrade2Opi);
        }

        private FxforwardTrade2Opi MapFxforwardTrade2Opi(FxforwardTrade2Opi fxforwardTrade2Opi, FXForwardTrade2OPIModel model)
        {
            fxforwardTrade2Opi.ClientCompanyOpiid = model.AccountID;
            fxforwardTrade2Opi.CreatedByAuthUserId = model.CreatedByAuthUserID;
            fxforwardTrade2Opi.CreatedDateTime = model.CreatedDateTime;
            fxforwardTrade2Opi.Amount = model.Amount;
            fxforwardTrade2Opi.TradeValueDate = model.ValueDate;
            fxforwardTrade2Opi.Details = model.Details;
            fxforwardTrade2Opi.FxforwardTradeCode = model.FXForwardTradeCode;
            
            return fxforwardTrade2Opi;
        }

        private string GetClientUserName(ICollection<ClientCompanyContact> clientCompanyContactAuthUser)
        {
            if (clientCompanyContactAuthUser != null && clientCompanyContactAuthUser.Count > 0)
            {
                ClientCompanyContact clientCompanyContact = clientCompanyContactAuthUser.FirstOrDefault();
                return $"{clientCompanyContact.Forename} {clientCompanyContact.Surname}";
            }
            
            return string.Empty;
        }

        private string GetArgentexUserName(ICollection<AppUser> appUser)
        {
            if (appUser != null && appUser.Count > 0)
            {
                return appUser.FirstOrDefault().FullName;                
            }

            return string.Empty;
        }
        
        private static ClientCompanyCurrencyDefaultOpi CreateDefaultAccountEntity(ClientCompanyOpi opi, int authUserId)
        {
            return new ClientCompanyCurrencyDefaultOpi
            {
                ClientCompanyId = opi.ClientCompanyId,
                CurrencyId = opi.CurrencyId,
                ClientCompanyOpiid = opi.Id,
                UpdateAuthUserId = authUserId
            };
        }

        private static ClientCompanyOpi MapClientCompanyOpi(ClientCompanyOpi clientCompanyOpi, SettlementAccountModel settlementAccount)
        {
            clientCompanyOpi.Description = settlementAccount.Description ?? "";
            clientCompanyOpi.CurrencyId = settlementAccount.CurrencyId;
            clientCompanyOpi.CountryId = settlementAccount.CountryId;
            clientCompanyOpi.BankName = settlementAccount.BankName ?? "";
            clientCompanyOpi.BankAddress = settlementAccount.BankAddress;
            clientCompanyOpi.AccountName = settlementAccount.AccountName;
            clientCompanyOpi.AccountNumber = settlementAccount.AccountNumber.ToString();
            clientCompanyOpi.ClearingCodePrefixId = settlementAccount.ClearingCodePrefixId == 0 ? null : settlementAccount.ClearingCodePrefixId;
            clientCompanyOpi.SortCode = settlementAccount.SortCode;
            clientCompanyOpi.Reference = settlementAccount.Reference ?? "";
            clientCompanyOpi.SwiftCode = settlementAccount.SwiftCode;
            clientCompanyOpi.Iban = settlementAccount.Iban;
            clientCompanyOpi.BeneficiaryName = settlementAccount.BeneficiaryName;
            clientCompanyOpi.BeneficiaryAddress = settlementAccount.BeneficiaryAddress;
            clientCompanyOpi.CreatedDate = clientCompanyOpi.CreatedDate;
            clientCompanyOpi.CreatedByAuthUserId = settlementAccount.UpdatedByAuthUserId;
            clientCompanyOpi.UpdatedDate = DateTime.UtcNow;
            clientCompanyOpi.UpdatedByAuthUserId = settlementAccount.UpdatedByAuthUserId;
            clientCompanyOpi.ClientCompanyId = settlementAccount.ClientCompanyId;
            clientCompanyOpi.IsOwnAccount = clientCompanyOpi.IsOwnAccount;
            clientCompanyOpi.Rejected = clientCompanyOpi.Rejected;
            clientCompanyOpi.Authorised = clientCompanyOpi.Authorised;
            clientCompanyOpi.IsCompanyAccount = clientCompanyOpi.IsCompanyAccount;

            return clientCompanyOpi;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _clientCompanyAccountsUow?.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

    }
}
