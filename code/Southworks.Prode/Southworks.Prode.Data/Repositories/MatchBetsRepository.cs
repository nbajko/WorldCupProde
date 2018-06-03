using Southworks.Prode.Data.Models;

namespace Southworks.Prode.Data.Repositories
{
    public class MatchBetsRepository : BaseRepository<MatchBetEntity>, IMatchBetsRepository, IDataRepository
    {
        public MatchBetsRepository(ProdeDbContext context)
            : base(context, context.MatchBetsDbSet)
        {
        }
    }
}
