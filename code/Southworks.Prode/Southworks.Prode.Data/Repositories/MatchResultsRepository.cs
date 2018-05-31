using Southworks.Prode.Data.Models;

namespace Southworks.Prode.Data.Repositories
{
    public class MatchResultsRepository : BaseRepository<MatchResultEntity>, IMatchResultsRepository, IDataRepository
    {
        public MatchResultsRepository(ProdeDbContext context)
            : base(context, context.MatchResultsDbSet)
        {
        }
    }
}
