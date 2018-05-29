using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Mvc;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Newtonsoft.Json;
using Owin;
using Serilog;
using Southworks.Prode.Data.Models;
using Southworks.Prode.Services.Data;
using Southworks.Prode.Web.Helpers;

namespace Southworks.Prode.Web
{
    public class AuthConfig
    {
        public static void ConfigureAuth(IAppBuilder app)
        {
            var clientId = ConfigurationManager.AppSettings["ActiveDirectoryClientID"];
            var tenant = ConfigurationManager.AppSettings["ActiveDirectoryTenant"];
            var audience = ConfigurationManager.AppSettings["ActiveDirectoryAudience"];
            var authority = ConfigurationManager.AppSettings["ActiveDirectoryAuthority"];

            var tenants = ConfigureTenants();

            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions { CookieHttpOnly = true, CookieSecure = CookieSecureOption.Always });

            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    ClientId = clientId,
                    Authority = authority,
                    TokenValidationParameters = new TokenValidationParameters
                    {
                        // instead of using the default validation we inject our own multitenant validation logic
                        ValidateIssuer = false
                    },
                    Notifications = new OpenIdConnectAuthenticationNotifications()
                    {
                        RedirectToIdentityProvider = (context) =>
                        {
                            if (IsAjaxRequest(context.Request))
                            {
                                context.HandleResponse();
                                return Task.FromResult(0);
                            }

                            // This ensures that the address used for sign in and sign out is picked up dynamically from the request
                            // this allows you to deploy your app without having to change settings
                            // Remember that the base URL of the address used here must be provisioned in Azure AD beforehand.
                            var appBaseUrl = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase;
                            context.ProtocolMessage.RedirectUri = appBaseUrl + "/";
                            context.ProtocolMessage.PostLogoutRedirectUri = appBaseUrl;
                            return Task.FromResult(0);
                        },
                        SecurityTokenValidated = (context) => // we use this notification for injecting our custom logic
                        {
                            // retriever caller data from the incoming principal
                            var tenantID = context.AuthenticationTicket.Identity.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value;

                            // tenant validation
                            var tenantModel = tenants.FirstOrDefault(t => string.Equals(t.TenantID, tenantID, StringComparison.OrdinalIgnoreCase));
                            if (tenantModel == null)
                            {
                                throw new SecurityTokenValidationException();
                            }
                            
                            SetClaims(context.AuthenticationTicket.Identity);

                            return Task.FromResult(0);
                        },
                        AuthenticationFailed = (context) =>
                        {
                            context.OwinContext.Response.Redirect("/auth/unauthorized/");
                            context.HandleResponse(); // Suppress the exception
                            return Task.FromResult(0);
                        }
                    }
                });
        }

        private static void SetClaims(ClaimsIdentity identity)
        {
            var usersService = DependencyResolver.Current.GetService(typeof(IUsersService)) as IUsersService;

            var emailAddress = identity.GetEmailAddress();

            var user = usersService.GetUser(emailAddress);

            if (user == null)
            {
                user = new UserEntity
                {
                    Email = emailAddress,
                    Name = identity.GetName(),
                    AccessLevel = UserAccessLevel.Player
                };

                Task.Run(async () => user = await usersService.SetUser(user)).Wait();
            }

            if (user.AccessLevel == UserAccessLevel.Admin)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, UserAccessLevel.Admin.ToString()));
            }

            identity.AddClaim(new Claim("user_id", user.Id.ToString()));
        }

        private static bool IsAjaxRequest(IOwinRequest request)
        {
            if (request.Query != null && request.Query["X-Requested-With"] == "XMLHttpRequest")
            {
                return true;
            }

            return request.Headers != null && request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }

        private static List<Tenant> ConfigureTenants()
        {
            var tenantsList = File.ReadAllText(HostingEnvironment.MapPath(@"~/App_Data/tenants.json"));
            return JsonConvert.DeserializeObject<List<Tenant>>(tenantsList);
        }

        private class Tenant
        {
            public string TenantID { get; set; }

            public string Organization { get; set; }

            public string Comments { get; set; }

            public bool RequiresInvitation { get; set; }
        }
    }
}