using System;
using System.Linq;
using System.Threading.Tasks;
using Southworks.Prode.Data.Models;

namespace Southworks.Prode.Services.Data
{
    public interface IMatchBetsService
    {
        IQueryable<MatchBetEntity> GetBets();

        IQueryable<MatchBetEntity> GetUserBets(Guid userId);

        Task<MatchBetEntity> SaveBetAsync(MatchBetEntity entity);

        IQueryable<MatchBetEntity> GetBetsByMatch(Guid matchId);
    }
}
