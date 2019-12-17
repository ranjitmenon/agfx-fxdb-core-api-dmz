using System;
using System.Data;
using Argentex.Core.DataAccess.Entities;
using Synetec.Data.UnitOfWork.BaseUnitOfWork;
using Synetec.Data.UnitOfWork.GenericRepo;

namespace Argentex.Core.UnitsOfWork.Statements
{
    public interface IStatementUoW : IBaseUow
    {
        IGenericRepo<FxforwardTrade> FxForwardTradeRepository { get; }
        IGenericRepo<Payment> PaymentRepository { get; }
        IGenericRepo<ClientCompany> ClientCompanyRepository { get; }
        IGenericRepo<BankAccountTransaction> BankAccountTransactionRepository { get; }
        IGenericRepo<Currency> CurrencyRepository { get; }

        DataTable GetClientCompanyVirtualAccountBalances(int clientCompanyId, DateTime valueDate);
    }
}
