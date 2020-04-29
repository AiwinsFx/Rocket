using System.Threading.Tasks;
using Aiwins.Rocket.Security.Claims;

namespace Aiwins.Rocket.Authorization.Permissions {
    public class UserPermissionValueProvider : PermissionValueProvider {
        public const string ProviderName = "U";

        public override string Name => ProviderName;

        public UserPermissionValueProvider (IPermissionStore permissionStore) : base (permissionStore) {

        }

        public override async Task<PermissionGrantResult> GetResultAsync (PermissionValueCheckContext context) {
            var userId = context.Principal?.FindFirst (RocketClaimTypes.UserId)?.Value;

            if (userId == null) {
                return PermissionGrantResult.Undefined;
            }

            return await PermissionStore.GetResultAsync (context.Permission.Name, Name, userId);
        }
    }
}