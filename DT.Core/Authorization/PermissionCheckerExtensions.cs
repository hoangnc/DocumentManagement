using System.Security.Claims;

namespace DT.Core.Authorization
{
    /// <summary>
    /// Extension methods for <see cref="IPermissionChecker"/>
    /// </summary>
    public static class PermissionCheckerExtensions
    {
        /// <summary>
        /// Checks if current user is granted for a permission.
        /// </summary>
        /// <param name="permissionChecker">Permission checker</param>
        /// <param name="permissionName">Name of the permission</param>
        public static bool IsGranted(this IPermissionChecker permissionChecker, string claimName, string permissionName)
        {
            return permissionChecker.IsGrantedAsync(claimName, permissionName).Result;
        }

        /// <summary>
        /// Checks if a user is granted for a permission.
        /// </summary>
        /// <param name="permissionChecker">Permission checker</param>
        /// <param name="principal">User to check</param>
        /// <param name="permissionName">Name of the permission</param>
        public static bool IsGranted(this IPermissionChecker permissionChecker, ClaimsPrincipal principal, string claimName, string permissionName)
        {
            return permissionChecker.IsGrantedAsync(principal, claimName, permissionName).Result;
        }
    }
}
