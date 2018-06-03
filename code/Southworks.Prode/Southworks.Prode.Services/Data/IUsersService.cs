using System.Linq;
using System.Threading.Tasks;
using Southworks.Prode.Data.Models;

namespace Southworks.Prode.Services.Data
{
    public interface IUsersService
    {
        UserEntity GetUser(string emailAddress);

        Task<UserEntity> SetUser(UserEntity entity);

        IQueryable<UserEntity> Get();
    }
}