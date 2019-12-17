using Argentex.Core.DataAccess.Entities;
using Synetec.Data.UnitOfWork.BaseUnitOfWork;
using Synetec.Data.UnitOfWork.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Argentex.Core.UnitsOfWork.ClientCompanyContacts.Model;

namespace Argentex.Core.UnitsOfWork.ClientCompanyContacts
{
    public interface IClientCompanyUow : IBaseUow
    {
        IGenericRepo<ClientCompanyContact> ClientCompanyContactRepository { get; }
        IQueryable<ClientCompany> GetClientCompany(int clientCompanyId);
        IQueryable<ClientCompanyOpi> GetClientCompanyAccounts(int clientCompanyId);
        IQueryable<ClientCompanyContact> GetClientCompanyContact(int clientCompanyId);
        IQueryable<ClientCompany> GetClientCompanies();
        void UpdateCompanyQualifiedTradeDetails(int clientCompanyId, string qualifiedTradeCode, int authUserId);
        void UpdateCompanyFirstTradeDate(int clientCompanyId, int authUserId);
        void UpdateCompanyLastContractDate(int clientCompanyId, DateTime? contractDate, int authUserId);

        IQueryable<ClientCompanyOnlineDetails> GetClientCompanyOnlineDetails(int clientCompanyId);
        IQueryable<ClientCompanyOnlineDetailsSkew> GetClientCompanyOnlineDetailsSkew(int clientCompanyId, int currency1Id, int currency2Id, bool isBuy);
        IQueryable<ClientCompanyOnlineSpreadAdjustment> GetClientCompanyOnlineSpreadAdjustment(int clientCompanyId, int currency1Id, int currency2Id, bool isBuy);

        void AddClientCompanyOnlineSpreadAdjustment(ClientCompanyOnlineSpreadAdjustment model);
        void SetClientCompanyOnlineKicked(int clientCompanyId);
        IQueryable<ClientCompanyContactCategory> GetClientCompanyContactCategories(int clientCompanyContactId);
        IQueryable<ContactCategory> GetContactCategories();
        
        void AddContactCategory(ContactCategory entity);
        IQueryable<ContactCategory> GetContactCategory(int contactCategoryId);
        IQueryable<ContactCategory> GetContactCategory(string contactCategoryDescription);

        bool ProcessClientCompanyContactCategories(List<int> unassignClientCompanyContactCategoryIds, List<int> assignClientCompanyContactCategoryIds, int modelClientCompanyContactId, int modelCreatedByAuthUserId);
        IQueryable<ClientCompanyContact> GetClientCompanyContactList(int clientCompanyID);
        ClientCompanyContact GetCurrentClientCompanyContact(ClientCompanyContactSearchModel clientCompanyContactSearchContext);
    }
}
