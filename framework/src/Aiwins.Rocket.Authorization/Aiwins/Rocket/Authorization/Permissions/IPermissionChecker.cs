using System.Security.Claims;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Authorization.Permissions {
    public interface IPermissionChecker {
        Task<PermissionGrantResult> GetResultAsync ([NotNull] string name);

        Task<PermissionGrantResult> GetResultAsync ([CanBeNull] ClaimsPrincipal claimsPrincipal, [NotNull] string name);
    }
}