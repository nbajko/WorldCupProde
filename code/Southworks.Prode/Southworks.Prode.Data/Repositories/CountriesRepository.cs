using Southworks.Prode.Data.Models;

namespace Southworks.Prode.Data.Repositories
{
    public class CountriesRepository : BaseRepository<CountryEntity>, ICountriesRepository, IDataRepository
    {
        public CountriesRepository(ProdeDbContext context)
            : base(context, context.CountriesDbSet)
        {
        }
    }
}
