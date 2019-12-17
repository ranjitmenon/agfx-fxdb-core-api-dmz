using System;

namespace Argentex.Core.Service.AppSettings
{
    public interface IAppSettingService : IDisposable
    {
        int GetStreamingQuoteDuration();
        int GetTimeOut();
        string GetBarxFXFixQuoteUrl();
        string GetBarxFXFixNewOrderUrl();
        string GetEmirUtiCode();
        int GetFixTimeout();
        int GetStreamingDuration();
        int GetTradeNotificationCounter();
        int GetSpreadAdjustmentValidity();
        int GetUserChangeDaysRequiredForApproval();
    }
}
