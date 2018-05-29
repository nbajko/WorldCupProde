using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Southworks.Prode.Data.Models;

namespace Southworks.Prode.Data.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class, IIdentifiable<Guid>
    {
        internal readonly DbContext Context;
        internal readonly DbSet<T> Set;

        public BaseRepository(DbContext context, DbSet<T> set)
        {
            this.Context = context;
            this.Set = set;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> filter = null)
        {
            return filter != null ? this.Set.Where(filter) : this.Set.AsQueryable<T>();
        }

        public T Get(Guid id)
        {
            return this.Set
                .SingleOrDefault(x => x.Id.Equals(id));
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await this.Set
                .SingleOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task SaveAsync(T entity)
        {
            var entry = this.Context.Entry<T>(entity);
            if (entry.State == EntityState.Detached)
            {
                this.Set.Add(entity);
            }

            if (this.Context.ChangeTracker.HasChanges())
            {
                await this.Context.SaveChangesAsync();
            }
        }
    }
}
