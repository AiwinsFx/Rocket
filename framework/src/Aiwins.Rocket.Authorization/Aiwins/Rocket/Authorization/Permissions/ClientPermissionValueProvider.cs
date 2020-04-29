using System.Threading.Tasks;
using Aiwins.Rocket.Security.Claims;

namespace Aiwins.Rocket.Authorization.Permissions {
    public class ClientPermissionValueProvider : PermissionValueProvider {
        public const string ProviderName = "C";

        public override string Name => ProviderName;

        public ClientPermissionValueProvider (IPermissionStore permissionStore) : base (permissionStore) {

        }

        public override async Task<PermissionGrantResult> GetResultAsync (PermissionValueCheckContext context) {
            var clientId = context.Principal?.FindFirst (RocketClaimTypes.ClientId)?.Value;

            if (clientId == null) {
                return PermissionGrantResult.Undefined;
            }

            return await PermissionStore.GetResultAsync (context.Permission.Name, Name, clientId);
        }
    }
}