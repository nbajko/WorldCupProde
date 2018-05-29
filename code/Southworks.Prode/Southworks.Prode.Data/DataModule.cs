using System.Linq;
using Autofac;
using Southworks.Prode.Data.Repositories;

namespace Southworks.Prode.Data
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProdeDbContext>().InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(ProdeDbContext).Assembly)
                .Where(x => x.IsAssignableTo<IDataRepository>())
                .AsImplementedInterfaces();
        }
    }
}
