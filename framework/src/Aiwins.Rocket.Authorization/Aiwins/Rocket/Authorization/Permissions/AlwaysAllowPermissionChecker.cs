using System.Security.Claims;
using System.Threading.Tasks;
using Aiwins.Rocket.Threading;

namespace Aiwins.Rocket.Authorization.Permissions {
    /// <summary>
    /// 允许任何权限通过
    /// 
    /// 可以通过调用方法 IServiceCollection.AddAlwaysAllowAuthorization()替换IPermissionChecker
    /// 对于测试非常实用
    /// </summary>
    public class AlwaysAllowPermissionChecker : IPermissionChecker {
        public Task<PermissionGrantResult> GetResultAsync (string name) {
            return Task.FromResult(PermissionGrantResult.Granted);
        }

        public Task<PermissionGrantResult> GetResultAsync (ClaimsPrincipal claimsPrincipal, string name) {
            return Task.FromResult(PermissionGrantResult.Granted);
        }
    }
}