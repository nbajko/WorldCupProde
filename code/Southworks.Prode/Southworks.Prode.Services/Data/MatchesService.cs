using System;
using System.Linq;
using System.Threading.Tasks;
using Southworks.Prode.Data.Models;
using Southworks.Prode.Data.Repositories;

namespace Southworks.Prode.Services.Data
{
    public class MatchesService : IMatchesService, IDataService
    {
        private readonly IMatchesRepository matchesRepository;

        public MatchesService(IMatchesRepository matchesRepository)
        {
            this.matchesRepository = matchesRepository;
        }

        public IQueryable<MatchEntity> GetMatches()
        {
            return this.matchesRepository.Get();
        }

        public async Task<MatchEntity> SaveMatch(MatchEntity entity)
        {
            MatchEntity existingEntity = null;
            if (entity.Id != null)
            {
                existingEntity = await this.matchesRepository.GetAsync(entity.Id);
            }

            if (existingEntity != null)
            {
                existingEntity.AwayTeam = entity.AwayTeam;
                existingEntity.HomeTeam = entity.HomeTeam;
                existingEntity.PlayedOn = entity.PlayedOn;
                existingEntity.Stage = entity.Stage;
            }
            else
            {
                existingEntity = entity;
                existingEntity.Id = Guid.NewGuid();
            }

            await this.matchesRepository.SaveAsync(existingEntity);

            return existingEntity;
        }
    }
}
