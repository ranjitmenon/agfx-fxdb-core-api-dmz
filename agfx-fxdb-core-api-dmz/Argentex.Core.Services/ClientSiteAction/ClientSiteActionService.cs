using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Service.AppSettings;
using Argentex.Core.Service.Helpers;
using Argentex.Core.Service.Models.ClientSiteAction;
using Argentex.Core.UnitsOfWork.ClientSiteAction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Argentex.Core.Service.ClientSiteAction
{
    public class ClientSiteActionService : IClientSiteActionService
    {
        private readonly IClientSiteActionUow _clientSiteActionUow;
        private readonly IAppSettingService _appSettingService;
        private bool _disposed;
                
        public ClientSiteActionService(IClientSiteActionUow clientSiteActionUow,
            IAppSettingService appSettingService)
        {
            _clientSiteActionUow = clientSiteActionUow;
            _appSettingService = appSettingService;
        }

        public ClientSiteActionModel GetClientSiteAction(long clientSiteActionID)
        {
            return _clientSiteActionUow.GetClientSiteAction(clientSiteActionID).Select(x => new ClientSiteActionModel()
            {
                ID = x.Id,
                ActionType = x.ClientSiteActionType.Name,
                ActionStatus = x.ClientSiteActionStatus.Name,
                Details = x.Details,
                CreatedByUser = x.CreatedByAuthUser.UserName,
                CreatedDateTime = x.CreatedDateTime,
                UpdatedByUser = x.UpdatedByAuthUser.UserName,
                UpdatedDateTime = x.UpdatedDateTime
            }).SingleOrDefault();
        }

        public ClientSiteActionModel GetClientSiteActionByOPIID(int clientCompanyOPIID)
        {
            return _clientSiteActionUow.GetClientSiteActionByOPIID(clientCompanyOPIID).Select(x => new ClientSiteActionModel()
            {
                ID = x.ClientSiteAction.Id,
                ActionType = x.ClientSiteAction.ClientSiteActionType.Name,
                ActionStatus = x.ClientSiteAction.ClientSiteActionStatus.Name,
                Details = x.ClientSiteAction.Details,
                CreatedByUser = x.ClientSiteAction.CreatedByAuthUser.UserName,
                CreatedDateTime = x.ClientSiteAction.CreatedDateTime,
                UpdatedByUser = x.ClientSiteAction.UpdatedByAuthUser.UserName,
                UpdatedDateTime = x.ClientSiteAction.UpdatedDateTime
            }).SingleOrDefault();
        }

        public void LogActionOpiPayment(int authUserId, long fxforwardTrade2OpiId, string accountName, decimal amount)
        {
            var actionType = _clientSiteActionUow.GetClientSiteActionTypeFromName(SystemConstant.ClientSiteAction_Type_OPIPayment);
            var actionStatus = _clientSiteActionUow.GetClientSiteActionStatusFromName(SystemConstant.ClientSiteAction_Status_Pending);

            var details = $"{fxforwardTrade2OpiId}: {accountName}, Amount: {amount}";

            var action = CreateAction(authUserId, details, actionType.Id, actionStatus.Id);

            _clientSiteActionUow.LogAction(action, fxforwardTrade2OpiId.ToString());
        }

        public void LogActionSwapCreation(int authUserId, int FxswapId)
        {
            var actionType = _clientSiteActionUow.GetClientSiteActionTypeFromName(SystemConstant.ClientSiteAction_Type_SwapCreation);
            var actionStatus = _clientSiteActionUow.GetClientSiteActionStatusFromName(SystemConstant.ClientSiteAction_Status_Pending);

            var action = CreateAction(authUserId, FxswapId.ToString(), actionType.Id, actionStatus.Id);

            _clientSiteActionUow.LogAction(action, FxswapId.ToString());
        }

        public void LogActionUnconfirmedTrade(int authUserId, string tradeCode)
        {
            var actionType = _clientSiteActionUow.GetClientSiteActionTypeFromName(SystemConstant.ClientSiteAction_Type_NoFIXConfirmation);
            var actionStatus = _clientSiteActionUow.GetClientSiteActionStatusFromName(SystemConstant.ClientSiteAction_Status_Pending);

            var action = CreateAction(authUserId, tradeCode, actionType.Id, actionStatus.Id);

            _clientSiteActionUow.LogAction(action, tradeCode);
        }

        public void LogActionNewOpi(int authUserId, int newOpiId)
        {
            var actionType = _clientSiteActionUow.GetClientSiteActionTypeFromName(SystemConstant.ClientSiteAction_Type_NewOPI);
            var actionStatus = _clientSiteActionUow.GetClientSiteActionStatusFromName(SystemConstant.ClientSiteAction_Status_Requested);

            var action = CreateAction(authUserId, newOpiId.ToString(), actionType.Id, actionStatus.Id);

            _clientSiteActionUow.LogAction(action, newOpiId.ToString());
        }

        private static DataAccess.Entities.ClientSiteAction CreateAction(int authUserId, string details, int actionTypeId, int actionStatusId)
        {
            var action = new DataAccess.Entities.ClientSiteAction
            {
                ClientSiteActionTypeId = actionTypeId,
                ClientSiteActionStatusId = actionStatusId,
                Details = details,
                CreatedByAuthUserId = authUserId,
                CreatedDateTime = DateTime.UtcNow,
                UpdatedByAuthUserId = authUserId,
                UpdatedDateTime = DateTime.UtcNow
            };

            return action;
        }

        public IEnumerable<CSATradesWithoutFIXConfirmationModel> GetTradesWithoutFIXConfirmation()
        {
            return _clientSiteActionUow.GetTradesWithoutFIXConfirmation()
            .Select(x => new CSATradesWithoutFIXConfirmationModel()
            {
                ActionID = x.ClientSiteAction.Id,
                FXForwardTradeCode = x.FxforwardTradeCodeNavigation.Code,
                ValueDate = x.FxforwardTradeCodeNavigation.ValueDate,
                SellAmount = x.FxforwardTradeCodeNavigation.IsBuy ? 
                            x.FxforwardTradeCodeNavigation.ClientRhsamt : x.FxforwardTradeCodeNavigation.ClientLhsamt,
                BuyAmount = x.FxforwardTradeCodeNavigation.IsBuy ?
                            x.FxforwardTradeCodeNavigation.ClientLhsamt : x.FxforwardTradeCodeNavigation.ClientRhsamt,
                Rate = x.FxforwardTradeCodeNavigation.ClientRate ?? 0.0m,
                CurrencyPair = x.FxforwardTradeCodeNavigation.CurrencyPair,
                ActionCreatedDateTime = x.ClientSiteAction.CreatedDateTime,
                ActionStatus = x.ClientSiteAction.ClientSiteActionStatus.Name,
                ActionStatusID = x.ClientSiteAction.ClientSiteActionStatus.Id
            })
            .OrderByDescending(x => x.ActionCreatedDateTime)
            .ToList();            
        }

        public void UpdateClientSiteAction(ClientSiteActionModel model)
        {
            _clientSiteActionUow.UpdateClientSiteAction(new DataAccess.Entities.ClientSiteAction()
            {
                Id = model.ID,
                UpdatedByAuthUserId = model.UpdatedByUserID,
                UpdatedDateTime = model.UpdatedDateTime,
                ClientSiteActionStatusId = model.ActionStatusID
            });
        }

        public ClientSiteActionStatus GetClientSiteActionStatus(string name)
        {
            return _clientSiteActionUow.GetClientSiteActionStatusFromName(name);
        }

        public IEnumerable<CSAOPIsAssignedToTradesDisplayModel> GetOPIsAssignedToTrades()
        {
            var clientSiteActions =
                _clientSiteActionUow.GetOPIsAssignedToTrades();

            return clientSiteActions
                    .Select(x => new CSAOPIsAssignedToTradesDisplayModel
                    {
                        CompanyName = x.FxforwardTrade2Opi.ClientCompanyOpi.ClientCompany.Name,
                        CompanyID = x.FxforwardTrade2Opi.ClientCompanyOpi.ClientCompany.Id,
                        TradeCode = x.FxforwardTrade2Opi.FxforwardTradeCode,
                        OPIName = x.FxforwardTrade2Opi.ClientCompanyOpi.AccountName,
                        Amount = x.FxforwardTrade2Opi.Amount,
                        CreatedByClientName = GetClientUserName(x.ClientSiteAction.CreatedByAuthUser.ClientCompanyContactAuthUser),
                        CreatedDateTime = x.ClientSiteAction.CreatedDateTime
                    })
                    .OrderByDescending(y => y.CreatedDateTime)
                    .ToList();
        }

        public IEnumerable<CSASwapsModel> GetSwaps()
        {
            IQueryable<CSASwapsModel> parentTradeList = _clientSiteActionUow.GetSwaps()
           .Select(x => new CSASwapsModel()
           {
               ActionID = x.ClientSiteAction.Id,
               ClientCompanyID = x.Fxswap.ParentTradeCodeNavigation.ClientCompanyNavigation.Id,
               ClientCompanyName = x.Fxswap.ParentTradeCodeNavigation.ClientCompanyNavigation.Name,
               FXForwardTradeCode = x.Fxswap.ParentTradeCode,
               CreatedByClientName = GetClientUserName(x.ClientSiteAction.CreatedByAuthUser.ClientCompanyContactAuthUser),
               ValueDate = x.Fxswap.ParentTradeCodeNavigation.ValueDate,
               SellAmount = x.Fxswap.ParentTradeCodeNavigation.IsBuy ?
                           x.Fxswap.ParentTradeCodeNavigation.ClientRhsamt : x.Fxswap.ParentTradeCodeNavigation.ClientLhsamt,
               BuyAmount = x.Fxswap.ParentTradeCodeNavigation.IsBuy ?
                           x.Fxswap.ParentTradeCodeNavigation.ClientLhsamt : x.Fxswap.ParentTradeCodeNavigation.ClientRhsamt,
               Rate = x.Fxswap.ParentTradeCodeNavigation.ClientRate ?? 0.0m,
               CurrencyPair = x.Fxswap.ParentTradeCodeNavigation.CurrencyPair,
               ActionCreatedDateTime = x.ClientSiteAction.CreatedDateTime,
               ActionStatus = x.ClientSiteAction.ClientSiteActionStatus.Name,
               ActionStatusID = x.ClientSiteAction.ClientSiteActionStatus.Id,
               IsParentTrade = true
           });

            IQueryable<CSASwapsModel> deliveryLegList = _clientSiteActionUow.GetSwaps()
           .Select(x => new CSASwapsModel()
           {
               ActionID = x.ClientSiteAction.Id,
               ClientCompanyID = x.Fxswap.DeliveryLegTradeCodeNavigation.ClientCompanyNavigation.Id,
               ClientCompanyName = x.Fxswap.DeliveryLegTradeCodeNavigation.ClientCompanyNavigation.Name,
               FXForwardTradeCode = x.Fxswap.DeliveryLegTradeCode,
               CreatedByClientName = GetClientUserName(x.ClientSiteAction.CreatedByAuthUser.ClientCompanyContactAuthUser),
               ValueDate = x.Fxswap.DeliveryLegTradeCodeNavigation.ValueDate,
               SellAmount = x.Fxswap.DeliveryLegTradeCodeNavigation.IsBuy ?
                           x.Fxswap.DeliveryLegTradeCodeNavigation.ClientRhsamt : x.Fxswap.DeliveryLegTradeCodeNavigation.ClientLhsamt,
               BuyAmount = x.Fxswap.DeliveryLegTradeCodeNavigation.IsBuy ?
                           x.Fxswap.DeliveryLegTradeCodeNavigation.ClientLhsamt : x.Fxswap.DeliveryLegTradeCodeNavigation.ClientRhsamt,
               Rate = x.Fxswap.DeliveryLegTradeCodeNavigation.ClientRate ?? 0.0m,
               CurrencyPair = x.Fxswap.DeliveryLegTradeCodeNavigation.CurrencyPair,
               ActionCreatedDateTime = x.ClientSiteAction.CreatedDateTime,
               ActionStatus = x.ClientSiteAction.ClientSiteActionStatus.Name,
               ActionStatusID = x.ClientSiteAction.ClientSiteActionStatus.Id,
               IsParentTrade = false
           });

            IQueryable<CSASwapsModel> reversalLegList = _clientSiteActionUow.GetSwaps()
           .Select(x => new CSASwapsModel()
           {
               ActionID = x.ClientSiteAction.Id,
               ClientCompanyID = x.Fxswap.ReversalLegTradeCodeNavigation.ClientCompanyNavigation.Id,
               ClientCompanyName = x.Fxswap.ReversalLegTradeCodeNavigation.ClientCompanyNavigation.Name,
               FXForwardTradeCode = x.Fxswap.ReversalLegTradeCode,
               CreatedByClientName = GetClientUserName(x.ClientSiteAction.CreatedByAuthUser.ClientCompanyContactAuthUser),
               ValueDate = x.Fxswap.ReversalLegTradeCodeNavigation.ValueDate,
               SellAmount = x.Fxswap.ReversalLegTradeCodeNavigation.IsBuy ?
                           x.Fxswap.ReversalLegTradeCodeNavigation.ClientRhsamt : x.Fxswap.ReversalLegTradeCodeNavigation.ClientLhsamt,
               BuyAmount = x.Fxswap.ReversalLegTradeCodeNavigation.IsBuy ?
                           x.Fxswap.ReversalLegTradeCodeNavigation.ClientLhsamt : x.Fxswap.ReversalLegTradeCodeNavigation.ClientRhsamt,
               Rate = x.Fxswap.ReversalLegTradeCodeNavigation.ClientRate ?? 0.0m,
               CurrencyPair = x.Fxswap.ReversalLegTradeCodeNavigation.CurrencyPair,
               ActionCreatedDateTime = x.ClientSiteAction.CreatedDateTime,
               ActionStatus = x.ClientSiteAction.ClientSiteActionStatus.Name,
               ActionStatusID = x.ClientSiteAction.ClientSiteActionStatus.Id,
               IsParentTrade = false
           });

            return parentTradeList.Concat(deliveryLegList).Concat(reversalLegList)            
            .OrderByDescending(x => x.ActionCreatedDateTime)
            .ToList();
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

        public IEnumerable<CSANewOPIRequestDisplayModel> GetNewOPIRequested()
        {
            var clientSiteActions =
                _clientSiteActionUow.GetNewOPIRequested();

            return clientSiteActions
                    .Select(x => new CSANewOPIRequestDisplayModel
                    {
                        CompanyName = x.ClientCompanyOpi.ClientCompany.Name,
                        CompanyID = x.ClientCompanyOpi.ClientCompany.Id,
                        CurrencyCode = x.ClientCompanyOpi.Currency.Code,
                        OPIName = x.ClientCompanyOpi.AccountName,
                        Status= x.ClientSiteAction.ClientSiteActionStatus.Name,
                        CreatedByClientName = GetClientUserName(x.ClientSiteAction.CreatedByAuthUser.ClientCompanyContactAuthUser),
                        CreatedDateTime = x.ClientSiteAction.CreatedDateTime
                    })
                    .OrderByDescending(y => y.CreatedDateTime)
                    .ToList();
        }

        public void DeleteAction2AssignedSettlementLink(long settlementId)
        {
            _clientSiteActionUow.DeleteAction2AssignedSettlementLink(settlementId);
        }

        #region disposing

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _clientSiteActionUow?.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
