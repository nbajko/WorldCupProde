using System;
using System.Linq;
using System.Threading.Tasks;
using Southworks.Prode.Data.Models;

namespace Southworks.Prode.Services.Data
{
    public interface IUsersService
    {
        UserEntity GetUser(string emailAddress);

        UserEntity GetUser(Guid userId);

        Task<UserEntity> SetUser(UserEntity entity);

        IQueryable<UserEntity> Get();
    }
}