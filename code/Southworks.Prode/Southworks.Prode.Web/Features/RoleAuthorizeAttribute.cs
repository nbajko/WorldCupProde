using System;
using System.Linq;
using System.Web.Mvc;
using Southworks.Prode.Web.Helpers;

namespace Southworks.Prode.Web.Features
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        private static bool? lockdownEnabled;

        public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                this.AuthorizeUser(filterContext);
            }
            else
            {
                if (!lockdownEnabled.HasValue)
                {
                    lockdownEnabled = GlobalAuthFilterConfig.LockdownEnabled();
                }

                if (lockdownEnabled.Value)
                {
                    base.OnAuthorization(filterContext);
                }
            }
        }

        protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("~/auth/access-denied");
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }

        private void AuthorizeUser(AuthorizationContext context)
        {
            var principal = context.HttpContext.User;

            var roles = this.Roles.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(role => role.Trim());

            var authorized = roles.Any(role => principal.UserIsInRole(role));

            if (!authorized)
            {
                var redirectResult = new RedirectResult("~/auth/access-denied");

                if (context.HttpContext.Request.IsAjaxRequest())
                {
                    context.Result = new System.Web.Mvc.JavaScriptResult
                    {
                        Script = "var RedirectObject= {location:'" + UrlHelper.GenerateContentUrl(redirectResult.Url, context.HttpContext) + "'};"
                    };
                }
                else
                {
                    context.Result = redirectResult;
                }
            }
        }
    }
}