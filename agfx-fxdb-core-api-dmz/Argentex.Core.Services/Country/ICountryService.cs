using Argentex.Core.Service.Models.Country;
using System;
using System.Collections.Generic;

namespace Argentex.Core.Service.Country
{
    public interface ICountryService : IDisposable
    {
        IEnumerable<CountryModel> GetCountries();
    }
}
