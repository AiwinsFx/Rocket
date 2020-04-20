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
        public Task<bool> IsGrantedAsync (string name) {
            return TaskCache.TrueResult;
        }

        public Task<bool> IsGrantedAsync (ClaimsPrincipal claimsPrincipal, string name) {
            return TaskCache.TrueResult;
        }
    }
}