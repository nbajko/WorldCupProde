using System;
using System.Linq;
using System.Threading.Tasks;
using Southworks.Prode.Data.Models;
using Southworks.Prode.Data.Repositories;

namespace Southworks.Prode.Services.Data
{
    public class UsersService : IUsersService, IDataService
    {
        private readonly IUsersRepository usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public IQueryable<UserEntity> Get()
        {
            return this.usersRepository.Get();
        }

        public UserEntity GetUser(string emailAddress)
        {
            return this.usersRepository
                .Get(x => x.Email.Equals(emailAddress, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();
        }

        public async Task<UserEntity> SetUser(UserEntity entity)
        {
            UserEntity existingEntity = null;
            if (entity.Id != null && !Guid.Empty.Equals(entity.Id))
            {
                existingEntity = await this.usersRepository.GetAsync(entity.Id);
            }

            if (existingEntity == null && string.IsNullOrWhiteSpace(entity.Email))
            {
                existingEntity = this.GetUser(entity.Email);
            }

            if (existingEntity != null)
            {
                existingEntity.AccessLevel = entity.AccessLevel;
                existingEntity.Name = entity.Name;
            }
            else
            {
                existingEntity = entity;
                existingEntity.Id = Guid.NewGuid();
            }

            await this.usersRepository.SaveAsync(existingEntity);

            return existingEntity;
        }
    }
}
