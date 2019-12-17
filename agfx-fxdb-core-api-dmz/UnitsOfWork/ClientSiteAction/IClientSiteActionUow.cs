using Argentex.Core.DataAccess.Entities;
using Synetec.Data.UnitOfWork.BaseUnitOfWork;
using System.Linq;

namespace Argentex.Core.UnitsOfWork.ClientSiteAction
{
    public interface IClientSiteActionUow: IBaseUow
    {
        IQueryable<DataAccess.Entities.ClientSiteAction> GetClientSiteAction(long clientSiteActionID);
        void LogAction(DataAccess.Entities.ClientSiteAction action, string Id);
        ClientSiteActionStatus GetClientSiteActionStatusFromName(string actionStatusName);
        ClientSiteActionType GetClientSiteActionTypeFromName(string actionTypeName);
        IQueryable<ClientSiteAction2FxforwardTrade2Opi> GetOPIsAssignedToTrades();
        IQueryable<ClientSiteAction2FixFxforwardTrade> GetTradesWithoutFIXConfirmation();
        void UpdateClientSiteAction(DataAccess.Entities.ClientSiteAction action);
        IQueryable<ClientSiteAction2ClientCompanyOpi> GetNewOPIRequested();
        IQueryable<ClientSiteAction2ClientCompanyOpi> GetClientSiteActionByOPIID(int clientCompanyOPIID);
        IQueryable<ClientSiteAction2Fxswap> GetSwaps();
        void DeleteAction2AssignedSettlementLink(long settlementId);
    }
}
