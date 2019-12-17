using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Argentex.Core.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Synetec.Data.UnitOfWork.BaseUnitOfWork;
using Synetec.Data.UnitOfWork.GenericRepo;

namespace Argentex.Core.UnitsOfWork.Payments
{
    public class PaymentUoW : BaseUow, IPaymentUoW
    {
        #region Repositories

        private IGenericRepo<Payment> _paymentRepository;
        private IGenericRepo<ClientCompanyOpitransaction> _clientCompanyOpiTransactionRepository;
        private IGenericRepo<PaymentType> _paymentTypeRepository;

        private IGenericRepo<Payment> PaymentRepository =>
            _paymentRepository = _paymentRepository ?? new GenericRepo<Payment>(Context);

        private IGenericRepo<ClientCompanyOpitransaction> ClientCompanyOpiTransactionRepository =>
            _clientCompanyOpiTransactionRepository = _clientCompanyOpiTransactionRepository ??
                                                     new GenericRepo<ClientCompanyOpitransaction>(Context);

        private IGenericRepo<PaymentType> PaymentTypeRepository =>
            _paymentTypeRepository = _paymentTypeRepository ?? new GenericRepo<PaymentType>(Context);

        #endregion

        public PaymentUoW(FXDB1Context context) : base(context)
        {
        }
        
        public IQueryable<Payment> GetPayment(string paymentCode)
        {
            return PaymentRepository.GetQueryable(x => x.Code == paymentCode);
        }

        public IQueryable<ClientCompanyOpitransaction> GetClientCompanyOpiTransaction(string paymentCode)
        {
            return ClientCompanyOpiTransactionRepository.GetQueryable(x => x.Payment.Code == paymentCode)
                .Include(x => x.ClientCompanyOpi);
        }

        public IEnumerable<PaymentType> GetPaymentType(string paymentType)
        {
            return PaymentTypeRepository.GetQueryable(x => x.Description == paymentType);
        }

        public DataTable MakePayment(Payment payment, ClientCompanyOpi clientCompanyOpi, 
            BankAccount debitBankAccount, ClientCompanyVirtualAccount debitVirtualAccount, int paymentSwiftOutgoingStatusId, bool paymentAuthorised, string authUserName)
        {
            DataTable dataTable = new DataTable();            
            using (SqlConnection sqlConn = (SqlConnection)Context.Database.GetDbConnection())
            {                
                string sql = "PaymentCreate";
                using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    #region Sql parameters
                    sqlCmd.Parameters.Add(new SqlParameter("@PaymentTypeID", SqlDbType.Int) { Value = payment.PaymentType.Id });
                    sqlCmd.Parameters.Add(new SqlParameter("@IsSWIFTPayment", SqlDbType.Bit) { Value = payment.PaymentType.DefaultSendToSwift });
                    sqlCmd.Parameters.Add(new SqlParameter("@AuthUserName", SqlDbType.NVarChar, 50) { Value = authUserName });
                    sqlCmd.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Decimal) { Value = payment.Amount, Precision = 25, Scale = 8 });
                    sqlCmd.Parameters.Add(new SqlParameter("@CurrencyID", SqlDbType.Int) { Value = payment.Currency.Id });
                    sqlCmd.Parameters.Add(new SqlParameter("@ValueDate", SqlDbType.DateTime) { Value = payment.ValueDate });
                    sqlCmd.Parameters.Add(new SqlParameter("@NotifyClient", SqlDbType.Bit) { Value = payment.NotifyClient });
                    sqlCmd.Parameters.Add(new SqlParameter("@Comments", SqlDbType.NVarChar, -1) { Value = payment.Comments });
                    sqlCmd.Parameters.Add(new SqlParameter("@PaymentSwiftOutgoingStatusId", SqlDbType.Int) { Value = paymentSwiftOutgoingStatusId });
                    sqlCmd.Parameters.Add(new SqlParameter("@Authorised", SqlDbType.Bit) { Value = paymentAuthorised });

                    if (!string.IsNullOrWhiteSpace(payment.Reference))
                    {
                        sqlCmd.Parameters.Add(new SqlParameter("@Reference", SqlDbType.NVarChar, 255) { Value = payment.Reference });
                    }

                    if (payment.ClientCompany != null)
                    {
                        sqlCmd.Parameters.Add(new SqlParameter("@ClientCompanyID", SqlDbType.Int) { Value = payment.ClientCompany.Id });
                    }

                    if (debitBankAccount != null)
                    {
                        sqlCmd.Parameters.Add(new SqlParameter("@DebitBankAccountID", SqlDbType.Int) { Value = debitBankAccount.Id });
                    }

                    if (debitVirtualAccount != null)
                    {
                        sqlCmd.Parameters.Add(new SqlParameter("@DebitClientCompanyVirtualAccountID", SqlDbType.Int) { Value = debitVirtualAccount.Id });
                    }

                    if (clientCompanyOpi != null)
                    {
                        sqlCmd.Parameters.Add(new SqlParameter("@CreditClientCompanyOPIID", SqlDbType.Int) { Value = clientCompanyOpi.Id });
                    } 
                    #endregion Sql parameters

                    sqlConn.Open();
                    using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                    {
                        sqlAdapter.Fill(dataTable);
                    }                    
                }
            }
            return dataTable;
        }

        public IQueryable<Payment> GetPaymentNotification(string paymentCode)
        {
            return PaymentRepository
                .GetQueryable(x => x.Code == paymentCode, null, "ClientCompany, Currency, ClientCompanyOpitransaction.ClientCompanyOpi, PaymentType");
        }
    }
}
