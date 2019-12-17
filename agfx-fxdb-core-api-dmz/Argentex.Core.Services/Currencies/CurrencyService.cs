using System;
using System.Collections.Generic;
using System.Linq;
using Argentex.Core.Service.Exceptions;
using Argentex.Core.Service.Models.Currencies;
using Argentex.Core.UnitsOfWork.Currencies;

namespace Argentex.Core.Service.Currencies
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyUoW _currencyUoW;

        private bool _disposed;

        public CurrencyService(ICurrencyUoW currencyUoW)
        {
            _currencyUoW = currencyUoW;
        }
        
        public double GetCurrencyPairRate(string currencyPair)
        {
            var currencyPairPricing = _currencyUoW.CurrencyPairPricingRepository
                .Get(x => x.CurrencyPair.Equals(currencyPair, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();

            if(currencyPairPricing == null)
                throw new CurrencyPairPricingNotFoundException($"{currencyPair} does not exist");

            return currencyPairPricing.Rate;
        }

        public CurrencyModel GetCurrency(int currencyId)
        {
            var currency = _currencyUoW
                .GetCurrency(currencyId)
                .Select(x => new CurrencyModel()
                {
                    Code = x.Code
                }).SingleOrDefault();

            if(currency == null)
                throw new CurrencyNotFoundException($"Currency with id {currencyId} does not exist");

            return currency;
        }


        public IEnumerable<CurrencyModel> GetCurrencies()
        {
            return _currencyUoW.GetCurrencies().OrderBy(x => x.Code)
                .Select(x => new CurrencyModel
                {
                    Id = x.Id,
                    Code = x.Code
                })
                .ToList();
        }

        public int GetCurrencyId(string code)
        {
            return _currencyUoW.GetCurrency(code)
                .Select(x => x.Id)
                .SingleOrDefault();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _currencyUoW?.Dispose();
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
