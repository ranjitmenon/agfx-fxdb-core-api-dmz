using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Service.Models.Identity;
using Argentex.Core.Service.Models.Order;
using Argentex.Core.UnitsOfWork.Notifications;
using Argentex.Core.UnitsOfWork.Users;
using System.Collections.Generic;
using System.Linq;

namespace Argentex.Core.Service.Order
{
    public class NotificationService : INotificationService
    {
        #region Constructor and Properties

        private readonly INotificationUow _notificationUow;
        private readonly IUserUow _userUow;

        public NotificationService(INotificationUow notificationUow, IUserUow userUow)
        {
            _notificationUow = notificationUow;
            _userUow = userUow;
        }

        #endregion

        public bool SaveAppUserNotification(AppUserNotificationModel model)
        {
            var saveItem = new AppUserNotification()
            {
                Id = model.ID,
                AppUserId = (int)model.AppUserID,
                ClientCompanyId = model.ClientCompanyID,
                InwardPaymentNotifications = model.InwardPaymentNotifications,
                OutwardPaymentNotifications = model.OutwardPaymentNotifications,
                SettlementRequests = model.SettlementRequests,
                TradeNotifications = model.TradeNotifications,
            };
            return _notificationUow.SaveAppUserNotification(saveItem);
        }

        public IList<AppUserNotificationModel> GetCompanyUsers(int clientCompanyID)
        {
            var applicationServiceUserList = _userUow.GetUsersByCompanyId(clientCompanyID)
                 .Select(x => new ApplicationServiceUser
                 {
                     Id = x.AuthUserId,
                     Title = x.Title,
                     Forename = x.Forename,
                     Surname = x.Surname,
                     Username = x.UserName,
                     Email = x.Email,
                     ClientCompanyId = x.ClientCompanyId,
                     UpdatedByAuthUserId = x.UpdatedByAuthUserId,
                     Position = x.Position,
                     PhoneNumberDirect = x.PhoneNumber,
                     PhoneNumberMobile = x.PhoneNumberMobile,
                     Birthday = x.Birthday.ToString(),
                     IsApproved = x.IsApproved,
                     IsAdmin = x.IsAdmin,
                     IsSignatory = x.IsSignatory,
                     IsAuthorisedSignatory = x.IsAuthorisedSignatory
                 });

            var appUserNotification = _notificationUow.GetCompanyAppUserNotification(clientCompanyID)
                .Select(elem => new AppUserNotificationModel
                {
                    ID = elem.Id,
                    ClientCompanyID = elem.ClientCompanyId,
                    AppUserID = elem.AppUserId,
                    TradeNotifications = elem.TradeNotifications,
                    InwardPaymentNotifications = elem.InwardPaymentNotifications,
                    OutwardPaymentNotifications = elem.OutwardPaymentNotifications,
                    SettlementRequests = elem.SettlementRequests
                }).ToList<AppUserNotificationModel>();

            var result = applicationServiceUserList.GroupJoin(appUserNotification,
                user => user.Id, notification => notification.AppUserID, (user, notification) => new AppUserNotificationModel
                {
                    ID = notification.SingleOrDefault() == null ? 0 : notification.SingleOrDefault().ID,
                    ClientCompanyID = user.ClientCompanyId,
                    AppUserID = user.Id,
                    TradeNotifications = notification.SingleOrDefault() == null ? false : notification.SingleOrDefault().TradeNotifications,
                    InwardPaymentNotifications = notification.SingleOrDefault() == null ? false : notification.SingleOrDefault().InwardPaymentNotifications,
                    OutwardPaymentNotifications = notification.SingleOrDefault() == null ? false : notification.SingleOrDefault().OutwardPaymentNotifications,
                    SettlementRequests = notification.SingleOrDefault() == null ? false : notification.SingleOrDefault().SettlementRequests,
                    Forename = user.Forename,
                    Surname = user.Surname,
                    Username = user.Username,
                    Email = user.Email,
                    Position = user.Position,
                    PhoneNumberDirect = user.PhoneNumberDirect,
                    PhoneNumberMobile = user.PhoneNumberMobile,
                    IsApproved = user.IsApproved,
                    IsAdmin = user.IsAdmin
                }).ToList();

            return result;
        }

        #region V2

        //public IList<NotificationTypeModel> GetNotificationTypes()
        //{
        //    var result = new List<NotificationTypeModel>();

        //    result = _notificationUow.GetNotificationTypes()
        //        .Select(elem => new NotificationTypeModel
        //        {
        //            ID = elem.ID,
        //            Name = elem.Name
        //        }).ToList<NotificationTypeModel>();

        //    return result;
        //}

        //public IList<AppUserNotificationTypeModel> GetCompanyNotifications(int clientCompanyID)
        //{
        //    var result = new List<AppUserNotificationTypeModel>();

        //    result = _notificationUow.GetCompanyNotifications(clientCompanyID)
        //        .Select(elem => new AppUserNotificationTypeModel
        //        {
        //            ID = elem.ID,
        //            ClientCompanyID = elem.ClientCompanyID,
        //            AppUserID = elem.AppUserID,
        //            NotificationTypeID = elem.NotificationTypeID
        //        }).ToList<AppUserNotificationTypeModel>();

        //    return result;
        //}

        //public bool SaveUserNotification(AppUserNotificationTypeModel model)
        //{
        //    var saveItem = new AppUserNotificationType()
        //    {
        //        ID = model.ID,
        //        AppUserID = model.AppUserID,
        //        ClientCompanyID = model.ClientCompanyID,
        //        NotificationTypeID = model.NotificationTypeID
        //    };
        //    return _notificationUow.SaveUserNotification(saveItem);
        //}

        #endregion
    }
}
