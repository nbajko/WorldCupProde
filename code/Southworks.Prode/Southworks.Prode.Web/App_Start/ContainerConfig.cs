using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Southworks.Prode.Data;
using Southworks.Prode.Services.Data;

namespace Southworks.Prode.Web
{
    public class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(typeof(MvcApplication).Assembly);
            builder.RegisterAssemblyModules(typeof(ProdeDbContext).Assembly);
            builder.RegisterAssemblyModules(typeof(UsersService).Assembly);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            return container;
        }
    }
}