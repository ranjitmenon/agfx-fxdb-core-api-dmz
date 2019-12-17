using Argentex.Core.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Synetec.Data.UnitOfWork.BaseUnitOfWork;
using Synetec.Data.UnitOfWork.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using SynetecLogger;
using Argentex.Core.UnitsOfWork.ClientCompanyContacts.Model;

namespace Argentex.Core.UnitsOfWork.ClientCompanyContacts
{
    public class ClientCompanyUow : BaseUow, IClientCompanyUow
    {
        private IGenericRepo<ClientCompanyContact> _clientCompanyContactRepository;
        private IGenericRepo<ClientCompany> _clientCompanyRepository;
        private IGenericRepo<ClientCompanyOpi> _clientCompanyOpiRepository;
        private IGenericRepo<ClientCompanyOnlineDetails> _clientCompanyOnlineDetailsRepository;
        private IGenericRepo<ClientCompanyOnlineDetailsSkew> _clientCompanyOnlineDetailsSkewRepository;
        private IGenericRepo<ClientCompanyOnlineSpreadAdjustment> _clientCompanyOnlineSpreadAdjustmentRepository;
        private IGenericRepo<ClientCompanyContactCategory> _clientCompanyContactCategoryRepository;
        private IGenericRepo<ContactCategory> _contactCategoryRepository;
        private IGenericRepo<LogClientCompanyContact> _logClientCompanyContactRepository;
        private IGenericRepo<LogClientCompanyContactCategory> _logClientCompanyContactCategoryRepository;

        private readonly ILogWrapper _logger;

        private IGenericRepo<ClientCompany> ClientCompanyRepository =>
            _clientCompanyRepository = _clientCompanyRepository ?? new GenericRepo<ClientCompany>(Context);

        private IGenericRepo<ClientCompanyOpi> ClientCompanyOpiRepository =>
    _clientCompanyOpiRepository = _clientCompanyOpiRepository ?? new GenericRepo<ClientCompanyOpi>(Context);

        public IGenericRepo<ClientCompanyContact> ClientCompanyContactRepository =>
            _clientCompanyContactRepository = _clientCompanyContactRepository ?? new GenericRepo<ClientCompanyContact>(Context);

        private IGenericRepo<ClientCompanyOnlineDetails> ClientCompanyOnlineDetailsRepository =>
            _clientCompanyOnlineDetailsRepository = _clientCompanyOnlineDetailsRepository ?? new GenericRepo<ClientCompanyOnlineDetails>(Context);

        private IGenericRepo<ClientCompanyOnlineDetailsSkew> ClientCompanyOnlineDetailsSkewRepository =>
            _clientCompanyOnlineDetailsSkewRepository = _clientCompanyOnlineDetailsSkewRepository ?? new GenericRepo<ClientCompanyOnlineDetailsSkew>(Context);

        private IGenericRepo<ClientCompanyOnlineSpreadAdjustment> ClientCompanyOnlineSpreadAdjustmentRepository =>
            _clientCompanyOnlineSpreadAdjustmentRepository = _clientCompanyOnlineSpreadAdjustmentRepository ?? new GenericRepo<ClientCompanyOnlineSpreadAdjustment>(Context);

        private IGenericRepo<ClientCompanyContactCategory> ClientCompanyContactCategoryRepository =>
            _clientCompanyContactCategoryRepository = _clientCompanyContactCategoryRepository ?? new GenericRepo<ClientCompanyContactCategory>(Context);

        private IGenericRepo<ContactCategory> ContactCategoryRepository =>
            _contactCategoryRepository = _contactCategoryRepository ?? new GenericRepo<ContactCategory>(Context);

        public IGenericRepo<LogClientCompanyContact> LogClientCompanyContactRepository =>
            _logClientCompanyContactRepository = _logClientCompanyContactRepository ?? new GenericRepo<LogClientCompanyContact>(Context);

        public IGenericRepo<LogClientCompanyContactCategory> LogClientCompanyContactCategoryRepository =>
            _logClientCompanyContactCategoryRepository = _logClientCompanyContactCategoryRepository ?? new GenericRepo<LogClientCompanyContactCategory>(Context);

        public ClientCompanyUow(FXDB1Context context) : base (context)
        {

        }
        public ClientCompanyUow(FXDB1Context context, ILogWrapper logger) : base(context)
        {
            _logger = logger;
        }
        
