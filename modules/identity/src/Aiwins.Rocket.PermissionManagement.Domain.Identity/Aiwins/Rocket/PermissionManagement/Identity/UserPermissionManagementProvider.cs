using Aiwins.Rocket.Authorization.Permissions;
using Aiwins.Rocket.Guids;
using Aiwins.Rocket.MultiTenancy;

namespace Aiwins.Rocket.PermissionManagement.Identity
{
    public class UserPermissionManagementProvider : PermissionManagementProvider
    {
        public override string Name => UserPermissionValueProvider.ProviderName;

        public UserPermissionManagementProvider(
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