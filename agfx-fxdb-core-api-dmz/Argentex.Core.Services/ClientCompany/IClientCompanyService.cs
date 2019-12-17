using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Service.Models.ClientCompany;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Argentex.Core.Service
{
    public interface IClientCompanyService : IDisposable
    {
        string GetClientCompanyName(int clientCompanyId);
        ClientCompanyModel GetClientCompany(int clientCompanyId);

        ICollection<ClientCompaniesModel> GetClientCompanies();
        ICollection<ClientCompanyAccountModel> GetClientCompanyAccounts(int clientCompanyId);
        ClientCompanyContactResponseModel GetClientCompanyContact(ClientCompanyContactSearchContext clientCompanyContactSearchContext);
        bool GetTradeExecutionStatusBySpread(int clientCompanyID, string currency1, string currency2, bool isBuyDirection);
        int GetClientCompanySpread(int clientCompanyID, string currency1, string currency2, bool isBuyDirection, DateTime valueDate, DateTime contractDate);
        ClientCompanyOnlineDetailsModel GetClientCompanyOnlineDetailsModel(int clientCompanyId);
        ClientCompanyOnlineDetails GetClientCompanyOnlineDetails(int clientCompanyId);
        void AddSpredAdjustment(SpreadAdjustmentModel model);
        void SetKicked(int clientCompanyID);
        ClientCompanyAccountModel GetClientCompanyDefaultAccount(int clientCompanyId, int currencyId);
        Task<IEnumerable<ClientCompanyContactCategoryModel>> GetClientCompanyContactCategories(int clientCompanyContactId);
        Task<IEnumerable<ContactCategoryModel>> GetContactCategories();
        
        bool AddContactCategory(ContactCategoryModel model);
        ContactCategory GetContactCategory(int contactCategoryId);
        ContactCategory GetContactCategory(string contactCategoryDescription);
        bool ProcessClientCompanyContactCategories(ClientCompanyContactBulkCategoryModel model);
        ClientCompanyContactListResponseModel GetCompanyContactList(int clientCompanyId);
        ClientCompanyContactResponseModel GetErrorMessages(HttpStatusCode statusCode, Exception exception, ClientCompanyContactSearchContext clientCompanyContactSearchContext);
        ClientCompanyContactListResponseModel GetErrorMessagesForContactList(HttpStatusCode statusCode, Exception exception, int clientCompanyId);
    }
}
