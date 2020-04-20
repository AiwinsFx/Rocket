using System.Linq;
using System.Threading.Tasks;
using Aiwins.Rocket.Security.Claims;

namespace Aiwins.Rocket.Authorization.Permissions {
    public class RolePermissionValueProvider : PermissionValueProvider {
        public const string ProviderName = "R";

        public override string Name => ProviderName;

        public RolePermissionValueProvider (IPermissionStore permissionStore) : base (permissionStore) {

        }

        public override async Task<PermissionGrantResult> CheckAsync (PermissionValueCheckContext context) {
            var roles = context.Principal?.FindAll (RocketClaimTypes.Role).Select (c => c.Value).ToArray ();

            if (roles == null || !roles.Any ()) {
                return PermissionGrantResult.Undefined;
            }

            foreach (var role in roles) {
                if (await PermissionStore.IsGrantedAsync (context.Permission.Name, Name, role)) {
                    return PermissionGrantResult.Granted;
                }
            }

            return PermissionGrantResult.Undefined;
        }
    }
}