        public IQueryable<ClientCompany> GetClientCompany(int clientCompanyId)
        {
            return ClientCompanyRepository
                .GetQueryable(x => x.Id == clientCompanyId);
        }

        public IQueryable<ClientCompany> GetClientCompanies()
        {
            return ClientCompanyRepository.Get().AsQueryable();
                
        }

        public IQueryable<ClientCompanyOpi> GetClientCompanyAccounts(int clientCompanyId)
        {
            return ClientCompanyOpiRepository
                .GetQueryable(x => x.ClientCompanyId == clientCompanyId && !x.IsDeleted);
        }

        public IQueryable<ClientCompanyContact> GetClientCompanyContact(int clientCompanyId)
        {
            return ClientCompanyContactRepository
                .GetQueryable(x => x.ClientCompanyId == clientCompanyId);
        }

        public void UpdateCompanyQualifiedTradeDetails(int clientCompanyId, string qualifiedTradeCode, int authUserId)
        {
            ClientCompany clientCompany = GetClientCompany(clientCompanyId).SingleOrDefault();

            clientCompany.QualifiedNewTradeCode = qualifiedTradeCode;
            clientCompany.UpdatedByAuthUserId = authUserId;
            clientCompany.UpdatedDateTime = DateTime.Now;

            ClientCompanyRepository.Update(clientCompany);
            SaveContext();
        }

        public void UpdateCompanyFirstTradeDate(int clientCompanyId, int authUserId)
        {
            ClientCompany clientCompany = GetClientCompany(clientCompanyId).SingleOrDefault();
            
            if (clientCompany.FirstTradeDate == null)
            {
                clientCompany.FirstTradeDate = DateTime.Now;
                clientCompany.UpdatedByAuthUserId = authUserId;
                clientCompany.UpdatedDateTime = DateTime.Now;

                ClientCompanyRepository.Update(clientCompany);
                SaveContext();
            }
        }

        public void UpdateCompanyLastContractDate(int clientCompanyId, DateTime? tradeContractDate, int authUserId)
        {
            ClientCompany clientCompany = GetClientCompany(clientCompanyId).SingleOrDefault();

            if (tradeContractDate.HasValue)
            {
                clientCompany.LastContractDate = tradeContractDate;
                clientCompany.UpdatedByAuthUserId = authUserId;
                clientCompany.UpdatedDateTime = DateTime.Now;

                ClientCompanyRepository.Update(clientCompany);
                SaveContext();
            }
        }

        public IQueryable<ClientCompanyOnlineDetails> GetClientCompanyOnlineDetails(int clientCompanyId)
        {
            return ClientCompanyOnlineDetailsRepository
                .GetQueryable(x => x.ClientCompanyId == clientCompanyId);
        }

        public IQueryable<ClientCompanyOnlineDetailsSkew> GetClientCompanyOnlineDetailsSkew(int clientCompanyId, int currency1Id, int currency2Id, bool isBuy)
        {
            return ClientCompanyOnlineDetailsSkewRepository
                .GetQueryable(x => x.ClientCompanyOnlineDetails.ClientCompanyId == clientCompanyId &&
                x.ClientCompanyOnlineDetails.AllowOnlineTrading &&
                x.Currency1Id == currency1Id &&
                x.Currency2Id == currency2Id &&
                x.IsBuy == isBuy, orderBy: null, includeProperties: "ClientCompanyOnlineDetails")
                .OrderByDescending(x => x.UpdatedDateTime); 
        }

        public IQueryable<ClientCompanyOnlineSpreadAdjustment> GetClientCompanyOnlineSpreadAdjustment(int clientCompanyId, int currency1Id, int currency2Id, bool isBuy)
        {
            return ClientCompanyOnlineSpreadAdjustmentRepository
            .GetQueryable(x => x.ClientCompanyOnlineDetails.ClientCompanyId == clientCompanyId &&
                            x.ClientCompanyOnlineDetails.AllowOnlineTrading &&
                            x.Currency1Id == currency1Id &&
                            x.Currency2Id == currency2Id &&
                            x.IsBuy == isBuy, orderBy: null, includeProperties: "ClientCompanyOnlineDetails")
            .OrderByDescending(x => x.UpdatedDateTime);
        }

        public void AddClientCompanyOnlineSpreadAdjustment(ClientCompanyOnlineSpreadAdjustment model)
        {
            ClientCompanyOnlineSpreadAdjustmentRepository.Insert(model);
            SaveContext();
        }

