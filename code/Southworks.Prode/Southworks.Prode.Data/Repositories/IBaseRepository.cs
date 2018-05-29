using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Southworks.Prode.Data.Models;

namespace Southworks.Prode.Data.Repositories
{
    public interface IBaseRepository<T> where T : class, IIdentifiable<Guid>
    {
        IQueryable<T> Get(Expression<Func<T, bool>> filter = null);

        T Get(Guid id);

        Task<T> GetAsync(Guid id);

        Task SaveAsync(T entity);
    }
}
