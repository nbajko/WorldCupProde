using System;
using System.Linq;
using System.Threading.Tasks;
using Southworks.Prode.Data.Models;

namespace Southworks.Prode.Services.Data
{
    public interface IMatchResultsService
    {
        IQueryable<MatchResultEntity> GetResults();

        Task<MatchResultEntity> SaveResultAsync(MatchResultEntity entity);
    }
}
