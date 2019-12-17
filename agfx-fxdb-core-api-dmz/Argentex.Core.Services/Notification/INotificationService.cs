using Argentex.Core.Service.Models.Email;
using Argentex.Core.Service.Models.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Argentex.Core.Service.Order
{
    public interface INotificationService
    {
        // v1
        IList<AppUserNotificationModel> GetCompanyUsers(int clientCompanyID);
        bool SaveAppUserNotification(AppUserNotificationModel model);

        //// v2
        //IList<NotificationTypeModel> GetNotificationTypes();
        //IList<AppUserNotificationTypeModel> GetCompanyNotifications(int clientCompanyID);
        //bool SaveUserNotification(AppUserNotificationTypeModel model);
    }
}
