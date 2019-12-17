using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Service.Models.ClientSiteAction;
using System;
using System.Collections.Generic;

namespace Argentex.Core.Service.ClientSiteAction
{
    public interface IClientSiteActionService : IDisposable
    {
        ClientSiteActionModel GetClientSiteAction(long clientSiteActionID);
        void LogActionOpiPayment(int authUserId, long fxforwardTrade2OpiId, string accountName, decimal amount);
        void LogActionSwapCreation(int authUserId, int FxswapId);
        void LogActionUnconfirmedTrade(int authUserId, string tradeCode);
        void LogActionNewOpi(int authUserId, int newOpiId);
        IEnumerable<CSATradesWithoutFIXConfirmationModel> GetTradesWithoutFIXConfirmation();
        IEnumerable<CSAOPIsAssignedToTradesDisplayModel> GetOPIsAssignedToTrades();
        void UpdateClientSiteAction(ClientSiteActionModel model);
        ClientSiteActionStatus GetClientSiteActionStatus(string name);
        IEnumerable<CSANewOPIRequestDisplayModel> GetNewOPIRequested();
        ClientSiteActionModel GetClientSiteActionByOPIID(int clientCompanyOPIID);
        IEnumerable<CSASwapsModel> GetSwaps();
        void DeleteAction2AssignedSettlementLink(long settlementId);
    }
}
