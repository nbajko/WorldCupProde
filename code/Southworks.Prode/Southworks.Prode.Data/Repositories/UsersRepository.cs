using Southworks.Prode.Data.Models;

namespace Southworks.Prode.Data.Repositories
{
    public class UsersRepository : BaseRepository<UserEntity>, IUsersRepository, IDataRepository
    {
        public UsersRepository(ProdeDbContext context)
            : base(context, context.UsersDbSet)
        {
        }
    }
}
