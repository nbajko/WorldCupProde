using System;
using System.Configuration;
using System.Web.Mvc;

namespace Southworks.Prode.Web
{
    public class GlobalAuthFilterConfig
    {
        public static void RegisterMvcGlobalAuthFilter(GlobalFilterCollection filters)
        {
            if (LockdownEnabled())
            {
                filters.Add(new AuthorizeAttribute());
            }
        }

        public static bool LockdownEnabled()
        {
            return ConfigurationManager.AppSettings["LockdownEnabled"].Equals("true", StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool AuthEnabled()
        {
            return ConfigurationManager.AppSettings["AuthEnabled"].Equals("true", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}