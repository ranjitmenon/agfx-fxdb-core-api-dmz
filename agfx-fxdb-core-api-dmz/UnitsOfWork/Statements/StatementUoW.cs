using System;
using System.Data;
using System.Data.SqlClient;
using Argentex.Core.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Synetec.Data.UnitOfWork.BaseUnitOfWork;
using Synetec.Data.UnitOfWork.GenericRepo;

namespace Argentex.Core.UnitsOfWork.Statements
{
    public class StatementUoW : BaseUow, IStatementUoW
    {
        private IGenericRepo<FxforwardTrade> _fxForwardTradeRepository;
        private IGenericRepo<Payment> _paymentRepository;
        private IGenericRepo<ClientCompany> _clientCompanyRepository;
        private IGenericRepo<BankAccountTransaction> _bankAccountTransactionRepository;
        private IGenericRepo<Currency> _currencyRepository;

        public StatementUoW(FXDB1Context context) : base(context)
        {
        }

        public IGenericRepo<FxforwardTrade> FxForwardTradeRepository => _fxForwardTradeRepository =
            _fxForwardTradeRepository ?? new GenericRepo<FxforwardTrade>(Context);

        public IGenericRepo<Payment> PaymentRepository =>
            _paymentRepository = _paymentRepository ?? new GenericRepo<Payment>(Context);

        public IGenericRepo<ClientCompany> ClientCompanyRepository => _clientCompanyRepository =
            _clientCompanyRepository ?? new GenericRepo<ClientCompany>(Context);

        public IGenericRepo<BankAccountTransaction> BankAccountTransactionRepository =>
            _bankAccountTransactionRepository =
                _bankAccountTransactionRepository ?? new GenericRepo<BankAccountTransaction>(Context);
        
        public IGenericRepo<Currency> CurrencyRepository =>
            _currencyRepository = _currencyRepository ?? new GenericRepo<Currency>(Context);

        public DataTable GetClientCompanyVirtualAccountBalances(int clientCompanyId, DateTime valueDate)
        {
            var formatedDate = valueDate.ToString("yyyy-MM-dd");
            var dt = new DataTable();
            using (var sqlConn = (SqlConnection)Context.Database.GetDbConnection())
            {
                var sql = "ClientCompanyVirtualAccountBalanceDisplay";
                using (var sqlCmd = new SqlCommand(sql, sqlConn))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandTimeout = 300;
                    sqlCmd.Parameters.AddWithValue("@ClientCompanyID", clientCompanyId);
                    sqlCmd.Parameters.AddWithValue("@ValueDate", formatedDate);
                    sqlConn.Open();
                    using (var sqlAdapter = new SqlDataAdapter(sqlCmd))
                    {
                        sqlAdapter.Fill(dt);
                    }
                }
            }
            return dt;
        }
    }
}
