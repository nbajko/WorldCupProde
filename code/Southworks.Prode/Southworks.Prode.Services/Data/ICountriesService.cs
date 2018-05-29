using System.Linq;
using Southworks.Prode.Data.Models;

namespace Southworks.Prode.Services.Data
{
    public interface ICountriesService
    {
        IQueryable<CountryEntity> GetCountries();
    }
}
