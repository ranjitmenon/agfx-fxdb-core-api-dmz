using Argentex.Core.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Synetec.Data.UnitOfWork.BaseUnitOfWork;
using Synetec.Data.UnitOfWork.GenericRepo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Argentex.Core.UnitsOfWork.Notifications
{
    public class NotificationUow : BaseUow, INotificationUow
    {
        #region Properties V1

        private IGenericRepo<AppUserNotification> _appUserNotificationRepo;

        private IGenericRepo<AppUserNotification> AppUserNotificationRepo =>
           _appUserNotificationRepo = _appUserNotificationRepo ?? new GenericRepo<AppUserNotification>(Context);

        #endregion

        #region Properties V2

        //private IGenericRepo<NotificationType> _notificationTypeRepo;
        //private IGenericRepo<AppUserNotificationType> _appUserNotificationType;

        //private IGenericRepo<NotificationType> NotificationTypeRepo =>
        //    _notificationTypeRepo = _notificationTypeRepo ?? new GenericRepo<NotificationType>(Context);

        //private IGenericRepo<AppUserNotificationType> AppUserNotificationTypeRepo =>
        //   _appUserNotificationType = _appUserNotificationType ?? new GenericRepo<AppUserNotificationType>(Context);

        #endregion

        public NotificationUow(FXDB1Context context) : base(context)
        {
        }

        public IQueryable<AppUserNotification> GetCompanyAppUserNotification(int clientCompanyID)
        {
            return this.AppUserNotificationRepo
                .GetQueryable(e => e.ClientCompanyId == clientCompanyID);
        }

        public bool SaveAppUserNotification(AppUserNotification model)
        {
            var notificationItem = AppUserNotificationRepo.GetQueryable(e => e.AppUserId == model.AppUserId && e.ClientCompanyId == model.ClientCompanyId).SingleOrDefault();

            if (notificationItem != null && notificationItem.Id > 0)
            {
                notificationItem.InwardPaymentNotifications = model.InwardPaymentNotifications;
                notificationItem.OutwardPaymentNotifications = model.OutwardPaymentNotifications;
                notificationItem.TradeNotifications = model.TradeNotifications;
                notificationItem.SettlementRequests = model.SettlementRequests;

                AppUserNotificationRepo.Update(notificationItem);
            }
            else
            {
                model.Id = 0;
                AppUserNotificationRepo.Insert(model);
            }
            SaveContext();
            return true;
        }

        #region V2

        //public IQueryable<NotificationType> GetNotificationTypes()
        //{
        //    return NotificationTypeRepo.GetQueryable();
        //}

        //public IQueryable<AppUserNotificationType> GetCompanyNotifications(int clientCompanyID)
        //{
        //    return AppUserNotificationTypeRepo.GetQueryable(x => x.ClientCompanyID == clientCompanyID);
        //}

        //public bool SaveUserNotification(AppUserNotificationType model)
        //{
        //    AppUserNotificationTypeRepo.Insert(model);
        //    SaveContext();
        //    return true;
        //}

        #endregion
    }
}
