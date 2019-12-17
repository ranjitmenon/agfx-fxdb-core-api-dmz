using Argentex.Core.Service.Helpers;
using Argentex.Core.UnitsOfWork.AppSettings;
using System;
using System.Collections.Generic;

namespace Argentex.Core.Service.AppSettings
{
    public class AppSettingService : IAppSettingService
    {
        private readonly IAppSettingUow _appSettingUow;
        private bool _disposed;

        public AppSettingService(IAppSettingUow uow)
        {
            _appSettingUow = uow;
        }
        public int GetStreamingQuoteDuration()
        {
            var entity = _appSettingUow.GetAppSetting("StreamingQuoteDuration");

            if (entity == null)
            {
                throw new KeyNotFoundException("FixQuoteRequestTimeOut is not set in AppSetting");
            }

            int duration;
            if (!int.TryParse(entity.SettingValue, out duration))
            {
                throw new FormatException("StreamingQuoteDuration string cannot be converted to int");
            }
            return duration;
        }

        public int GetTimeOut()
        {
            var entity = _appSettingUow.GetAppSetting("FixFXTimeOut");

            if (entity == null)
            {
                throw new KeyNotFoundException("FixFXTimeOut is not set in AppSetting");
            }

            if (!int.TryParse(entity.SettingValue, out int timeOut))
            {
                throw new FormatException("TimeOut string cannot be converted to int");
            }
            return timeOut;
        }

        public string GetBarxFXFixQuoteUrl()
        {
            var entity = _appSettingUow.GetAppSetting("SynetecFixGetQuoteUrl");

            if (entity == null)
            {
                throw new KeyNotFoundException("SynetecFixGetQuoteUrl is not set in AppSetting");
            }

            if (string.IsNullOrWhiteSpace(entity.SettingValue))
            {
                throw new FormatException("SynetecFixGetQuoteUrl string is not valid URL");
            }
            return entity.SettingValue;
        }

        public string GetBarxFXFixNewOrderUrl()
        {
            var entity = _appSettingUow.GetAppSetting("SynetecFixNewOrderUrl");

            if (entity == null)
            {
                throw new KeyNotFoundException("SynetecFixNewOrderUrl is not set in AppSetting");
            }

            if (string.IsNullOrWhiteSpace(entity.SettingValue))
            {
                throw new FormatException("SynetecFixNewOrderUrl string is not valid URL");
            }
            return entity.SettingValue;
        }

        public string GetEmirUtiCode()
        {
            var entity = _appSettingUow.GetAppSetting("EMIR_FXForwardTrade_UTI_Prefix");

            if (entity == null)
            {
                throw new KeyNotFoundException("GetEmirUtiCode is not set in AppSetting");
            }

            if (string.IsNullOrWhiteSpace(entity.SettingValue))
            {
                throw new FormatException("GetEmirUtiCode string is not valid URL");
            }
            return entity.SettingValue;
        }

        public int GetFixTimeout()
        {
            var entity = _appSettingUow.GetAppSetting("FixFXTimeOut");

            if (entity == null)
            {
                throw new KeyNotFoundException("FixFXTimeOut is not set in AppSetting");
            }

            if (string.IsNullOrWhiteSpace(entity.SettingValue))
            {
                throw new FormatException("FixFXTimeOut string is not valid URL");
            }

            int timeout = int.Parse(entity.SettingValue);

            return timeout;
        }

        public int GetStreamingDuration()
        {
            var entity = _appSettingUow.GetAppSetting("StreamingQuoteDuration");

            if (entity == null)
            {
                throw new KeyNotFoundException("StreamingQuoteDuration is not set in AppSetting");
            }

            if (string.IsNullOrWhiteSpace(entity.SettingValue))
            {
                throw new FormatException("StreamingQuoteDuration string is not valid URL");
            }

            int streamingDuration = int.Parse(entity.SettingValue);

            return streamingDuration;
        }

        public int GetTradeNotificationCounter()
        {
            var entity = _appSettingUow.GetAppSetting("TradeNotificationCounter");

            if (entity == null)
            {
                throw new KeyNotFoundException("TradeNotificationCounter is not set in AppSetting");
            }

            if (string.IsNullOrWhiteSpace(entity.SettingValue))
            {
                throw new FormatException("TradeNotificationCounter value is null");
            }

            return int.Parse(entity.SettingValue);
        }

        public int GetSpreadAdjustmentValidity()
        {
            var entity = _appSettingUow.GetAppSetting("SpreadAdjustmentValidityMinutes");

            if (entity == null)
            {
                throw new KeyNotFoundException("SpreadAdjustmentValidityMinutes is not set in AppSetting");
            }

            if (string.IsNullOrWhiteSpace(entity.SettingValue))
            {
                throw new FormatException("SpreadAdjustmentValidityMinutes value is null");
            }

            return int.Parse(entity.SettingValue);
        }

        public int GetUserChangeDaysRequiredForApproval()
        {
            var entity = _appSettingUow.GetAppSetting(SystemConstant.Setting_UserChangeDaysRequiresForApproval);

            if (entity == null || string.IsNullOrWhiteSpace(entity.SettingValue))
                return int.Parse(entity.SettingValue);
            else
                return SystemConstant.Setting_UserChangeDaysRequiresForApproval_Default;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _appSettingUow?.Dispose();
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
