using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Service.AppSettings;
using Argentex.Core.Service.Currencies;
using Argentex.Core.Service.Helpers;
using Argentex.Core.Service.Models.ClientCompany;
using Argentex.Core.Service.User;
using Argentex.Core.UnitsOfWork.ClientCompanyContacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Argentex.Core.UnitsOfWork.ClientCompanyContacts.Model;

namespace Argentex.Core.Service
{
    public class ClientCompanyService : IClientCompanyService
    {
        private readonly IClientCompanyUow _clientCompanyUow;
        private readonly IUserService _userService;
        private readonly ICurrencyService _currencyService;
        private readonly IAppSettingService _appSettingService;
        private readonly IConfigWrapper _config;
        private bool _disposed;

        public ClientCompanyService(IClientCompanyUow clientCompanyUow,
            IUserService userService,
            ICurrencyService currencyService,
            IAppSettingService appSettingService,
            IConfigWrapper config)
        {
            _clientCompanyUow = clientCompanyUow;
            _userService = userService;
            _currencyService = currencyService;
            _appSettingService = appSettingService;
            _config = config;
        }

        public string GetClientCompanyName(int clientCompanyId)
        {
            return _clientCompanyUow
                .ClientCompanyContactRepository
                .GetQueryable(x => x.ClientCompanyId == clientCompanyId)
                .Select(x => x.ClientCompany.Name)
                .FirstOrDefault();
        }

        public ClientCompanyModel GetClientCompany(int clientCompanyId)
        {
            return _clientCompanyUow
                .GetClientCompany(clientCompanyId)
                .Select(x => new ClientCompanyModel
                {
                    Name = x.Name,
                    Crn = x.Crn,
                    DealerAppUserID = x.DealerAppUserId
                })
                .SingleOrDefault();
        }

        public ICollection<ClientCompaniesModel> GetClientCompanies()
        {
            return _clientCompanyUow
                .GetClientCompanies()
                .Select(x => new ClientCompaniesModel
                {
                   ClientCompanyId = x.Id,
                   ClientCompanyName = x.Name
                }).ToList();
                
        }

