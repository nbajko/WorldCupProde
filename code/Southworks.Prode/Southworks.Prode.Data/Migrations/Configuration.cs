using System;
using System.Data.Entity.Migrations;
using Southworks.Prode.Data.Models;

namespace Southworks.Prode.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Southworks.Prode.Data.ProdeDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Southworks.Prode.Data.ProdeDbContext context)
        {
            context.CountriesDbSet.AddOrUpdate(x => x.Code,
                new CountryEntity { Id = Guid.NewGuid(), Name = "Alemania", Code = "GER" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "Arabia Saudí", Code = "KSA" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "Argentina", Code = "ARG" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "Australia", Code = "AUS" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "Bélgica", Code = "BEL" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "Brasil", Code = "BRA" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "Colombia", Code = "COL" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "Costa Rica", Code = "CRC" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "Croacia", Code = "CRO" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "Dinamarca", Code = "DEN" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "Egipto", Code = "EGY" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "España", Code = "ESP" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "Francia", Code = "FRA" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "Inglaterra", Code = "ENG" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "Islandia", Code = "ISL" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "Japón", Code = "JPN" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "Marruecos", Code = "MAR" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "México", Code = "MEX" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "Nigeria", Code = "NGA" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "Panamá", Code = "PAN" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "Perú", Code = "PER" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "Polonia", Code = "POL" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "Portugal", Code = "POR" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "República de Corea", Code = "KOR" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "RI de Irán", Code = "IRN" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "Rusia", Code = "RUS" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "Senegal", Code = "SEN" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "Serbia", Code = "SRB" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "Suecia", Code = "SWE" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "Suiza", Code = "SUI" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "Túnez", Code = "TUN" },
                new CountryEntity { Id = Guid.NewGuid(), Name = "Uruguay", Code = "URU" }
                );

            context.UsersDbSet.AddOrUpdate(x => x.Email,
                new UserEntity { Id = Guid.NewGuid(), Name = "Nicolás Bajkó", Email = "nicolas.bajko@soutworks.com", AccessLevel = UserAccessLevel.Admin }
                );
        }
    }
}
