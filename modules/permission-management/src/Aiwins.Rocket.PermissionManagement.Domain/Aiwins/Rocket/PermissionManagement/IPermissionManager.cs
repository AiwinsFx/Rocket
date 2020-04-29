using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Aiwins.Rocket.PermissionManagement {
    public interface IPermissionManager {
        Task<PermissionWithGrantedProviders> GetAsync (string permissionName, string providerName, string providerKey);

        Task<List<PermissionWithGrantedProviders>> GetAllAsync ([NotNull] string providerName, [NotNull] string providerKey);

        Task SetAsync (string permissionName, string providerName,string providerScope, string providerKey);

        Task<PermissionGrant> UpdateProviderKeyAsync (PermissionGrant permissionGrant, string providerKey);
    }
}