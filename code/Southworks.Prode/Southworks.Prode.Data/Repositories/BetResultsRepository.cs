using Southworks.Prode.Data.Models;

namespace Southworks.Prode.Data.Repositories
{
    public class BetResultsRepository : BaseRepository<BetResultEntity>, IBetResultsRepository, IDataRepository
    {
        public BetResultsRepository(ProdeDbContext context)
            : base(context, context.BetResultsDbSet)
        {
        }
    }
}
