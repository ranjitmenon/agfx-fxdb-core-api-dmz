using Argentex.Core.DataAccess.Entities;
using Synetec.Data.UnitOfWork.BaseUnitOfWork;
using System;
using System.Collections.Generic;

namespace Argentex.Core.UnitsOfWork.Settlements
{
    public interface ISettlementUow : IBaseUow
    {
        IDictionary<FxforwardTrade, DataAccess.Entities.ClientSiteAction> GetTradeSwaps(string parentTradeCode);
        IList<FxforwardTrade2Opi> GetTradeOpis(string parentTradeCode);
        FxforwardTradeSwapCount GetTradeSwapCount(string parentTradeCode);
        int Assign(FxforwardTrade deliveryLegTrade, FxforwardTrade reversalLegTrade, string parentTradeCode, int authUserID);
        void AddTrade2Opi(FxforwardTrade2Opi trade2opi);
        void DeleteAssignedSettlement(long settlementId);
        decimal GetSettlementAmountForTrade(string tradeCode);
        DateTime GetMaxCreateDateForTrade(string tradeCode);
    }
}
