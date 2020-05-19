using System.Linq;
using System.Threading.Tasks;
using Aiwins.Rocket.Security.Claims;

namespace Aiwins.Rocket.Authorization.Permissions {
    public class RolePermissionValueProvider : PermissionValueProvider {
        public const string ProviderName = "R";

        public override string Name => ProviderName;

        public RolePermissionValueProvider (IPermissionStore permissionStore) : base (permissionStore) {

        }

        public override async Task<PermissionGrantResult> GetResultAsync (PermissionValueCheckContext context) {
            var roleIds = context.Principal?.FindAll (RocketClaimTypes.RoleId).Select (c => c.Value).ToArray ();

            if (roleIds == null || !roleIds.Any ()) {
                return PermissionGrantResult.Undefined;
            }

            var permissionGrantResult = PermissionGrantResult.Undefined;

            foreach (var roleId in roleIds) {
                var result = await PermissionStore.GetResultAsync (context.Permission.Name, Name, roleId);
                if (result == null) continue;

                if (result.ScopeType > permissionGrantResult.ScopeType) {
                    permissionGrantResult = result;
                }
            }

            return permissionGrantResult;
        }
    }
}