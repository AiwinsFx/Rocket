using Aiwins.Rocket.Authorization.Permissions;
using Aiwins.Rocket.Guids;
using Aiwins.Rocket.MultiTenancy;

namespace Aiwins.Rocket.PermissionManagement.IdentityServer
{
    public class ClientPermissionManagementProvider : PermissionManagementProvider
    {
        public override string Name => ClientPermissionValueProvider.ProviderName;

        public ClientPermissionManagementProvider(
            IPermissionGrantRepository permissionGrantRepository,
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant)
            : base(
                permissionGrantRepository,
                guidGenerator,
                currentTenant)
        {

        }
    }
}
