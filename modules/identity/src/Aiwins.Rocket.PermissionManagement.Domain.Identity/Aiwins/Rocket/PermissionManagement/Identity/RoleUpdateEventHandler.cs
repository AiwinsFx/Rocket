using System.Threading.Tasks;
using Aiwins.Rocket.Authorization.Permissions;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.EventBus.Local;
using Aiwins.Rocket.Identity;

namespace Aiwins.Rocket.PermissionManagement.Identity {
    public class RoleUpdateEventHandler:
        ILocalEventHandler<IdentityRoleNameChangedEvent>,
        ITransientDependency {
            protected IIdentityRoleRepository RoleRepository { get; }
            protected IPermissionManager PermissionManager { get; }
            protected IPermissionGrantRepository PermissionGrantRepository { get; }

            public RoleUpdateEventHandler (
                IIdentityRoleRepository roleRepository,
                IPermissionManager permissionManager,
                IPermissionGrantRepository permissionGrantRepository) {
                RoleRepository = roleRepository;
                PermissionManager = permissionManager;
                PermissionGrantRepository = permissionGrantRepository;
            }

            public virtual async Task HandleEventAsync (IdentityRoleNameChangedEvent eventData) {
                var role = await RoleRepository.FindAsync (eventData.IdentityRole.Id, false);
                if (role == null) {
                    return;
                }

                var permissionGrantsInRole = await PermissionGrantRepository.GetListAsync (RolePermissionValueProvider.ProviderName, eventData.OldName);
                foreach (var permissionGrant in permissionGrantsInRole) {
                    await PermissionManager.UpdateProviderKeyAsync (permissionGrant, eventData.IdentityRole.Name);
                }
            }
        }
}