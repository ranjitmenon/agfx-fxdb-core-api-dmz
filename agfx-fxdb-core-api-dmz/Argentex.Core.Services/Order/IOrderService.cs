using Argentex.Core.Service.Models.Email;
using Argentex.Core.Service.Models.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Argentex.Core.Service.Order
{
    public interface IOrderService
    {
        Task<IList<OrderResponseModel>> ExecuteOrdersAsync(OrderRequestModel ordersToExecute);
        IList<Models.Trade.TradeModel> GetOpenOrders(int clientCompanyId);

        IList<CancelOrderModel> GetExpiredValidityOrders();
        Task<bool> CancelOrderAsync(string tradeCode);
        Task<bool> CancelOrderAsync(CancelOrderModel model);
    }
}
