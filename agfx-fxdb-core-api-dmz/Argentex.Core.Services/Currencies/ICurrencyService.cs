using System;
using System.Collections.Generic;
using Argentex.Core.Service.Models.Currencies;

namespace Argentex.Core.Service.Currencies
{
    public interface ICurrencyService : IDisposable
    {
        double GetCurrencyPairRate(string currencyPair);
        CurrencyModel GetCurrency(int currencyId);
        int GetCurrencyId(string code);
        IEnumerable<CurrencyModel> GetCurrencies();
    }
}
