using System.Security.Claims;
using System.Security.Principal;
using static DT.Core.Web.Common.Identity.Constants;
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

        public static string GetUserName(this IIdentity identity)
        {
            Claim claim = ((ClaimsIdentity)identity).FindFirst(IdConstants.ClaimTypes.Name);
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetDisplayName(this IIdentity identity)
        {
            Claim claim = ((ClaimsIdentity)identity).FindFirst(IdConstants.ClaimTypes.Subject);
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetDepartment(this IIdentity identity)
        {
            Claim claim = ((ClaimsIdentity)identity).FindFirst(DtClaimTypes.Department);
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}
