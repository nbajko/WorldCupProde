using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Southworks.Prode.Data.Models;
using Southworks.Prode.Data.Repositories;

namespace Southworks.Prode.Services.Data
{
    public class BetResultsService : IBetResultsService, IDataService
    {
        private readonly IBetResultsRepository betResultsRepository;

        public BetResultsService(IBetResultsRepository betResultsRepository)
        {
            this.betResultsRepository = betResultsRepository;
        }

        public IQueryable<BetResultEntity> GetBetResults()
        {
            return this.betResultsRepository.Get();
        }

        public IQueryable<BetResultEntity> GetBetResultsByMatch(Guid matchId)
        {
            return this.betResultsRepository.Get(x => x.MatchId.Equals(matchId));
        }

        public async Task SaveBetResults(IEnumerable<BetResultEntity> entities)
        {
            foreach (var entity in entities)
            {
                BetResultEntity existingEntity = null;
                existingEntity = await this.betResultsRepository.GetAsync(entity.Id);

                if (existingEntity != null)
                {
                    existingEntity.HitAwayGoals = entity.HitAwayGoals;
                    existingEntity.HitExactResult = entity.HitExactResult;
                    existingEntity.HitGoalsDif = entity.HitGoalsDif;
                    existingEntity.HitHomeGoals = entity.HitHomeGoals;
                    existingEntity.HitPenalties = entity.HitPenalties;
                    existingEntity.HitResult = entity.HitResult;
                    existingEntity.Points = entity.Points;
                    existingEntity.ExtraPoint = entity.ExtraPoint;
                }
                else
                {
                    existingEntity = entity;
                }

                await this.betResultsRepository.SaveAsync(existingEntity);
            }
        }
    }
}
