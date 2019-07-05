using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace DT.Core.Authorization
{
    /// <summary>
    /// This class is used to permissions for users.
    /// </summary>
    public interface IPermissionChecker
    {
        /// <summary>
        /// Checks if current user is granted for a permission.
        /// </summary>
        /// <param name="permissionName">Name of the permission</param>
        Task<bool> IsGrantedAsync(string claimName, string permissionName);

        /// <summary>
        /// Checks if a user is granted for a permission.
        /// </summary>
        /// <param name="user">User to check</param>
        /// <param name="permissionName">Name of the permission</param>
        Task<bool> IsGrantedAsync(ClaimsPrincipal principal, string claimName, string permissionName);
    }
}
