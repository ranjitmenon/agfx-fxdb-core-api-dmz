using Argentex.Core.Service.Models.Country;
using Argentex.Core.UnitsOfWork.Countries;
using System.Collections.Generic;
using System.Linq;

namespace Argentex.Core.Service.Country
{
    public class CountryService : ICountryService
    {
        private readonly ICountryUow _countryUow;
        private bool _disposed;

        public CountryService(ICountryUow countryUow)
        {
            _countryUow = countryUow;
        }

        public IEnumerable<CountryModel> GetCountries()
        {
            return _countryUow.GetCountries()
                .Select(x => new CountryModel {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _countryUow?.Dispose();
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
