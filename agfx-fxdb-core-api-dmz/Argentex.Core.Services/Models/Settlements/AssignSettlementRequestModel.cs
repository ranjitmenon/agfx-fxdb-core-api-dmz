using Argentex.Core.Service.Models.Trade;
using System.Collections.Generic;

namespace Argentex.Core.Service.Models.Settlements
{
    public class AssignSettlementRequestModel
    {
        public int AuthUserId { get; set; }
        public int ClientCompanyId { get; set; }
        public TradeModel Trade { get; set; }
        public IList<AssignSettlementModel> SettlementModels { get; set; }
    }
}
