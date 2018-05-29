using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Southworks.Prode.Data
{
    public class DbMigratorInitializer<TContext, TConfiguration> : IDatabaseInitializer<TContext>
        where TContext : DbContext
        where TConfiguration : DbMigrationsConfiguration<TContext>, new()
    {
        public void InitializeDatabase(TContext context)
        {
            var migrator = new DbMigrator(new TConfiguration());

            var pending = migrator.GetPendingMigrations();
            if (pending.Any())
            {
                migrator.Update();
            }
        }
    }
}
