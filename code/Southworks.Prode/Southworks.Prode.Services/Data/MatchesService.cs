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

        public bool ExistsMatch(Guid homeTeam, Guid awayTeam, MatchStage stage)
        {
            return this.matchesRepository.Get(x => ((homeTeam.Equals(x.HomeTeam) && awayTeam.Equals(x.AwayTeam))
                || (awayTeam.Equals(x.HomeTeam) && homeTeam.Equals(x.AwayTeam)))
                && x.Stage == stage).Any();
        }

        public IQueryable<MatchEntity> GetMatches()
        {
            return this.matchesRepository.Get();
        }

        public MatchEntity GetMatch(Guid id)
        {
            return this.matchesRepository.Get(id);
        }

        public async Task<MatchEntity> SaveMatch(MatchEntity entity)
        {
            MatchEntity existingEntity = null;
            if (entity.Id != null && !Guid.Empty.Equals(entity.Id))
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
