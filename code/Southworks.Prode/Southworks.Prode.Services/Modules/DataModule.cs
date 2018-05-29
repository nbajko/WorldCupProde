using Autofac;
using Southworks.Prode.Services.Data;

namespace Southworks.Prode.Services.Modules
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(UsersService).Assembly)
                .Where(x => x.IsAssignableTo<IDataService>())
                .AsImplementedInterfaces();
        }
    }
}
