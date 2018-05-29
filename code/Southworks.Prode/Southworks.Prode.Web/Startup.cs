using Microsoft.Owin;
using Owin;
using Southworks.Prode.Web;

[assembly: OwinStartup(typeof(Startup))]

namespace Southworks.Prode.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            if (GlobalAuthFilterConfig.AuthEnabled())
            {
                AuthConfig.ConfigureAuth(app);
            }
        }
    }
}