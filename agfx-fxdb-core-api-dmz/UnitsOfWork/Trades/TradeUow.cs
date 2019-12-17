using Argentex.Core.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Synetec.Data.UnitOfWork.BaseUnitOfWork;
using Synetec.Data.UnitOfWork.GenericRepo;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Argentex.Core.UnitsOfWork.Trades
{
    public class TradeUow : BaseUow, ITradeUow
    {
        private IGenericRepo<FxforwardTrade> _tradeRepository;
        private IGenericRepo<FxforwardTradeSwapCount> _tradeSwapCountRepository;
        private IGenericRepo<LogFxforwardTrade> _tradeLogRepository;
        private IGenericRepo<Currency> _currencyRepo;
        private IGenericRepo<CurrencyPairValidation> _currencyPairValidationRepo;
        private IGenericRepo<ClientCompanyTradeCount> _tradeCountRepo;
        private IGenericRepo<VirtualAccountTransaction> _virtualAccountTransactionRepo;
        private IGenericRepo<VirtualAccountType> _virtualAccountTypeRepo;
        private IGenericRepo<BankAccountTransaction> _bankAccountTransactionRepo;
        private IGenericRepo<ClientCompanyVirtualAccount> _clientCompanyVirtualAccountRepo;
        private IGenericRepo<Broker> _brokerRepo;
        private IGenericRepo<FxforwardTradeStatus> _tradeStatusRepository;
        private IGenericRepo<Emirstatus> _emirStatusRepository;
        private IGenericRepo<TradeInstructionMethod> _tradeInstructionMethodRepository;               

        #region Properties

        private IGenericRepo<FxforwardTrade> TradeRepository =>            
            _tradeRepository = _tradeRepository ?? new GenericRepo<FxforwardTrade>(Context);

        private IGenericRepo<FxforwardTradeSwapCount> TradeSwapCountRepository =>
            _tradeSwapCountRepository = _tradeSwapCountRepository ?? new GenericRepo<FxforwardTradeSwapCount>(Context);

        private IGenericRepo<LogFxforwardTrade> TradeLogRepository =>
            _tradeLogRepository = _tradeLogRepository ?? new GenericRepo<LogFxforwardTrade>(Context);

        private IGenericRepo<Currency> CurrencyRepo =>
            _currencyRepo = _currencyRepo ?? new GenericRepo<Currency>(Context);

        private IGenericRepo<CurrencyPairValidation> CurrencyPairValidationRepo =>
            _currencyPairValidationRepo = _currencyPairValidationRepo ?? new GenericRepo<CurrencyPairValidation>(Context);

        private IGenericRepo<VirtualAccountTransaction> VirtualAccountTransactionRepository =>
            _virtualAccountTransactionRepo = _virtualAccountTransactionRepo ?? new GenericRepo<VirtualAccountTransaction>(Context);

        private IGenericRepo<VirtualAccountType> VirtualAccountTypeRepository =>
            _virtualAccountTypeRepo = _virtualAccountTypeRepo ?? new GenericRepo<VirtualAccountType>(Context);

        private IGenericRepo<ClientCompanyVirtualAccount> ClientCompanyVirtualAccountRepository =>
            _clientCompanyVirtualAccountRepo = _clientCompanyVirtualAccountRepo ?? new GenericRepo<ClientCompanyVirtualAccount>(Context);

        private IGenericRepo<BankAccountTransaction> BankAccountTransactionRepository =>
            _bankAccountTransactionRepo = _bankAccountTransactionRepo ?? new GenericRepo<BankAccountTransaction>(Context);

        private IGenericRepo<ClientCompanyTradeCount> ClientCompanyTradeCountRepository =>
            _tradeCountRepo = _tradeCountRepo ?? new GenericRepo<ClientCompanyTradeCount>(Context);

        private IGenericRepo<Broker> BrokerRepository =>
            _brokerRepo = _brokerRepo ?? new GenericRepo<Broker>(Context);
      
        private IGenericRepo<FxforwardTradeStatus> TradeStatusRepository =>
            _tradeStatusRepository = _tradeStatusRepository ?? new GenericRepo<FxforwardTradeStatus>(Context);

        private IGenericRepo<Emirstatus> EmirStatusRepository =>
            _emirStatusRepository = _emirStatusRepository ?? new GenericRepo<Emirstatus>(Context);

        private IGenericRepo<TradeInstructionMethod> TradeInstructionMethodRepository =>
            _tradeInstructionMethodRepository = _tradeInstructionMethodRepository ?? new GenericRepo<TradeInstructionMethod>(Context);
        #endregion

        public TradeUow(FXDB1Context context) : base(context)
        {
        }

        public DataTable GetUnsettledTrades(int clientCompanyId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlConn = (SqlConnection)Context.Database.GetDbConnection())
            {
                string sql = "ClientCompanyGetClientOpenTradeSummary";
                using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@ClientCompanyID", clientCompanyId);
                    sqlConn.Open();
                    using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                    {
                        sqlAdapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public IQueryable<Currency> GetCurrencies()
        {
            return CurrencyRepo.GetQueryable();
        }

        public IQueryable<CurrencyPairValidation> GetCurrencyPairValidation()
        {
            return CurrencyPairValidationRepo.GetQueryable();
        }

        public bool ExecuteOrder(FxforwardTrade trade, ClientCompanyTradeCount tradeCountObject)
        {
            bool isSuccessful = false;

            ClientCompanyTradeCountRepository.Update(tradeCountObject);

            TradeSwapCountRepository.Insert(new FxforwardTradeSwapCount { FxforwardTradeCode = trade.Code, SwapCount = 0});

            TradeRepository.Insert(trade);

            SaveContext();
            isSuccessful = true;

            return isSuccessful;
        }

        public bool CreateDeal(FxforwardTrade trade, ClientCompanyTradeCount tradeCountObject)
        {
            bool isSuccessful = false;

            ClientCompanyTradeCountRepository.Update(tradeCountObject);
            TradeSwapCountRepository.Insert(new FxforwardTradeSwapCount { FxforwardTradeCode = trade.Code, SwapCount = 0 });
            TradeRepository.Insert(trade);
            InsertTradeLog(trade, "INSERT");
            SaveContext();

            CreateTradeTransactions(trade);
            SaveContext();

            isSuccessful = true;

            return isSuccessful;
        }

        public bool BrokerDeal(FxforwardTrade trade, ClientCompanyTradeCount tradeCountObject)
        {
            bool isSuccessful = false;

            TradeRepository.Update(trade);
            InsertTradeLog(trade, "UPDATE");
            SaveContext();

            BrokerTradeTransactions(trade);
            SaveContext();

            isSuccessful = true;

            return isSuccessful;
        }

        public void RejectOrder(FxforwardTrade trade)
        {
            trade.Deleted = true;
            TradeRepository.Update(trade);

            SaveContext();
        }

        public ClientCompanyTradeCount GetTradeCountByPrimaryKey(int clientCompanyId)
        {
            var tradeCountObject = ClientCompanyTradeCountRepository.GetByPrimaryKey(clientCompanyId);
            if(tradeCountObject == null) throw new NullReferenceException($"Cannot find trade count for ClientCompanyId: {clientCompanyId}");

            return tradeCountObject;
        }

        public IQueryable<FxforwardTrade> GetTrade(string tradeCode)
        {            
            return TradeRepository.GetQueryable(x => x.Code == tradeCode);
        }

        public IQueryable<ClientCompanyTradeCount> GetClientCompanyTradeCount(int clientCompanyId)
        {
            return ClientCompanyTradeCountRepository.GetQueryable(x => x.ClientCompanyId == clientCompanyId);
        }

        public DataTable GetClosedTrades(int clientCompanyId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlConn = (SqlConnection)Context.Database.GetDbConnection())
            {
                string sql = "ClientCompanyGetClientDeliveredTradeSummary";
                using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@ClientCompanyID", clientCompanyId);
                    sqlConn.Open();
                    using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                    {
                        sqlAdapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public IQueryable<FxforwardTrade> GetOpenOrders(int clientCompanyId)
        {
            var pendingStatus = TradeStatusRepository.Get(x => x.Description == "Pending").SingleOrDefault();

            var orders = TradeRepository
                .GetQueryable(x => x.ClientCompanyId == clientCompanyId &&
                x.IsOrder == true &&
                x.Deleted == false &&
                x.FxforwardTradeStatusId == pendingStatus.Id
            ).OrderByDescending(x => x.CreatedDate);

            return orders;
        }

        /// <summary>
        /// Getting open orders that have the validity date expired
        /// </summary>
        /// <returns>FxforwardTrade</returns>
        public IQueryable<FxforwardTrade> GetExpiredValidityOrders()
        {
            var pendingStatus = TradeStatusRepository.Get(x => x.Description == "Pending").SingleOrDefault();

            var validityDateCompare = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 0, 0);

            var orders = TradeRepository
                .GetQueryable(x => 
                x.IsOrder == true &&
                x.Deleted == false &&
                x.OpenValueDate < validityDateCompare &&
                x.FxforwardTradeStatusId == pendingStatus.Id
            ).OrderByDescending(x => x.CreatedDate);

            return orders;
        }

        public void UpdateTrade(FxforwardTrade trade)
        {
            _tradeRepository.Update(trade);
            SaveContext();
        }

        public FxforwardTrade GetTrade(string tradeCode, bool getAdditionalProperties)
        {
            if(getAdditionalProperties)
            {
                return TradeRepository
                    .GetQueryable(x => x.Code == tradeCode, orderBy: null, includeProperties: "Rhsccy,Lhsccy,ClientCompanyNavigation,ClientCompanyOpi")
                    .Single();
            }
            else
            {
                return GetTrade(tradeCode).Single();
            }
        }

        public FxforwardTradeStatus GetFxForwardStatus(string statusDescription)
        {
             return TradeStatusRepository.Get(x => x.Description == statusDescription).SingleOrDefault();
        }

        public Emirstatus GetEmirStatus(string emirStatusDescription)
        {
            return EmirStatusRepository.Get(x => x.Description == emirStatusDescription).SingleOrDefault();
        }

        public TradeInstructionMethod GetTradeInstructionMethod(string tradeInstructionMethod)
        {
            return TradeInstructionMethodRepository.Get(x => x.Description == tradeInstructionMethod).SingleOrDefault();
        }

        public Broker GetBroker(string brokerDescription)
        {
            return BrokerRepository.Get(x => x.Description == brokerDescription).SingleOrDefault();
        }

        public async Task<bool> CancelOrder(string code)
        {
            var order = TradeRepository.GetByPrimaryKey(code);
            order.Deleted = true;
            TradeRepository.Update(order);
            await SaveContextAsync();
            return order.Deleted;
        }

        // This method was created because there is a connection conflict if using GetUnsettledTrades() when connection is opened and closed
        public DataTable GetUnsettledTradesForBalanceCalculation(int clientCompanyId)
        {
            DataTable dt = new DataTable();
            SqlConnection sqlConn = (SqlConnection)Context.Database.GetDbConnection();

            string sql = "ClientCompanyGetClientOpenTradeSummary";
            using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@ClientCompanyID", clientCompanyId);
                
                using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                {
                    sqlAdapter.Fill(dt);
                }
            }

            return dt;
        }

        #region Private methods

        private void CreateTradeTransactions(FxforwardTrade trade)
        {
            CheckAccountExistsAndIfNotCreate(trade.ClientCompanyId);

            var debitVat = CreateVirtualAccountTransaction(trade, "X", true, false);
            VirtualAccountTransactionRepository.Insert(debitVat);

            var creditVat = CreateVirtualAccountTransaction(trade, "Y", false, false);
            VirtualAccountTransactionRepository.Insert(creditVat);
        }

        private void BrokerTradeTransactions(FxforwardTrade trade)
        {
            var debitBat = CreateBankAccountTransaction(trade, true);
            BankAccountTransactionRepository.Insert(debitBat);

            var creditBat = CreateBankAccountTransaction(trade, false);
            BankAccountTransactionRepository.Insert(creditBat);

            bool isDebit = false;
            if (trade.Profit <= 0)
            {
                isDebit = true;
                trade.Profit = trade.Profit * -1;
            }
            var vat = CreateVirtualAccountTransaction(trade, "Y", isDebit, true);
            VirtualAccountTransactionRepository.Insert(vat);
        }

        private BankAccountTransaction CreateBankAccountTransaction(FxforwardTrade trade, bool isDebit)
        {
            var barclaysBroker = BrokerRepository.GetByPrimaryKey(trade.BrokerId);

            BankAccountTransaction bat = new BankAccountTransaction
            {
                BankAccountId = isDebit ? barclaysBroker.BankAccountBrokerPaymentsOutId.Value : barclaysBroker.BankAccountBrokerPaymentsInId.Value,
                CurrencyId = isDebit ?
                    trade.IsBuy ? trade.Rhsccyid.Value : trade.Lhsccyid.Value :
                    trade.IsBuy ? trade.Lhsccyid.Value : trade.Rhsccyid.Value,
                Amount = isDebit ?
                    trade.IsBuy ? trade.BrokerRhsamt.Value : trade.BrokerLhsamt.Value :
                    trade.IsBuy ? trade.BrokerLhsamt.Value : trade.BrokerRhsamt.Value,
                IsDebit = isDebit,
                PaymentId = null,
                FxforwardTradeCode = trade.Code
            };
            return bat;
        }

        private VirtualAccountTransaction CreateVirtualAccountTransaction(FxforwardTrade trade, string accountDescription, bool isDebit, bool isProfitTransaction)
        {
            VirtualAccountTransaction vat;

            if (isProfitTransaction)
            {
                vat = new VirtualAccountTransaction
                {
                    VirtualAccountId = GetHouseAccountId(accountDescription),
                    CurrencyId = trade.IsRhsmajor.Value ? trade.Lhsccyid.Value : trade.Rhsccyid.Value,
                    Amount = trade.Profit,
                    IsDebit = isDebit,
                    PaymentId = null,
                    FxforwardTradeCode = trade.Code,
                    IsProfitTransaction = true
                };
            }
            else
            {
                vat = new VirtualAccountTransaction
                {
                    VirtualAccountId = GetVirtualAccountId(accountDescription, trade.ClientCompanyId),
                    CurrencyId = isDebit ?
                        trade.IsBuy ? trade.Rhsccyid.Value : trade.Lhsccyid.Value :
                        trade.IsBuy ? trade.Lhsccyid.Value : trade.Rhsccyid.Value,
                    Amount = isDebit ?
                        trade.IsBuy ? trade.BrokerRhsamt.Value : trade.BrokerLhsamt.Value :
                        trade.IsBuy ? trade.BrokerLhsamt.Value : trade.BrokerRhsamt.Value,
                    IsDebit = isDebit,
                    PaymentId = null,
                    FxforwardTradeCode = trade.Code,
                    IsProfitTransaction = false
                };
            }

            return vat;
        }

        private void CheckAccountExistsAndIfNotCreate(int clientCompanyId)
        {
            bool associationExists = true;

            if (!ClientCompanyVirtualAccountExists(clientCompanyId, "X"))
            {
                VirtualAccountCreate(clientCompanyId, "X");
                associationExists = false;
            }

            if (!ClientCompanyVirtualAccountExists(clientCompanyId, "Y"))
            {
                VirtualAccountCreate(clientCompanyId, "Y");
                associationExists = false;
            }

            if (!associationExists)
            {
                if (!ClientCompanyVirtualAccountExists(clientCompanyId, "A"))
                {
                    VirtualAccountCreate(clientCompanyId, "A");
                }

                if (!ClientCompanyVirtualAccountExists(clientCompanyId, "B"))
                {
                    VirtualAccountCreate(clientCompanyId, "B");
                }

                if (!ClientCompanyVirtualAccountExists(clientCompanyId, "Collateral"))
                {
                    VirtualAccountCreate(clientCompanyId, "Collateral");
                }
            }
        }

        private void VirtualAccountCreate(int clientCompanyId, string description)
        {
            ClientCompanyVirtualAccount ccva = new ClientCompanyVirtualAccount
            {
                ClientCompanyId = clientCompanyId,
                VirtualAccountTypeId = GetVirtualAccountTypeId(description)
            };

            ClientCompanyVirtualAccountRepository.Insert(ccva);
            SaveContext();
        }

        private bool ClientCompanyVirtualAccountExists(int clientCompanyId, string description)
        {
            var account = ClientCompanyVirtualAccountRepository
                .GetQueryable(x => x.ClientCompanyId == clientCompanyId && x.VirtualAccountType.Description == description);

            return account.Any();
        }

        private int GetVirtualAccountTypeId(string accountDescription)
        {
            var VirtualAccountTypeId = VirtualAccountTypeRepository
                .GetQueryable(x => x.Description == accountDescription)
                .Select(vat => vat.Id).FirstOrDefault();

            return VirtualAccountTypeId;
        }

        private int GetVirtualAccountId(string accountDescription, int clientCompanyId)
        {
            var clientCompanyVirtualAccountId = ClientCompanyVirtualAccountRepository
                .GetQueryable(x => x.ClientCompanyId == clientCompanyId && x.VirtualAccountType.Description == accountDescription)
                .Select(ccva => ccva.Id).FirstOrDefault();

            return clientCompanyVirtualAccountId;
        }

        private int GetHouseAccountId(string accountDescription)
        {
            var clientCompanyVirtualAccountId = ClientCompanyVirtualAccountRepository
                .GetQueryable(x => x.ClientCompany.IsHouseAccount == true && x.VirtualAccountType.Description == accountDescription)
                .Select(ccva => ccva.Id).FirstOrDefault();

            return clientCompanyVirtualAccountId;
        }

        private void InsertTradeLog(FxforwardTrade trade, string action)
        {
            var tradeLog = new LogFxforwardTrade
            {
                LogAction = action,
                UpdatedDate = DateTime.Now,
                Code = trade.Code,
                CreatedDate = trade.CreatedDate,
                CreatedByAuthUserId = trade.CreatedByAuthUserId,
                ClientCompanyId = trade.ClientCompanyId,
                AuthorisedByClientCompanyContactId = trade.AuthorisedByClientCompanyContactId,
                Verified = trade.Verified,
                ContractDate = trade.ContractDate,
                ValueDate = trade.ValueDate,
                IsOrder = trade.IsOrder,
                CurrencyPair = trade.CurrencyPair,
                IsBuy = trade.IsBuy,
                Lhsccyid = trade.Lhsccyid,
                Rhsccyid = trade.Rhsccyid,
                ClientRate = trade.ClientRate,
                BrokerRate = trade.BrokerRate,
                CollateralPerc = trade.CollateralPerc,
                UpdatedByAuthUserId = trade.UpdatedByAuthUserId,
                IsRhsmajor = trade.IsRhsmajor,
                ProfitConsolidated = trade.ProfitConsolidated,
                Deleted = trade.Deleted,
                EmirReported = trade.EmirReported,
                IsComplianceSupported = trade.IsComplianceSupported,
                IsComplianceRegulated = trade.IsComplianceRegulated,
                TradeInstructionMethodId = trade.TradeInstructionMethodId,
                FxforwardTradeStatusId = trade.FxforwardTradeStatusId,
                EmirUti = trade.EmirUti,
                BrokerId = trade.BrokerId,
                ClientLhsamt = trade.ClientLhsamt,
                BrokerLhsamt = trade.BrokerLhsamt,
                ClientRhsamt = trade.ClientRhsamt,
                BrokerRhsamt = trade.BrokerRhsamt,
                RemainingClientLhsamt = trade.RemainingClientLhsamt,
                RemainingClientRhsamt = trade.RemainingClientRhsamt,
                Profit = trade.Profit
            };

            TradeLogRepository.Insert(tradeLog);
        }

        #endregion
    }
}
