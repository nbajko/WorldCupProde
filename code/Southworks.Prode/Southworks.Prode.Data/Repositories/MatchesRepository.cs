using Southworks.Prode.Data.Models;

namespace Southworks.Prode.Data.Repositories
{
    public class MatchesRepository : BaseRepository<MatchEntity>, IMatchesRepository, IDataRepository
    {
        public MatchesRepository(ProdeDbContext context)
            : base(context, context.MatchesDbSet)
        {
        }
    }
}
