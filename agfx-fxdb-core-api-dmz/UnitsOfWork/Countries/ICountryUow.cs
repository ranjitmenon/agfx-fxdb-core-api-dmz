using Argentex.Core.DataAccess.Entities;
using Synetec.Data.UnitOfWork.BaseUnitOfWork;
using System.Linq;

namespace Argentex.Core.UnitsOfWork.Countries
{
    public interface ICountryUow : IBaseUow
    {
        IQueryable<Country> GetCountries();
    }
}
