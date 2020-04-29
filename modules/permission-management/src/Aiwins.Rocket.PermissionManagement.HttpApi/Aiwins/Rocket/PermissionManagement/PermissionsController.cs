using System.Threading.Tasks;
using Aiwins.Rocket.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Aiwins.Rocket.PermissionManagement {
    [RemoteService (Name = PermissionManagementRemoteServiceConsts.RemoteServiceName)]
    [Area ("rocket")]
    public class PermissionsController : RocketController, IPermissionAppService {
        protected IPermissionAppService PermissionAppService { get; }

        public PermissionsController (IPermissionAppService permissionAppService) {
            PermissionAppService = permissionAppService;
        }

        public virtual Task<GetPermissionListResultDto> GetAsync (string providerName, string providerKey) {
            return PermissionAppService.GetAsync (providerName, providerKey);
        }

        public virtual Task UpdateAsync (string providerName, string providerKey, UpdatePermissionsDto input) {
            return PermissionAppService.UpdateAsync (providerName, providerKey, input);
        }
    }
}