using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace DT.Core.Authorization
{
    public class PermissionChecker : IPermissionChecker
    {
        public Task<bool> IsGrantedAsync(string claimName, string permissionName)
        {
            ClaimsPrincipal principal = (ClaimsPrincipal)HttpContext.Current.User;
            return Task.FromResult(principal.HasClaim(claimName, permissionName));
        }

        public Task<bool> IsGrantedAsync(ClaimsPrincipal principal, string claimName, string permissionName)
        {
            return Task.FromResult(principal.HasClaim(claimName, permissionName));
        }
    }
}
