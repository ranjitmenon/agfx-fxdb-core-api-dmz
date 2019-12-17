using Argentex.Core.DataAccess.Entities;
using Synetec.Data.UnitOfWork.BaseUnitOfWork;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Argentex.Core.UnitsOfWork.Trades
{
    public interface ITradeUow : IBaseUow
    {        
        DataTable GetUnsettledTrades(int clientCompanyId);
        IQueryable<Currency> GetCurrencies();
        IQueryable<CurrencyPairValidation> GetCurrencyPairValidation();
        bool ExecuteOrder(FxforwardTrade trade, ClientCompanyTradeCount tradeCountObject);
        bool CreateDeal(FxforwardTrade trade, ClientCompanyTradeCount tradeCountObject);
        bool BrokerDeal(FxforwardTrade trade, ClientCompanyTradeCount tradeCountObject);
        void RejectOrder(FxforwardTrade trade);
        ClientCompanyTradeCount GetTradeCountByPrimaryKey(int clientCompanyId);
        IQueryable<FxforwardTrade> GetTrade(string tradeCode);
        IQueryable<ClientCompanyTradeCount> GetClientCompanyTradeCount(int clientCompanyId);
        DataTable GetClosedTrades(int clientCompanyId);
        IQueryable<FxforwardTrade> GetOpenOrders(int clientCompanyId);
        IQueryable<FxforwardTrade> GetExpiredValidityOrders();
        void UpdateTrade(FxforwardTrade trade);
        FxforwardTrade GetTrade(string tradeCode, bool getAdditionalProperties);
        FxforwardTradeStatus GetFxForwardStatus(string statusDescription);        
        Emirstatus GetEmirStatus (string emirStatusDescription);
        TradeInstructionMethod GetTradeInstructionMethod(string tradeInstructionMethod);
        Broker GetBroker(string brokerDescription);
        Task<bool> CancelOrder(string code);
        DataTable GetUnsettledTradesForBalanceCalculation(int clientCompanyId);
    }
}
