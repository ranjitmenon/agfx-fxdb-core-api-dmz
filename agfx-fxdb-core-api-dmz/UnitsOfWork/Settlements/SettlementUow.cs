using Argentex.Core.DataAccess.Entities;
using Synetec.Data.UnitOfWork.BaseUnitOfWork;
using Synetec.Data.UnitOfWork.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Argentex.Core.UnitsOfWork.Settlements
{
    public class SettlementUow : BaseUow, ISettlementUow
    {
        #region Repos

        private IGenericRepo<FxforwardTrade> _tradeRepository;
        private IGenericRepo<FxforwardTradeSwapCount> _tradeSwapCountRepository;
        private IGenericRepo<Fxswap> _fxswapRepository;
        private IGenericRepo<FxforwardTrade2Opi> _fxforwardTrade2OpiRepo;
        private IGenericRepo<ClientSiteAction2Fxswap> _clientSiteAction2FxswapRepository;
        private IGenericRepo<DataAccess.Entities.ClientSiteAction> _clientSiteActionRepository;
        
        private IGenericRepo<FxforwardTrade> TradeRepository =>
            _tradeRepository = _tradeRepository ?? new GenericRepo<FxforwardTrade>(Context);
        private IGenericRepo<FxforwardTradeSwapCount> TradeSwapCountRepository =>
            _tradeSwapCountRepository = _tradeSwapCountRepository ?? new GenericRepo<FxforwardTradeSwapCount>(Context);
        private IGenericRepo<Fxswap> FxswapRepository =>
            _fxswapRepository = _fxswapRepository ?? new GenericRepo<Fxswap>(Context);
        private IGenericRepo<FxforwardTrade2Opi> FxforwardTrade2OpiRepo =>
            _fxforwardTrade2OpiRepo = _fxforwardTrade2OpiRepo ?? new GenericRepo<FxforwardTrade2Opi>(Context);
        private IGenericRepo<ClientSiteAction2Fxswap> ClientSiteAction2FxswapRepository =>
            _clientSiteAction2FxswapRepository = _clientSiteAction2FxswapRepository ?? new GenericRepo<ClientSiteAction2Fxswap>(Context);
        private IGenericRepo<DataAccess.Entities.ClientSiteAction> ClientSiteActionRepository =>
            _clientSiteActionRepository = _clientSiteActionRepository ?? new GenericRepo<DataAccess.Entities.ClientSiteAction>(Context);

        #endregion

        public SettlementUow(FXDB1Context context) : base(context) { }

        public IList<FxforwardTrade2Opi> GetTradeOpis(string parentTradeCode)
        {
            var result = new List<FxforwardTrade2Opi>();

            result = FxforwardTrade2OpiRepo.GetQueryable(
                e => e.FxforwardTradeCode == parentTradeCode,
                includeProperties: "ClientCompanyOpi,ClientCompanyOpi.Currency,FxforwardTradeCodeNavigation,FxforwardTradeCodeNavigation.Lhsccy,FxforwardTradeCodeNavigation.Rhsccy").ToList();

            return result;
        }

        public IDictionary<FxforwardTrade, DataAccess.Entities.ClientSiteAction> GetTradeSwaps(string parentTradeCode)
        {
            var result = new Dictionary<FxforwardTrade, DataAccess.Entities.ClientSiteAction>();
            var swaps = FxswapRepository.GetQueryable(e => e.ParentTradeCode == parentTradeCode);
            foreach(var swap in swaps)
            {
                var csa2Swap = ClientSiteAction2FxswapRepository.GetQueryable(e => e.FxswapId == swap.Id).SingleOrDefault();
                if(csa2Swap != null)
                {
                    var csa = ClientSiteActionRepository.GetQueryable(e => e.Id == csa2Swap.ClientSiteActionId).SingleOrDefault();
                    if(csa != null)
                    {
                        var trade = TradeRepository.GetQueryable(e => e.Code == swap.DeliveryLegTradeCode,
                        includeProperties: "Rhsccy,Lhsccy,ClientCompanyOpi,ClientCompanyOpi.Currency").SingleOrDefault();
                        result.Add(trade, csa);
                    }
                }
            }
            return result;
        }

        public FxforwardTradeSwapCount GetTradeSwapCount(string parentTradeCode)
        {
            return TradeSwapCountRepository.GetByPrimaryKey(parentTradeCode);
        }

        public int Assign(FxforwardTrade deliveryLegTrade, FxforwardTrade reversalLegTrade, string parentTradeCode, int authUserID)
        {
            // updating tradeswapcount +1
            var tradeSwapCount = GetTradeSwapCount(parentTradeCode);
            tradeSwapCount.SwapCount++;
            TradeSwapCountRepository.Update(tradeSwapCount);
            // inserting the counts
            TradeSwapCountRepository.Insert(
                new FxforwardTradeSwapCount {
                    FxforwardTradeCode = deliveryLegTrade.Code,
                    SwapCount = 0 });
            TradeSwapCountRepository.Insert(
                new FxforwardTradeSwapCount {
                    FxforwardTradeCode = reversalLegTrade.Code,
                    SwapCount = 0 });
            // inserting trades
            TradeRepository.Insert(deliveryLegTrade);
            TradeRepository.Insert(reversalLegTrade);
            // inserting the swap
            var swap = new Fxswap() {
                ParentTradeCode = parentTradeCode,
                DeliveryLegTradeCode = deliveryLegTrade.Code,
                ReversalLegTradeCode = reversalLegTrade.Code,
                CreatedAuthUserId = authUserID
            };
            FxswapRepository.Insert(swap);
            // ??? InsertTradeLog(trade, "INSERT");
            SaveContext();

            return swap.Id;
        }

        public void AddTrade2Opi(FxforwardTrade2Opi trade2opi)
        {
            FxforwardTrade2OpiRepo.Insert(trade2opi);
            SaveContext();
        }

        public void DeleteAssignedSettlement(long settlementId)
        {
            // TODO fix long-int
            var fxforwardTrade2Opi = FxforwardTrade2OpiRepo.GetByPrimaryKey(settlementId);

            FxforwardTrade2OpiRepo.Delete(fxforwardTrade2Opi);

            SaveContext();
        }

        public decimal GetSettlementAmountForTrade(string tradeCode) =>
            FxforwardTrade2OpiRepo.GetQueryable(x => x.FxforwardTradeCode == tradeCode).Sum(x => x.Amount);

        public DateTime GetMaxCreateDateForTrade(string tradeCode) =>
            FxforwardTrade2OpiRepo.GetQueryable(x => x.FxforwardTradeCode == tradeCode)
            .OrderByDescending(x => x.CreatedDateTime).Select(x => x.CreatedDateTime).FirstOrDefault();
    }
}