        public ICollection<ClientCompanyAccountModel> GetClientCompanyAccounts(int clientCompanyId)
        {
            return _clientCompanyUow
                .GetClientCompanyAccounts(clientCompanyId)
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
                                .Select(y => y.ClientCompanyOpiid)
                                .Contains(x.Id),
                    Approved = x.Authorised,
                    BeneficiaryName = x.BeneficiaryName,
                    BeneficiaryAddress = x.BeneficiaryAddress,
                    Reference = x.Reference,
                    UpdatedByAuthUserId = x.UpdatedByAuthUserId
                }).ToList();

        }

        public ClientCompanyContactResponseModel GetClientCompanyContact(ClientCompanyContactSearchContext clientCompanyContactSearchContext)
        {
            var clientCompanyContactSearchModel = new ClientCompanyContactSearchModel()
            {
                ClientCompanyContactId = clientCompanyContactSearchContext.ClientCompanyContactId,
                AuthUsertId = clientCompanyContactSearchContext.AuthUsertId
            };

            var contact = _clientCompanyUow.GetCurrentClientCompanyContact(clientCompanyContactSearchModel);

            if (contact == null) return new ClientCompanyContactResponseModel();

            var companyContact = new ClientCompanyContactResponseModel
            {
                CompanyContactModel = new ClientCompanyContactModel
                {
                    ID = contact.Id,
                    ContactTitle = contact.Title,
                    ContactForename = contact.Forename,
                    ContactSurname = contact.Surname,
                    ContactEmail = contact.Email,
                    ContactTelephone = contact.TelephoneDirect,
                    Authorized = contact.Authorized,
                    UserName = contact.AuthUser?.UserName,
                    UpdatedByAuthUserId = contact.UpdatedByAuthUserId,
                    Position = contact.Position,
                    TelephoneMobile = contact.TelephoneMobile,
                    TelephoneOther = contact.TelephoneOther,
                    BirthDay = contact.Birthday,
                    IsApproved = contact.AuthUser?.IsApproved ?? false,
                    PrimaryContact = contact.PrimaryContact ?? false,
                    ClientSiteAuthUserID = contact.AuthUserId.HasValue ? (int)contact.AuthUserId : 0,
                    LastTelephoneChangeDate = contact.LastTelephoneChangeDate.GetValueOrDefault(),
                    LastEmailChangeDate = contact.LastEmailChangeDate.GetValueOrDefault(),
                    BloombergGpi = contact.BloombergGpi,
                    NiNumber = contact.NiNumber,
                    ReceiveNotifications = contact.RecNotifications,
                    ReceiveAMReport = contact.RecAmreport,
                    ReceiveActivityReport = contact.RecActivityReport,
                    ASPNumber = contact.Aspnumber ?? string.Empty,
                    ASPCreationDate = contact.AspcreationDate,
                    FullName = contact.Fullname,
                    Notes = contact.Notes,
                    ClientCompany = MapClientCompany(contact.ClientCompany)
                },
                Succeeded = true
            };

            return companyContact;
        }

        public ClientCompanyContactResponseModel GetErrorMessages(HttpStatusCode statusCode, Exception exception, ClientCompanyContactSearchContext clientCompanyContactSearchContext)
        {
            ClientCompanyContactResponseModel responseModel = new ClientCompanyContactResponseModel();
            var IdOrUsername = clientCompanyContactSearchContext.ClientCompanyContactId != null ? "Client Company Contact ID" : "AuthUser ID";
            var userId = clientCompanyContactSearchContext.ClientCompanyContactId != null
                ? clientCompanyContactSearchContext.ClientCompanyContactId : clientCompanyContactSearchContext.AuthUsertId;

            switch (statusCode)
            {
                case HttpStatusCode.BadRequest:
                    responseModel.ResponseMessages = new Dictionary<string, string[]>
                    {
                        {
                            "Errors", new string[] {$"Client Company Contact with {IdOrUsername} {userId} could not be retrieved. {exception?.Message}"}
                        }
                    };
                    break;
                case HttpStatusCode.NotFound:
                    responseModel.ResponseMessages = new Dictionary<string, string[]>
                    {
                        {
                            "Errors", new string[] { $"Client Company Contact with {IdOrUsername} {userId} does not exist in the database. {exception?.Message}" }
                        }
                    };
                    break;
            }

            return responseModel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientCompanyContactId"></param>
        /// <returns></returns>
        public void AddSpredAdjustment(SpreadAdjustmentModel spreadAdjustment)
        {
            var spreadAdjustmentValidity = _appSettingService.GetSpreadAdjustmentValidity();

            var clientCompanyOnlineDetails = _clientCompanyUow.GetClientCompanyOnlineDetails(spreadAdjustment.ClientCompanyID).FirstOrDefault();

            var currency1 = _currencyService.GetCurrencyId(spreadAdjustment.BuyCcy);
            var currency2 = _currencyService.GetCurrencyId(spreadAdjustment.SellCcy);

            var model = new ClientCompanyOnlineSpreadAdjustment()
            {
                ClientCompanyOnlineDetailsId = clientCompanyOnlineDetails.Id,
                Currency1Id = currency1,
                Currency2Id = currency2,
                IsBuy = spreadAdjustment.IsBuy,
                Spread = spreadAdjustment.SpreadAdjustment,
                ExpirationDateTime = DateTime.UtcNow.AddMinutes(spreadAdjustmentValidity),
                UpdatedByAuthUserId = spreadAdjustment.UpdatedByAuthUserId,
                UpdatedDateTime = DateTime.UtcNow,
            };
            _clientCompanyUow.AddClientCompanyOnlineSpreadAdjustment(model);
        }

        /// <summary>
        /// Set ClientCompanyOnlineDetails Kicked flag to true
        /// </summary>
        /// <param name="clientCompanyID"></param>
        public void SetKicked(int clientCompanyID)
        {
            _clientCompanyUow.SetClientCompanyOnlineKicked(clientCompanyID);
        }

        /// <summary>
        /// Determines if the trade should be executed based on the spread set by the company
        /// In case there is an active temporary spread the trade can be executed
        /// Otherwise the CSR user must wait for the Trader site user to adjust the spread,
        /// cancel the spread adjusting or the adjust spread counter to time out
        /// </summary>
        /// <param name="clientCompanyID"></param>
        /// <param name="currency1"></param>
        /// <param name="currency2"></param>
        /// <param name="isBuyDirection"></param>
        /// <returns>True if there is a temporary spread for the given filters</returns>
        public bool GetTradeExecutionStatusBySpread(int clientCompanyID, string currency1, string currency2, bool isBuyDirection)
        {
            int currency1Id = _currencyService.GetCurrencyId(currency1);
            int currency2Id = _currencyService.GetCurrencyId(currency2);

            List<DataAccess.Entities.ClientCompanyOnlineSpreadAdjustment> list =
                _clientCompanyUow.GetClientCompanyOnlineSpreadAdjustment(clientCompanyID, currency1Id, currency2Id, isBuyDirection)
                .Where(x => x.ExpirationDateTime.Subtract(DateTime.UtcNow).TotalMinutes >= 0).ToList();

            return list != null && list.Count > 0;
        }

        /// <summary>
        /// Get the spread for the given company
        /// Order in which the spread is searched for: Temporary active spread, skew spread, default spread
        /// </summary>
        public int GetClientCompanySpread(int clientCompanyID, string currency1, string currency2, bool isBuy,
            DateTime valueDate, DateTime contractDate)
        {
            int spread = 0;

            //Temporary active spread 
            int currency1Id = _currencyService.GetCurrencyId(currency1);
            int currency2Id = _currencyService.GetCurrencyId(currency2);

            List<ClientCompanyOnlineSpreadAdjustment> list = _clientCompanyUow.GetClientCompanyOnlineSpreadAdjustment(clientCompanyID, currency1Id, currency2Id, isBuy)
                .Where(x => x.ExpirationDateTime.Subtract(DateTime.UtcNow).TotalMinutes >= 0).ToList();

            if (list != null && list.Count > 0)
            {
                //get the newest active temporary spread
                spread = list[0].Spread;
                return spread;
            }

            //Skew spread
            DataAccess.Entities.ClientCompanyOnlineDetailsSkew clientCompanyOnlineDetailsSkew =
                _clientCompanyUow.GetClientCompanyOnlineDetailsSkew(clientCompanyID, currency1Id, currency2Id, isBuy).FirstOrDefault();

            if (clientCompanyOnlineDetailsSkew != null)
            {
                spread = clientCompanyOnlineDetailsSkew.Spread;
                return spread;
            }

            //Default spread
            DataAccess.Entities.ClientCompanyOnlineDetails clientCompanyOnlineDetails = GetClientCompanyOnlineDetails(clientCompanyID);
            if (clientCompanyOnlineDetails != null && clientCompanyOnlineDetails.AllowOnlineTrading)
            {
                int spotSpread = clientCompanyOnlineDetails.SpotSpread ?? 0;
                int fwSpread = clientCompanyOnlineDetails.FwdSpread ?? 0;

                //determine which spread to use
                /*
                    SPOT if the Value Date is 2, 1 or 0 business days away from the Contract Date 
                    FORWARD if the Value Date further than 2, 1 or 0 business days away from the Contract Date
                */

                if (IsForward(contractDate, valueDate))
                {
                    spread = fwSpread;
                }
                else
                {
                    spread = spotSpread;
                }

                return spread;
            }

            //in case there are no spread details return the default spread value 0
            return spread;
        }

        public DataAccess.Entities.ClientCompanyOnlineDetails GetClientCompanyOnlineDetails(int clientCompanyId)
        {
            return _clientCompanyUow.GetClientCompanyOnlineDetails(clientCompanyId).FirstOrDefault();
        }

        public ClientCompanyOnlineDetailsModel GetClientCompanyOnlineDetailsModel(int clientCompanyId)
        {
            var result = _clientCompanyUow.GetClientCompanyOnlineDetails(clientCompanyId)
                .Select(e => new ClientCompanyOnlineDetailsModel
                {
                    Id = e.Id,
                    ClientCompanyId = e.ClientCompanyId,
                    AllowOnlineTrading = e.AllowOnlineTrading,
                    MaxTradeSize = e.MaxTradeSize,
                    MaxOpen = e.MaxOpen,
                    MaxTenor = e.MaxTenor,
                    Collateral = e.Collateral,
                    SpotSpread = e.SpotSpread,
                    FwdSpread = e.FwdSpread,
                    Kicked = e.Kicked,
                    DealerFullName = string.Empty,
                    DealerPhoneNumber = string.Empty

                }).FirstOrDefault();

            if(result == null)
            {
                result = new ClientCompanyOnlineDetailsModel();
            }
            
            var company = _clientCompanyUow.GetClientCompany(clientCompanyId).FirstOrDefault();
            if (company != null && company.DealerAppUserId != null)
            {
                var dealerAppUser = _userService.GetFXDBAppUserById((int)company.DealerAppUserId);
                
                if (dealerAppUser != null)
                {
                    result.DealerFullName = dealerAppUser.FullName;
                    // TODO add country code in CSR Core first
                    //result.DealerPhoneNumber = $"+{dealerAppUser.TelephoneCountryCode} {dealerAppUser.TelephoneNumber}";
                    result.DealerPhoneNumber = $"{dealerAppUser.TelephoneNumber}";
                }
            }
            if (string.IsNullOrEmpty(result.DealerPhoneNumber))
            {
                // TODO change to appsettings
                result.DealerPhoneNumber = _config.Get("Phones:ArgentexSupport");
            }

            return result;
        }

        public ClientCompanyAccountModel GetClientCompanyDefaultAccount(int clientCompanyId, int currencyId)
        {
            return _clientCompanyUow
                 .GetClientCompanyAccounts(clientCompanyId)
                 .Where(x => x.Authorised && x.CurrencyId == currencyId && x.ClientCompanyCurrencyDefaultOpi
                                 .Select(y => y.ClientCompanyOpiid)
                                 .Contains(x.Id))
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
                                    .Select(y => y.ClientCompanyOpiid)
                                    .Contains(x.Id),
                     Approved = x.Authorised,
                     BeneficiaryName = x.BeneficiaryName,
                     BeneficiaryAddress = x.BeneficiaryAddress,
                     Reference = x.Reference,
                     UpdatedByAuthUserId = x.UpdatedByAuthUserId
                 })
                 .FirstOrDefault();            
        }

        public async Task<IEnumerable<ContactCategoryModel>> GetContactCategories()
        {
            return await _clientCompanyUow.GetContactCategories()
                .Select(x => new ContactCategoryModel()
                {
                    Id = x.Id,
                    Description = x.Description,
                    Sequence = x.Sequence

                }).ToListAsync();
        }

        public bool AddContactCategory(ContactCategoryModel model)
        {
          var contactCategory = _clientCompanyUow.GetContactCategory(model.Description).FirstOrDefault();
          //the contact category already exists
          if (contactCategory != null) return false;

          _clientCompanyUow.AddContactCategory(new ContactCategory() { Description = model.Description });
          return true;
        }

        public ContactCategory GetContactCategory(int contactCategoryId)
        {
            return _clientCompanyUow.GetContactCategory(contactCategoryId).FirstOrDefault();
        }

        public ContactCategory GetContactCategory(string contactCategoryDescription)
        {
            return _clientCompanyUow.GetContactCategory(contactCategoryDescription).FirstOrDefault();
        }

        public bool ProcessClientCompanyContactCategories(ClientCompanyContactBulkCategoryModel model)
        {
            List<int> existingClientCompanyContactCategoryIds = _clientCompanyUow
                .GetClientCompanyContactCategories(model.ClientCompanyContactId)
                .Select(x => x.ContactCategoryId).ToList();

            List<int> newClientCompanyContactCategoryIds = model.ContactCategoryIds.ToList();
            
            List<int> unassignClientCompanyContactCategoryIds = existingClientCompanyContactCategoryIds
                .Except(newClientCompanyContactCategoryIds).ToList();


            List<int> assignClientCompanyContactCategoryIds = newClientCompanyContactCategoryIds
                .Except(existingClientCompanyContactCategoryIds).ToList();


            return _clientCompanyUow.ProcessClientCompanyContactCategories(unassignClientCompanyContactCategoryIds,
                assignClientCompanyContactCategoryIds,
                model.ClientCompanyContactId, model.CreatedByAuthUserId);
        }

        public async Task<IEnumerable<ClientCompanyContactCategoryModel>> GetClientCompanyContactCategories(int clientCompanyContactId)
        {
            return await _clientCompanyUow.GetClientCompanyContactCategories(clientCompanyContactId)
                .Select(x => new ClientCompanyContactCategoryModel()
                {
                    ClientCompanyContactId = x.ClientCompanyContactId,
                    ContactCategoryId = x.ContactCategoryId,
                    ContactCategoryDescription = x.ContactCategory.Description
                }).ToListAsync();
        }

        public ClientCompanyContactListResponseModel GetCompanyContactList(int clientCompanyId)
        {
            var applicationServiceUserList = _clientCompanyUow.GetClientCompanyContactList(clientCompanyId)
                .Select(x => new ClientCompanyContactList
                {
                    ID = x.Id,
                    ContactTitle = x.Title,
                    ContactForename = x.Forename,
                    ContactSurname = x.Surname,
                    ContactEmail = x.Email,
                    ClientCompanyId = x.ClientCompanyId,
                    Position = x.Position,
                    FullName = x.Fullname,
                    Authorized = x.Authorized,
                    PrimaryContact = x.PrimaryContact.GetValueOrDefault(),
                }).OrderByDescending(x => x.PrimaryContact).ThenBy(x => x.ContactSurname);

            ClientCompanyContactListResponseModel responseModel = new ClientCompanyContactListResponseModel()
            {
                CompanyContactListModel = applicationServiceUserList.ToList(),
                Succeeded = true
            };

            return responseModel;
        }

        public ClientCompanyContactListResponseModel GetErrorMessagesForContactList(HttpStatusCode statusCode, Exception exception, int clientCompanyId)
        {
            ClientCompanyContactListResponseModel responseModel = new ClientCompanyContactListResponseModel();

            switch (statusCode)
            {
                case HttpStatusCode.BadRequest:
                    responseModel.ResponseMessages = new Dictionary<string, string[]>
                    {
                        {
                            "Errors", new string[] {$"List of contacts could not be retrieved for Client Company with ID {clientCompanyId}. {exception?.Message}" }
                        }
                    };
                    break;
            }

            return responseModel;
        }

        private ClientCompanyModel MapClientCompany(ClientCompany clientCompany)
        {
            return new ClientCompanyModel()
            {
                ID = clientCompany.Id,
                Name = clientCompany.Name,
                Crn = clientCompany.Crn,
                DealerAppUserID = clientCompany.DealerAppUserId,
                Description = clientCompany.Description,
                TradingName = clientCompany.TradingName,
                TelephoneNumber = clientCompany.TelephoneNumber,
                FaxNumber = clientCompany.FaxNumber,
                WebsiteURL = clientCompany.WebsiteUrl,
                Address = clientCompany.Address,
                ClientCompanyTypeID = clientCompany.ClientCompanyTypeId,
                ClientCompanyStatusID = clientCompany.ClientCompanyStatusId,
                UpdatedByAuthUserID = clientCompany.UpdatedByAuthUserId,
                UpdatedDateTime = clientCompany.UpdatedDateTime,
                ImportantNote = clientCompany.ImportantNote,
                ClientCompanyCategoryID = clientCompany.ClientCompanyCategoryId,
                IsHouseAccount = clientCompany.IsHouseAccount,
                PostCode = clientCompany.PostCode,
                ApprovedDateTime = clientCompany.ApprovedDateTime,
                IsKYC = clientCompany.IsKyc,
                IsTandCs = clientCompany.IsTandCs,
                IsRiskWarning = clientCompany.IsRiskWarning,
                ClientCompanyOptionStatusID = clientCompany.ClientCompanyStatusId,
                ApprovedOptionDateTime = clientCompany.ApprovedOptionDateTime,
                IsPitched = clientCompany.IsPitched,
                PitchedByAppUserID = clientCompany.PitchedByAppUserId,
                PitchedDateTime = clientCompany.PitchedDateTime,
                AccountFormsSentDateTime = clientCompany.AccountFormsSentDateTime,
                IsInternalAccount = clientCompany.IsInternalAccount,
                QualifiedNewTradeCode = clientCompany.QualifiedNewTradeCode,
                TradingAddress = clientCompany.TradingAddress,
                MaxOpenGBP = clientCompany.MaxOpenGbp,
                MaxTradeSizeGBP = clientCompany.MaxTradeSizeGbp,
                MaxTenorMonths = clientCompany.MaxTenorMonths,
                MaxCreditLimit = clientCompany.MaxCreditLimit,
                TradingPostCode = clientCompany.TradingPostCode,
                EMIR_LEI = clientCompany.EmirLei,
                EMIR_EEA = clientCompany.EmirEea,
                AssignNewTrades = clientCompany.AssignNewTrades,
                ClientCompanyIndustrySectorID = clientCompany.ClientCompanyIndustrySectorId,
                ClientCompanySalesRegionID = clientCompany.ClientCompanySalesRegionId,
                SpreadsNote = clientCompany.SpreadsNote,
                ClientCompanyLinkedGroupID = clientCompany.ClientCompanyLinkedGroupId,
                IsExcludedFromEMoney = clientCompany.IsExcludedFromEmoney,
                FirstTradeDate = clientCompany.FirstTradeDate,
                ClientCompanyCreditTypeID = clientCompany.ClientCompanyCreditTypeId
            };
        }

        private static bool IsForward(DateTime contractDate, DateTime valueDate)
        {
            var dayDifference = (int)valueDate.Date.Subtract(contractDate.Date).TotalDays;

            if (dayDifference < 0)
                return true;

            int workingDays = Enumerable
                .Range(1, dayDifference)
                .Select(x => contractDate.AddDays(x))
                .Count(x => x.DayOfWeek != DayOfWeek.Saturday && x.DayOfWeek != DayOfWeek.Sunday);

            return workingDays > 2;
        }
        /// <summary>
        /// disposing == true coming from Dispose()
        /// disposing == false coming from finaliser
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _clientCompanyUow?.Dispose();
                    _currencyService?.Dispose();
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
