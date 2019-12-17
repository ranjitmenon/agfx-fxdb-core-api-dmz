using Argentex.Core.Service.Models.Order;
using Argentex.Core.Service.Order;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Argentex.Core.Api.Controllers.Order
{
    [Produces("application/json")]
    [Route("api/order")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Route("execute")]
        public async Task<IActionResult> ExecuteOrdersAsync([FromBody] OrderRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderResults = await _orderService.ExecuteOrdersAsync(model);

            return Ok(orderResults);
        }

        [HttpGet]
        [Route("open-orders/{clientCompanyId:int}")]
        public IActionResult GetOpenOrders(int clientCompanyId)
        {
            return Ok(_orderService.GetOpenOrders(clientCompanyId));
        }

        [HttpPost]
        [Route("cancel")]
        public async Task<IActionResult> CancelOrderAsync([FromBody] string trackCode)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(trackCode))
            {
                return BadRequest(ModelState);
            }

            return Ok(await _orderService.CancelOrderAsync(trackCode));
        }
    }
}