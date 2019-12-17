using Argentex.Core.Service.Models.Order;
using Argentex.Core.Service.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Argentex.Core.Api.Controllers.Order
{
    [Produces("application/json")]
    [Route("api/notification")]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        [Route("get-company-users/{clientCompanyId:int}")]
        public IActionResult GetCompanyUsers(int clientCompanyId)
        {
            return Ok(_notificationService.GetCompanyUsers(clientCompanyId));
        }

        [HttpPost]
        [Route("save-app-user-notification")]
        public IActionResult SaveUserNotification([FromBody] AppUserNotificationModel model)
        {
            return Ok(_notificationService.SaveAppUserNotification(model));
        }

        #region V2

        //[HttpGet]
        //[AllowAnonymous] //Remove after Trader Token implementation
        //[Route("get-types-v2")]
        //public IActionResult GetNotificationTypesV2()
        //{
        //    return Ok(_notificationService.GetNotificationTypes());
        //}

        //[HttpGet]
        //[AllowAnonymous] //Remove after Trader Token implementation
        //[Route("get-company-notifications-v2/{clientCompanyId:int}")]
        //public IActionResult GetCompanyNotificationsV2(int clientCompanyId)
        //{
        //    return Ok(_notificationService.GetCompanyNotifications(clientCompanyId));
        //}

        //[HttpPost]
        //[AllowAnonymous] //Remove after Trader Token implementation
        //[Route("save-user-notification-v2")]
        //public IActionResult SaveUserNotificationV2([FromBody] AppUserNotificationModel model)
        //{
        //    return Ok(_notificationService.SaveAppUserNotification(model));
        //}

        #endregion

    }
}