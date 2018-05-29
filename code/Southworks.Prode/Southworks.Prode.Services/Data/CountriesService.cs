using System.Linq;
using Southworks.Prode.Data.Models;
using Southworks.Prode.Data.Repositories;

namespace Southworks.Prode.Services.Data
{
    public class CountriesService : ICountriesService, IDataService
    {
        private readonly ICountriesRepository countriesRepository;

        public CountriesService(ICountriesRepository countriesRepository)
        {
            this.countriesRepository = countriesRepository;
        }

        public IQueryable<CountryEntity> GetCountries()
        {
            return this.countriesRepository.Get();
        }
    }
}
