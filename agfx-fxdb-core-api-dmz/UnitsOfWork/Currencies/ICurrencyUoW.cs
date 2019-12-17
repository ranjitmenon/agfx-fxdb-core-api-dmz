using System.Collections.Generic;
using System.Linq;
using Argentex.Core.DataAccess.Entities;
using Synetec.Data.UnitOfWork.BaseUnitOfWork;
using Synetec.Data.UnitOfWork.GenericRepo;

namespace Argentex.Core.UnitsOfWork.Currencies
{
    public interface ICurrencyUoW : IBaseUow
    {
        IGenericRepo<CurrencyPairPricing> CurrencyPairPricingRepository { get; }

        IQueryable<Currency> GetCurrency(int currencyId);
        IQueryable<Currency> GetCurrency(string code);

        IQueryable<Currency> GetCurrencies();
    }
}
