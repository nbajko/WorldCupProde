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

        public IQueryable<MatchResultEntity> GetResults()
        {
            return this.matchResultsRepository.Get();
        }

        public async Task<MatchResultEntity> SaveResultAsync(MatchResultEntity entity)
        {
            MatchResultEntity existingEntity = null;
            if (entity.Id != null || Guid.Empty.Equals(entity.Id))
            {
                existingEntity = await this.matchResultsRepository.GetAsync(entity.Id);
            }

            if (existingEntity != null)
            {
                existingEntity.AwayGoals = entity.AwayGoals;
                existingEntity.HomeGoals = entity.HomeGoals;
                existingEntity.Result = entity.Result;
            }
            else
            {
                existingEntity = entity;
            }

            await this.matchResultsRepository.SaveAsync(existingEntity);

            return existingEntity;
        }
    }
}
