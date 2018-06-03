using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Southworks.Prode.Data.Models;

namespace Southworks.Prode.Services.Data
{
    public interface IBetResultsService
    {
        IQueryable<BetResultEntity> GetBetResults();

        IQueryable<BetResultEntity> GetBetResultsByMatch(Guid matchId);

        Task SaveBetResults(IEnumerable<BetResultEntity> entities);
    }
}
