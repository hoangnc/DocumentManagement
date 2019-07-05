using System.Security.Claims;
using System.Security.Principal;
using IdConstants = IdentityServer3.Core.Constants;
namespace DT.Core.Web.Common.Identity.Extensions
{
    public static class IdentityExtension
    {
        public static string GetUserImage(this IIdentity identity)
        {
            Claim claim = ((ClaimsIdentity)identity).FindFirst(Constants.DtClaimTypes.UserImage);
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetUserEmail(this IIdentity identity)
        {
            Claim claim = ((ClaimsIdentity)identity).FindFirst(IdConstants.ClaimTypes.Email);
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}
