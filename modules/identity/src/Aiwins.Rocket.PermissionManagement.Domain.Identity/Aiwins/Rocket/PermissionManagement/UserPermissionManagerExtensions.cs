using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aiwins.Rocket.Authorization.Permissions;
using JetBrains.Annotations;

namespace Aiwins.Rocket.PermissionManagement {
    public static class UserPermissionManagerExtensions {
        public static Task<List<PermissionWithGrantedProviders>> GetAllForUserAsync ([NotNull] this IPermissionManager permissionManager, Guid userId) {
            Check.NotNull (permissionManager, nameof (permissionManager));

            return permissionManager.GetAllAsync (UserPermissionValueProvider.ProviderName, userId.ToString ());
        }

        public static Task SetForUserAsync ([NotNull] this IPermissionManager permissionManager, Guid userId, [NotNull] string name, string scope) {
            Check.NotNull (permissionManager, nameof (permissionManager));

            return permissionManager.SetAsync (name, UserPermissionValueProvider.ProviderName, userId.ToString (), scope);
        }
    }
}