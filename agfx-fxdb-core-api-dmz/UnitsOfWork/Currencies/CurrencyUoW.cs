using System.Linq;
using Argentex.Core.DataAccess.Entities;
using Synetec.Data.UnitOfWork.BaseUnitOfWork;
using Synetec.Data.UnitOfWork.GenericRepo;

namespace Argentex.Core.UnitsOfWork.Currencies
{
    public class CurrencyUoW : BaseUow, ICurrencyUoW
    {
        private IGenericRepo<CurrencyPairPricing> _currencyPairPricingRepository;
        private IGenericRepo<Currency> _currencyRepository;

        public CurrencyUoW(FXDB1Context context) : base(context)
        {
        }

        public IGenericRepo<CurrencyPairPricing> CurrencyPairPricingRepository => _currencyPairPricingRepository =
            _currencyPairPricingRepository ?? new GenericRepo<CurrencyPairPricing>(Context);

        private IGenericRepo<Currency> CurrencyRepository =>
            _currencyRepository = _currencyRepository ?? new GenericRepo<Currency>(Context);

        public IQueryable<Currency> GetCurrency(int currencyId)
        {
            return CurrencyRepository.GetQueryable(x => x.Id == currencyId);
        }

        public IQueryable<Currency> GetCurrencies()
        {
            return CurrencyRepository.GetQueryable();
        }

        public IQueryable<Currency> GetCurrency(string code)
        {
            return CurrencyRepository.GetQueryable(x => x.Code == code.ToUpper());
        }
    }
}
