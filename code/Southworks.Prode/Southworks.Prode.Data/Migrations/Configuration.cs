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
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000001"), Name = "Alemania", Code = "GER" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000002"), Name = "Arabia Saudí", Code = "KSA" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000003"), Name = "Argentina", Code = "ARG" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000004"), Name = "Australia", Code = "AUS" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000005"), Name = "Bélgica", Code = "BEL" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000006"), Name = "Brasil", Code = "BRA" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000007"), Name = "Colombia", Code = "COL" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000008"), Name = "Costa Rica", Code = "CRC" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000009"), Name = "Croacia", Code = "CRO" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000010"), Name = "Dinamarca", Code = "DEN" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000011"), Name = "Egipto", Code = "EGY" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000012"), Name = "España", Code = "ESP" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000013"), Name = "Francia", Code = "FRA" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000014"), Name = "Inglaterra", Code = "ENG" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000015"), Name = "Islandia", Code = "ISL" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000016"), Name = "Japón", Code = "JPN" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000017"), Name = "Marruecos", Code = "MAR" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000018"), Name = "México", Code = "MEX" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000019"), Name = "Nigeria", Code = "NGA" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000020"), Name = "Panamá", Code = "PAN" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000021"), Name = "Perú", Code = "PER" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000022"), Name = "Polonia", Code = "POL" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000023"), Name = "Portugal", Code = "POR" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000024"), Name = "República de Corea", Code = "KOR" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000025"), Name = "RI de Irán", Code = "IRN" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000026"), Name = "Rusia", Code = "RUS" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000027"), Name = "Senegal", Code = "SEN" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000028"), Name = "Serbia", Code = "SRB" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000029"), Name = "Suecia", Code = "SWE" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000030"), Name = "Suiza", Code = "SUI" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000031"), Name = "Túnez", Code = "TUN" },
                new CountryEntity { Id = Guid.Parse("00000000-0000-0000-0001-000000000032"), Name = "Uruguay", Code = "URU" }
                );

            context.UsersDbSet.AddOrUpdate(x => x.Email,
                new UserEntity { Id = Guid.NewGuid(), Name = "Nicolás Bajkó", Email = "nicolas.bajko@soutworks.com", AccessLevel = UserAccessLevel.Admin }
                );
        }
    }
}
