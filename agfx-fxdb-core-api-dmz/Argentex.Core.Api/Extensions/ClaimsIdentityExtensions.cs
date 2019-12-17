using System.Security.Claims;
using Argentex.Core.Service.Enums;

namespace Argentex.Core.Api.Extensions
{
    public static class ClaimsIdentityExtensions
    {
        public static RequestOrigin GetRequestOrigin(this ClaimsIdentity claimsIdentity)
        {
            const string grantType = "grantType";

            if (claimsIdentity.HasClaim(claim => claim.Type == grantType && claim.Value == "client_credentials"))
            {
                return RequestOrigin.ArgentexTrader;
            }

            if (claimsIdentity.HasClaim(claim => claim.Type == grantType && claim.Value == "password"))
            {
                return RequestOrigin.ClientSite;
            }

            return RequestOrigin.Unknown;
        }
    }
}
