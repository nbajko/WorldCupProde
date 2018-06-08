using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Southworks.Prode.Web.Helpers
{
    public static class IdentityHelper
    {
        private static bool? lockdownEnabled;

        public static string GetEmailAddress(this IIdentity identity)
        {
            var username = identity.ClaimValue(ClaimTypes.Email);

            if (string.IsNullOrWhiteSpace(username))
            {
                username = identity.ClaimValue("preferred_username");
            }

            // temporarily we fallback to Upn claim as both v1 and v2 endpoints are allowed (for PRs and branches sites)
            if (string.IsNullOrWhiteSpace(username))
            {
                username = identity.ClaimValue(ClaimTypes.Upn);
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                username = identity.Name;
            }

            return username;
        }

        public static string GetName(this IIdentity identity)
        {
            if (!string.IsNullOrWhiteSpace(identity.ClaimValue("name")))
            {
                return identity.ClaimValue("name");
            }

            if (!string.IsNullOrWhiteSpace(identity.Name))
            {
                return identity.Name;
            }

            var nameClaim = identity.ClaimValue(ClaimTypes.Name);
            if (!string.IsNullOrWhiteSpace(nameClaim))
            {
                return nameClaim;
            }

            return identity.GetEmailAddress();
        }

        public static string GetUserDisplayName(this IIdentity identity)
        {
            return $"{identity.GetName()} ({identity.GetEmailAddress()})";
        }

        public static Guid GetUserId(this IIdentity identity)
        {
            var userIdValue = identity.ClaimValue("user_id");

            return !string.IsNullOrWhiteSpace(userIdValue) ? Guid.Parse(userIdValue) : Guid.Empty;
        }

        public static string ClaimValue(this IIdentity userIdentity, string claimType)
        {
            var value = string.Empty;
            var claimsIdentity = userIdentity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == claimType);
                if (claim != null)
                {
                    value = claim.Value;
                }
            }

            return value;
        }

        public static bool UserIsInRole(this IPrincipal principal, string role)
        {
            if (!lockdownEnabled.HasValue)
            {
                lockdownEnabled = GlobalAuthFilterConfig.LockdownEnabled();
            }

            if (!lockdownEnabled.Value)
            {
                return true;
            }

            return principal.IsInRole(role);
        }
    }
}