using System;
using System.Linq;
using System.Threading.Tasks;
using Southworks.Prode.Data.Models;
using Southworks.Prode.Data.Repositories;

namespace Southworks.Prode.Services.Data
{
    public class MatchResultsService : IMatchResultsService, IDataService
    {
        private readonly IMatchResultsRepository matchResultsRepository;

        public MatchResultsService(IMatchResultsRepository matchResultsRepository)
        {
            this.matchResultsRepository = matchResultsRepository;
        }

        public MatchResultEntity GetResultByMatch(Guid matchId)
        {
            return this.matchResultsRepository.Get(x => x.MatchId.Equals(matchId)).FirstOrDefault();
        }

        public IQueryable<MatchResultEntity> GetResults()
        {
            return this.matchResultsRepository.Get();
        }

        public async Task<MatchResultEntity> SaveResultAsync(MatchResultEntity entity)
        {
            MatchResultEntity existingEntity = null;
            existingEntity = this.matchResultsRepository.Get(x => x.MatchId.Equals(entity.MatchId)).FirstOrDefault();

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

            await this.matchResultsRepository.SaveAsync(existingEntity);

            return existingEntity;
        }
    }
}
