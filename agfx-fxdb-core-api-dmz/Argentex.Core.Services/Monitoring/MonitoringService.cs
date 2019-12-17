using Argentex.ClientSite.Service.Http;
using Argentex.Core.Service.AppSettings;
using Argentex.Core.Service.Models.Trades;
using Argentex.Core.Service.Trade;
using Argentex.Core.Service.User;
using Argentex.Core.SignalRService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Argentex.Core.Service.Monitoring
{
    public class MonitoringService : IMonitoringService
    {
        private readonly IHttpService _httpService;
        private readonly IAppSettingService _appSettingService;
        private readonly IMonitoringHub _monitoring;
        private readonly IClientCompanyService _clientCompanyService;
        private readonly IUserService _userService;

        private bool _disposed;

        public MonitoringService(IHttpService httpService, IAppSettingService appSettingService, IMonitoringHub monitoring,
            IClientCompanyService clientCompanyService, IUserService userService)
        {
            _httpService = httpService;
            _appSettingService = appSettingService;
            _monitoring = monitoring;
            _clientCompanyService = clientCompanyService;
            _userService = userService;
        }

        public async Task<bool> NotifyTradeStarted(int authUserId)
        {
            try
            {
                await _monitoring.TradingStarted(authUserId.ToString());
            }
            catch (System.Exception ex)
            {
                var error = ex.Message;
                return false;
            }

            return true;
        }

        public async Task<bool> CheckExecuteTrade(TradeNotificationModel model)
        {
            try
            {                
                //determine if the trade should be executed 
                //in case there is an active temporary spread the trade can be executed
                //otherwise the CSR user must wait for the Trader site user to adjust the spread, 
                //cancel the spread adjusting or the adjust spread counter to time out

                bool executeTrade = _clientCompanyService.GetTradeExecutionStatusBySpread(model.ClientCompanyID, model.BuyCcy, model.SellCcy, model.IsBuy);

                if (executeTrade)
                {
                    return true;
                }
                else
                {
                    //the trade should not be executed at the moment

                    FillInTradeNotificationModel(model);

                    //in case the Dealer for the company has the OnlineTradingNotifications checked we send the notification to the Trader
                    //otherwise the trade can be executed (as there is no trader that can do the spread adjustment)
                    if (model.SendNotification)
                    {
                        //send trade notification to the Trader site
                        //do not continue the trade execution
                        await _monitoring.CheckExecuteTrade(JsonConvert.SerializeObject(model));
                        return false;
                    }
                    else
                    {
                        //the trade should be executed because either a dealer is not associated with the company
                        //or it does not have the OnlineTradingNotifications checked
                        //or the Company's AllowOnlineTrading is set to false
                        return true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public async Task RefreshClientDetails()
        {
            await _monitoring.RefreshClientDetails();
        }

        private void FillInTradeNotificationModel(TradeNotificationModel model)
        {
            model.BuyCcy = model.BuyCcy.ToUpperInvariant();
            model.SellCcy = model.SellCcy.ToUpperInvariant();

            ClientCompanyModel clientCompanyModel = _clientCompanyService.GetClientCompany(model.ClientCompanyID);
            if(clientCompanyModel != null)
            {
                model.ClientCompanyName = clientCompanyModel.Name;
                model.DealerAppUserID = clientCompanyModel.DealerAppUserID;
            }

            IList<Models.Identity.UserModel> list = _userService.GetUserLoginDetails(model.AuthUserID);
            if(list != null && list.Count > 0)
            {                    
                model.LastLoginDate = list[0].LastLoginDate.Value;
            }

            Models.Identity.ApplicationServiceUser user = _userService.GetApplicationUserByAuthUserId(model.AuthUserID);
            if (user != null)
            {
                model.AuthUserName = user.Forename + " " + user.Surname;
            }

            model.TraderNotificationCounter = _appSettingService.GetTradeNotificationCounter();

            model.SendNotification = false;
            if (model.DealerAppUserID.HasValue)
            {
                DataAccess.Entities.AppUser appUser = _userService.GetAppUserById(model.DealerAppUserID.Value);
                if (appUser != null)
                {
                    model.SendNotification = appUser.OnlineTradingNotifications;
                }

                if (model.SendNotification)
                {
                    DataAccess.Entities.ClientCompanyOnlineDetails clientCompanyOnlineDetails = _clientCompanyService.GetClientCompanyOnlineDetails(model.ClientCompanyID);
                    if (clientCompanyOnlineDetails == null)
                    {
                        model.SendNotification = false;
                    }
                    else
                    {
                        model.SendNotification = clientCompanyOnlineDetails.AllowOnlineTrading;
                    }
                }
            }                       

            model.Spread = _clientCompanyService.GetClientCompanySpread(model.ClientCompanyID, model.BuyCcy, model.SellCcy, model.IsBuy, model.ValueDate, model.ContractDate);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _appSettingService?.Dispose();
                    _httpService?.Dispose();
                    _clientCompanyService?.Dispose();
                    _userService?.Dispose();
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
