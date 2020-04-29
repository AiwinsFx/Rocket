using System.Threading.Tasks;
using Aiwins.Rocket.Application.Services;
using JetBrains.Annotations;

namespace Aiwins.Rocket.PermissionManagement {
    public interface IPermissionAppService : IApplicationService {
        Task<GetPermissionListResultDto> GetAsync ([NotNull] string providerName, [NotNull] string providerKey);

        Task UpdateAsync ([NotNull] string providerName, [NotNull] string providerKey, UpdatePermissionsDto input);
    }
}