        public void SetClientCompanyOnlineKicked(int clientCompanyId)
        {
            var clientCompanyOnlineDetails = GetClientCompanyOnlineDetails(clientCompanyId).SingleOrDefault();

            clientCompanyOnlineDetails.Kicked = true;

            ClientCompanyOnlineDetailsRepository.Update(clientCompanyOnlineDetails);
            SaveContext();
        }

        public IQueryable<ClientCompanyContactCategory> GetClientCompanyContactCategories(int clientCompanyContactId)
        {
            return ClientCompanyContactCategoryRepository
                .GetQueryable(x => x.ClientCompanyContactId == clientCompanyContactId)
                .Include(x => x.ContactCategory.Description);
        }

        public IQueryable<ContactCategory> GetContactCategories()
        {
            return ContactCategoryRepository
                .GetQueryable();
        }

        public void AddContactCategory(ContactCategory entity)
        {
            ContactCategoryRepository.Insert(entity);
            SaveContext();
        }

        public IQueryable<ContactCategory> GetContactCategory(int contactCategoryId)
        {
            return ContactCategoryRepository.Get(x => x.Id == contactCategoryId).AsQueryable();
        }

        public IQueryable<ContactCategory> GetContactCategory(string contactCategoryDescription)
        {
            return ContactCategoryRepository.Get(x => x.Description == contactCategoryDescription).AsQueryable();
        }
        
        public bool ProcessClientCompanyContactCategories(List<int> unassignClientCompanyContactCategoryIds, List<int> assignClientCompanyContactCategoryIds, int clientCompanyContactId, int authUserId)
        {
            using (var transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    foreach (int unassignContactCategoryId in unassignClientCompanyContactCategoryIds)
                    {
                        LogClientCompanyContactCategoryRepository.Insert(new LogClientCompanyContactCategory()
                        {
                            LogAction = "UNASSIGN",
                            ClientCompanyContactId = clientCompanyContactId,
                            ContactCategoryId = unassignContactCategoryId,
                            DateCreated = DateTime.Now,
                            CreatedByAuthUserId = authUserId
                        });

                        ClientCompanyContactCategoryRepository.Delete(new ClientCompanyContactCategory()
                        {
                            ClientCompanyContactId = clientCompanyContactId,
                            ContactCategoryId = unassignContactCategoryId,
                        });
                    }

                    foreach (int assignContactCategoryId in assignClientCompanyContactCategoryIds)
                    {
                        ClientCompanyContactCategoryRepository.Insert(new ClientCompanyContactCategory()
                        {
                            ClientCompanyContactId = clientCompanyContactId,
                            ContactCategoryId = assignContactCategoryId,
                            DateCreated = DateTime.Now,
                            CreatedByAuthUserId = authUserId,
                        });

                        LogClientCompanyContactCategoryRepository.Insert(new LogClientCompanyContactCategory()
                        {
                            LogAction = "ASSIGN",
                            ClientCompanyContactId = clientCompanyContactId,
                            ContactCategoryId = assignContactCategoryId,
                            DateCreated = DateTime.Now,
                            CreatedByAuthUserId = authUserId
                        });
                    }

                    SaveContext();
                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.Error(ex);

                    return false;
                }
            }
        }

        public ClientCompanyContact GetCurrentClientCompanyContact(ClientCompanyContactSearchModel clientCompanyContactSearchContext)
        {
            ClientCompanyContact clientCompanyContact = new ClientCompanyContact();
            if (clientCompanyContactSearchContext != null)
            {
                // Check if ClientCompanyContactId is passed and get data by it, else get data by AuthUserId
                clientCompanyContact = clientCompanyContactSearchContext.ClientCompanyContactId != null ?
                    ClientCompanyContactRepository.GetQueryable().Include(c => c.AuthUser)
                    .Include(c => c.ClientCompany).SingleOrDefault(c => c.Id == clientCompanyContactSearchContext.ClientCompanyContactId)
                    : ClientCompanyContactRepository.GetQueryable().Include(c => c.AuthUser)
                    .Include(c => c.ClientCompany).SingleOrDefault(c => c.AuthUserId == clientCompanyContactSearchContext.AuthUsertId);
            }

            return clientCompanyContact;
        }

        public IQueryable<ClientCompanyContact> GetClientCompanyContactList(int clientCompanyID)
        {
            var clientCompanyContactList = ClientCompanyContactRepository.GetQueryable(x => x.ClientCompanyId == clientCompanyID && !x.IsDeleted);

            return clientCompanyContactList;
        }
    }
}
