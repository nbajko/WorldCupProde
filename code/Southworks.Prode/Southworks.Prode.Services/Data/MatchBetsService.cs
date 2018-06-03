using System;
using System.Linq;
using System.Threading.Tasks;
using Southworks.Prode.Data.Models;
using Southworks.Prode.Data.Repositories;

namespace Southworks.Prode.Services.Data
{
    public class MatchBetsService : IMatchBetsService, IDataService
    {
        private readonly IMatchBetsRepository matchBetsRepository;

        public MatchBetsService(IMatchBetsRepository matchBetsRepository)
        {
            this.matchBetsRepository = matchBetsRepository;
        }

        public IQueryable<MatchBetEntity> GetBets()
        {
            return this.matchBetsRepository.Get();
        }

        public IQueryable<MatchBetEntity> GetBetsByMatch(Guid matchId)
        {
            return this.matchBetsRepository.Get(x => x.MatchId.Equals(matchId));
        }

        public IQueryable<MatchBetEntity> GetUserBets(Guid userId)
        {
            return this.matchBetsRepository.Get(x => x.UserId.Equals(userId));
        }

        public async Task<MatchBetEntity> SaveBetAsync(MatchBetEntity entity)
        {
            MatchBetEntity existingEntity = null;
            existingEntity = this.matchBetsRepository.Get(x=> x.MatchId.Equals(entity.MatchId) && x.UserId.Equals(entity.UserId)).FirstOrDefault();

            if (existingEntity != null)
            {
                existingEntity.AwayGoals = entity.AwayGoals;
                existingEntity.HomeGoals = entity.HomeGoals;
                existingEntity.HomePenalties = entity.HomePenalties;
                existingEntity.AwayPenalties = entity.AwayPenalties;
                existingEntity.Result = entity.Result;
            }
            else
            {
                existingEntity = entity;
                existingEntity.Id = Guid.NewGuid();
            }

            await this.matchBetsRepository.SaveAsync(existingEntity);

            return existingEntity;
        }
    }
}
