using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;

namespace Southworks.Prode.Web.Controllers
{
    public class AuthController : Controller
    {
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Head)]
        [Route("auth/signout")]
        public void SignOut()
        {
            this.HttpContext.GetOwinContext().Authentication.SignOut(
                OpenIdConnectAuthenticationDefaults.AuthenticationType, CookieAuthenticationDefaults.AuthenticationType);
        }

        [AllowAnonymous]
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Head)]
        [Route("auth/unauthorized")]
        public ActionResult Unauthorized()
        {
            return this.View("~/Views/Auth/_unauthorized.cshtml");
        }

        [AllowAnonymous]
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Head)]
        [Route("auth/access-denied")]
        public ActionResult AccessDenied()
        {
            return this.View("~/Views/Auth/_accessDenied.cshtml");
        }
    }
}