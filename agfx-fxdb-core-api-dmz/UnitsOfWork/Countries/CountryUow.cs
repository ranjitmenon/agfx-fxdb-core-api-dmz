using Argentex.Core.DataAccess.Entities;
using Synetec.Data.UnitOfWork.BaseUnitOfWork;
using Synetec.Data.UnitOfWork.GenericRepo;
using System.Linq;

namespace Argentex.Core.UnitsOfWork.Countries
{
    public class CountryUow : BaseUow, ICountryUow
    {
        private IGenericRepo<Country> _countryRepository;

        private IGenericRepo<Country> CountryRepository =>
            _countryRepository = _countryRepository ?? new GenericRepo<Country>(Context);

        public CountryUow(FXDB1Context context) : base(context)
        {
        }

        public IQueryable<Country> GetCountries()
        {
            return CountryRepository
                .GetQueryable();
        }
    }
}
