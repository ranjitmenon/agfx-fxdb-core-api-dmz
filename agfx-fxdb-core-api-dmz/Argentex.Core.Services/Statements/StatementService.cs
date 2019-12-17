using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Argentex.Core.DataAccess.Entities;
using Argentex.Core.Service.Models.Statements;
using Argentex.Core.UnitsOfWork.Statements;
using Microsoft.EntityFrameworkCore;

namespace Argentex.Core.Service.Statements
{
    public class StatementService : IStatementService
    {
        private readonly IStatementUoW _statementUoW;

        private bool _disposed;

        public StatementService(IStatementUoW statementUoW)
        {
            _statementUoW = statementUoW;
        }

        public IDictionary<string, List<StatementModel>> GetStatements(int clientCompanyId, DateTime startDate, DateTime endDate)
        {
            var paymentsAndTrades = AddTransactionsFromPayments(clientCompanyId, startDate, endDate);
            return paymentsAndTrades;
        }

        public bool CheckCompany(int clientCompanyId)
        {
            return _statementUoW
                .ClientCompanyRepository
                .GetQueryable(x => x.Id == clientCompanyId)
                .Any();
        }

        private IDictionary<string, List<StatementModel>> AddTransactionsFromPayments(int clientCompanyId, DateTime startDate, DateTime endDate)
        {
            //get list of payment ids for the company and the given dates
            var paymentIDs = _statementUoW.PaymentRepository.GetQueryable(x => x.ClientCompanyId == clientCompanyId
            && !x.PaymentSwiftoutgoingStatus.IsSwiftRejected
            && x.ValueDate.Date >= startDate.Date
                && x.ValueDate.Date <= endDate.Date).Select(x => x.Id).ToList();

            //get the list of trades for the company and the given dates
            var tradeCodes = _statementUoW.FxForwardTradeRepository.GetQueryable(x => x.ClientCompanyId == clientCompanyId
                            && x.ClientCompanyId == clientCompanyId
                            && x.ValueDate.HasValue
                            && x.ValueDate.Value.Date >= startDate.Date
                            && x.ValueDate.Value.Date <= endDate.Date
                            && !x.Deleted
                            && !x.TransactionCommitId.HasValue).Select(x => x.Code).ToList();

            var transactions = _statementUoW.BankAccountTransactionRepository
               .GetQueryable(x => (paymentIDs.Count > 0
                                       && x.PaymentId.HasValue
                                       && paymentIDs.Contains(x.PaymentId.Value))
                                   ||
                                   (tradeCodes.Count > 0
                                       && !string.IsNullOrWhiteSpace(x.FxforwardTradeCode)
                                       && tradeCodes.Contains(x.FxforwardTradeCode)))
               .Include(x => x.Payment)
               .Include(x => x.FxforwardTradeCodeNavigation)
               .Include(x => x.Currency)
               .Select(x => new BankAccountTransaction()
               {
                   Id = x.Id,
                   PaymentId = x.PaymentId,
                   Amount = x.Amount,
                   BankAccountId = x.BankAccountId,
                   FxforwardTradeCode = x.FxforwardTradeCode,
                   IsDebit = x.IsDebit,
                   Currency = x.Currency != null ? new Currency() { Code = x.Currency.Code, Id = x.Currency.Id}
                   : null,
                   CurrencyId = x.CurrencyId,
                   FxforwardTradeCodeNavigation = x.FxforwardTradeCodeNavigation != null ? new FxforwardTrade()
                   {
                       Code = x.FxforwardTradeCodeNavigation.Code ,
                       ValueDate = x.FxforwardTradeCodeNavigation.ValueDate
                   } : null
                   ,
                   Payment = x.Payment != null ? new Payment()
                   {
                       Code = x.Payment.Code, ValueDate = x.Payment.ValueDate 
                   } : null
               })
            .GroupBy(x => x.Currency.Code)
            .ToDictionary(x => x.Key, x => x.Select(y => new StatementModel()
            {

                PaymentCode = y.Payment?.Code,
                TradeCode = y.FxforwardTradeCode,
                BankAccountId = y.BankAccountId,
                ValueDate = y.Payment?.Code != null ? y.Payment.ValueDate : y.FxforwardTradeCodeNavigation.ValueDate.Value,
                Event = y.Payment?.Code != null ? $"Payment {y.Payment.Code}" : $"Trade {y.FxforwardTradeCode}",
                IsDebit = y.IsDebit,
                Amount = y.Amount ?? 0m
            })
            .OrderByDescending(y => y.ValueDate)
            .ToList());

            return transactions;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _statementUoW?.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
