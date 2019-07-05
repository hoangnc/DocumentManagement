using DT.Core.Authorization;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Abp.Authorization
{
    /// <summary>
    /// Null (and default) implementation of <see cref="IPermissionChecker"/>.
    /// </summary>
    public sealed class NullPermissionChecker : IPermissionChecker
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullPermissionChecker Instance { get; } = new NullPermissionChecker();

        public Task<bool> IsGrantedAsync(string claimName, string permissionName)
        {
            return Task.FromResult(true);
        }

        public Task<bool> IsGrantedAsync(ClaimsPrincipal principal, string claimName, string permissionName)
        {
            return Task.FromResult(true);
        }
    }
}