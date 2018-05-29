using System.Data.Entity;
using System.Diagnostics;
using Southworks.Prode.Data.Models;

namespace Southworks.Prode.Data
{
    public class ProdeDbContext : DbContext
    {

        public ProdeDbContext() : base("ProdeDbConnectionString")
        {
            if (Debugger.IsAttached)
            {
                this.Database.Log = (s) =>
                {
                    Debug.WriteLine(s);
                };
            }

            Database.SetInitializer<ProdeDbContext>(new DbMigratorInitializer<ProdeDbContext, Migrations.Configuration>());
        }

        public DbSet<UserEntity> UsersDbSet { get; set; }

        public DbSet<CountryEntity> CountriesDbSet { get; set; }

        public DbSet<MatchEntity> MatchesDbSet { get; set; }
    }
}
