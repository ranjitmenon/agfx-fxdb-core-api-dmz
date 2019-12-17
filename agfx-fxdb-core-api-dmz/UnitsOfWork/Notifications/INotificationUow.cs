using Argentex.Core.DataAccess.Entities;
using Synetec.Data.UnitOfWork.BaseUnitOfWork;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Argentex.Core.UnitsOfWork.Notifications
{
    public interface INotificationUow : IBaseUow
    {
        // v1
        IQueryable<AppUserNotification> GetCompanyAppUserNotification(int clientCompanyID);
        bool SaveAppUserNotification(AppUserNotification model);

        // v2
        //IQueryable<NotificationType> GetNotificationTypes();
        //IQueryable<AppUserNotificationType> GetCompanyNotifications(int clientCompanyID);
        //bool SaveUserNotification(AppUserNotificationType model);
    }
}
