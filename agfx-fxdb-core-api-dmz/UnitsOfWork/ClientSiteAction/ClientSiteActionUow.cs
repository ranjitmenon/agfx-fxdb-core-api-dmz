using Synetec.Data.UnitOfWork.BaseUnitOfWork;
using Synetec.Data.UnitOfWork.GenericRepo;
using Argentex.Core.DataAccess.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Argentex.Core.UnitsOfWork.ClientSiteAction
{
    public class ClientSiteActionUow : BaseUow, IClientSiteActionUow
    {
        private IGenericRepo<DataAccess.Entities.ClientSiteAction> _clientSiteActionRepo;
        private IGenericRepo<ClientSiteActionStatus> _clientSiteActionStatusRepo;
        private IGenericRepo<ClientSiteActionType> _clientSiteActionTypeRepo;

        private IGenericRepo<ClientSiteAction2FxforwardTrade2Opi> _clientSiteAction2FxforwardTrade2OpiRepo;
        private IGenericRepo<ClientSiteAction2Fxswap> _clientSiteAction2FxswapRepo;
        private IGenericRepo<ClientSiteAction2ClientCompanyOpi> _clientSiteAction2ClientCompanyOpiRepo;
        private IGenericRepo<ClientSiteAction2FixFxforwardTrade> _clientSiteAction2FixFxforwardTradeRepo;

        public ClientSiteActionUow(FXDB1Context context) : base(context)
        {
        }

        #region Repo Initializing

        private IGenericRepo<DataAccess.Entities.ClientSiteAction> ClientSiteActionRepo
        {
            get { return _clientSiteActionRepo = _clientSiteActionRepo ?? new GenericRepo<DataAccess.Entities.ClientSiteAction>(Context); }
        }

        private IGenericRepo<ClientSiteActionStatus> ClientSiteActionStatusRepo
        {
            get { return _clientSiteActionStatusRepo = _clientSiteActionStatusRepo ?? new GenericRepo<ClientSiteActionStatus>(Context); }
        }

        private IGenericRepo<ClientSiteActionType> ClientSiteActionTypeRepo
        {
            get { return _clientSiteActionTypeRepo = _clientSiteActionTypeRepo ?? new GenericRepo<ClientSiteActionType>(Context); }
        }

        private IGenericRepo<ClientSiteAction2ClientCompanyOpi> ClientSiteAction2ClientCompanyOpiRepo
        {
            get { return _clientSiteAction2ClientCompanyOpiRepo = _clientSiteAction2ClientCompanyOpiRepo ?? new GenericRepo<ClientSiteAction2ClientCompanyOpi>(Context); }
        }

        private IGenericRepo<ClientSiteAction2FixFxforwardTrade> ClientSiteAction2FixFxforwardTradeRepo
        {
            get { return _clientSiteAction2FixFxforwardTradeRepo = _clientSiteAction2FixFxforwardTradeRepo ?? new GenericRepo<ClientSiteAction2FixFxforwardTrade>(Context); }
        }

        private IGenericRepo<ClientSiteAction2FxforwardTrade2Opi> ClientSiteAction2FxforwardTrade2OpiRepo
        {
            get { return _clientSiteAction2FxforwardTrade2OpiRepo = _clientSiteAction2FxforwardTrade2OpiRepo ?? new GenericRepo<ClientSiteAction2FxforwardTrade2Opi>(Context); }
        }

        private IGenericRepo<ClientSiteAction2Fxswap> ClientSiteAction2FxswapRepo
        {
            get { return _clientSiteAction2FxswapRepo = _clientSiteAction2FxswapRepo ?? new GenericRepo<ClientSiteAction2Fxswap>(Context); }
        }

        #endregion

        public IQueryable<DataAccess.Entities.ClientSiteAction> GetClientSiteAction(long clientSiteActionID)
        {
            return ClientSiteActionRepo.GetQueryable(x => x.Id == clientSiteActionID, orderBy: null, 
                includeProperties: "CreatedByAuthUser,UpdatedByAuthUser,ClientSiteActionType,ClientSiteActionStatus");
        }

        public IQueryable<ClientSiteAction2ClientCompanyOpi> GetClientSiteActionByOPIID(int clientCompanyOPIID)
        {
            return ClientSiteAction2ClientCompanyOpiRepo.GetQueryable(x => x.ClientCompanyOpiid == clientCompanyOPIID)
                .Include(x => x.ClientSiteAction)
                .Include(x => x.ClientSiteAction.ClientSiteActionStatus)
                .Include(x => x.ClientSiteAction.ClientSiteActionType)
                .Include(x => x.ClientSiteAction.CreatedByAuthUser)
                .Include(x => x.ClientSiteAction.UpdatedByAuthUser);
        }

        public void LogAction(DataAccess.Entities.ClientSiteAction action, string Id)
        {
            ClientSiteActionRepo.Insert(action);

            switch (action.ClientSiteActionType.Name)
            {
                case "RequestSwap":
                    var Csa2Swap = new ClientSiteAction2Fxswap
                    {
                        FxswapId = int.Parse(Id),
                        ClientSiteActionId = action.Id
                    };

                    ClientSiteAction2FxswapRepo.Insert(Csa2Swap);

                    break;
                case "RequestOPIAssignedToTrades":
                    var Csa2Pay = new ClientSiteAction2FxforwardTrade2Opi
                    {
                        FxforwardTrade2Opiid = long.Parse(Id),
                        ClientSiteActionId = action.Id
                    };

                    ClientSiteAction2FxforwardTrade2OpiRepo.Insert(Csa2Pay);

                    break;
                case "RequestNewOPI":
                    var Csa2Opi = new ClientSiteAction2ClientCompanyOpi
                    {
                        ClientCompanyOpiid = int.Parse(Id),
                        ClientSiteActionId = action.Id
                    };

                    ClientSiteAction2ClientCompanyOpiRepo.Insert(Csa2Opi);

                    break;
                case "RequestTradesNoFIXConfirmation":
                    var Csa2FixTrade = new ClientSiteAction2FixFxforwardTrade
                    {
                        FxforwardTradeCode = Id,
                        ClientSiteActionId = action.Id
                    };

                    ClientSiteAction2FixFxforwardTradeRepo.Insert(Csa2FixTrade);

                    break;
                default:
                    break;
            }

            SaveContext();
        }

        public ClientSiteActionStatus GetClientSiteActionStatusFromName(string actionStatusName)
        {
            var actionStatus = ClientSiteActionStatusRepo.GetQueryable(x => x.Name == actionStatusName)
                .FirstOrDefault();

            return actionStatus;
        }

        public ClientSiteActionType GetClientSiteActionTypeFromName(string actionTypeName)
        {
            var actionType = ClientSiteActionTypeRepo.GetQueryable(x => x.Name == actionTypeName)
                .FirstOrDefault();

            return actionType;
        }

        public IQueryable<ClientSiteAction2FxforwardTrade2Opi> GetOPIsAssignedToTrades()
        {
            const string ClientSiteAction_Type_OpisAssignedToTrades = "RequestOPIAssignedToTrades";

            var actionType = GetClientSiteActionTypeFromName(ClientSiteAction_Type_OpisAssignedToTrades);

            return ClientSiteAction2FxforwardTrade2OpiRepo
                .GetQueryable(x => x.ClientSiteAction.ClientSiteActionType.Id == actionType.Id)
                .Include(x => x.ClientSiteAction)
                .Include(x => x.ClientSiteAction.ClientSiteActionStatus)
                .Include(x => x.ClientSiteAction.ClientSiteActionType)
                .Include(x => x.FxforwardTrade2Opi)
                .Include(x => x.FxforwardTrade2Opi.FxforwardTradeCodeNavigation)
                .Include(x => x.FxforwardTrade2Opi.ClientCompanyOpi)
                .Include(x => x.FxforwardTrade2Opi.ClientCompanyOpi.ClientCompany);
        }

        public IQueryable<ClientSiteAction2ClientCompanyOpi> GetNewOPIRequested()
        {
            const string ClientSiteAction_Type_RequestNewOPI = "RequestNewOPI";

            var actionType = GetClientSiteActionTypeFromName(ClientSiteAction_Type_RequestNewOPI);

            return ClientSiteAction2ClientCompanyOpiRepo
                .GetQueryable(x => x.ClientSiteAction.ClientSiteActionType.Id == actionType.Id)
                .Include(x => x.ClientSiteAction)
                .Include(x => x.ClientSiteAction.ClientSiteActionStatus)
                .Include(x => x.ClientSiteAction.ClientSiteActionType)
                .Include(x => x.ClientCompanyOpi);
        }

        public IQueryable<ClientSiteAction2FixFxforwardTrade> GetTradesWithoutFIXConfirmation()
        {
            const string ClientSiteAction_Type_NoFIXConfirmation = "RequestTradesNoFIXConfirmation";

            var actionType = GetClientSiteActionTypeFromName(ClientSiteAction_Type_NoFIXConfirmation);

            return ClientSiteAction2FixFxforwardTradeRepo
                .GetQueryable(x=>x.ClientSiteAction.ClientSiteActionType.Id == actionType.Id)
                .Include(x => x.ClientSiteAction)
                .Include(x => x.ClientSiteAction.ClientSiteActionStatus)
                .Include(x => x.ClientSiteAction.ClientSiteActionType)
                .Include(x => x.FxforwardTradeCodeNavigation);
        }

        public IQueryable<ClientSiteAction2Fxswap> GetSwaps()
        {
            const string ClientSiteAction_Type_SwapCreation = "RequestSwap";

            var actionType = GetClientSiteActionTypeFromName(ClientSiteAction_Type_SwapCreation);

            return ClientSiteAction2FxswapRepo
                .GetQueryable(x => x.ClientSiteAction.ClientSiteActionType.Id == actionType.Id)
                .Include(x => x.ClientSiteAction)
                .Include(x => x.ClientSiteAction.ClientSiteActionStatus)
                .Include(x => x.ClientSiteAction.ClientSiteActionType)
                .Include(x => x.Fxswap)
                .Include(x => x.Fxswap.ParentTradeCodeNavigation)
                .Include(x => x.Fxswap.ParentTradeCodeNavigation.ClientCompanyNavigation)
                .Include(x => x.Fxswap.DeliveryLegTradeCodeNavigation)
                .Include(x => x.Fxswap.ReversalLegTradeCodeNavigation)
                .Include(x => x.ClientSiteAction.CreatedByAuthUser.ClientCompanyContactAuthUser);
        }

        public void UpdateClientSiteAction(DataAccess.Entities.ClientSiteAction action)
        {
            var actionEntity = ClientSiteActionRepo.GetByPrimaryKey(action.Id);

            actionEntity.UpdatedByAuthUserId = action.UpdatedByAuthUserId;
            actionEntity.UpdatedDateTime = action.UpdatedDateTime;
            actionEntity.ClientSiteActionStatusId = action.ClientSiteActionStatusId;

            ClientSiteActionRepo.Update(actionEntity);
            SaveContext();
        }

        public void DeleteAction2AssignedSettlementLink(long settlementId)
        {
            var csa2Trade2Opi = ClientSiteAction2FxforwardTrade2OpiRepo
                .GetQueryable(x => x.FxforwardTrade2Opiid == settlementId)
                .Select(x => new ClientSiteAction2FxforwardTrade2Opi
                {
                    Id = x.Id,
                    ClientSiteActionId = x.ClientSiteActionId,
                    FxforwardTrade2Opiid = x.FxforwardTrade2Opiid
                }).SingleOrDefault();

            // Only delete the client site action if it was found
            // Client site actions are just created from client site, if opi was assigned from
            // trader site then client site action is not created.
            if (csa2Trade2Opi != null)
            {
                ClientSiteAction2FxforwardTrade2OpiRepo.Delete(csa2Trade2Opi);
                SaveContext();
            }
        }
    }
}
