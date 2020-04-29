using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Guids;

namespace Aiwins.Rocket.PermissionManagement {
    public class PermissionDataSeeder : IPermissionDataSeeder, ITransientDependency {
        protected IPermissionGrantRepository PermissionGrantRepository { get; }
        protected IGuidGenerator GuidGenerator { get; }

        public PermissionDataSeeder (
            IPermissionGrantRepository permissionGrantRepository,
            IGuidGenerator guidGenerator) {
            PermissionGrantRepository = permissionGrantRepository;
            GuidGenerator = guidGenerator;
        }

        public virtual async Task SeedAsync (
            string providerName,
            string providerKey,
            Dictionary<string, string> permissions,
            Guid? tenantId = null) {
            foreach (var permission in permissions) {
                var permissionGrant = await PermissionGrantRepository.FindAsync (permission.Key, providerName, providerKey);
                if (permissionGrant != null) {
                    if (permissionGrant.ProviderScope == permission.Value)
                        continue;
                    permissionGrant.ProviderScope = permission.Value;
                    await PermissionGrantRepository.UpdateAsync (permissionGrant);
                }

                await PermissionGrantRepository.InsertAsync (
                    new PermissionGrant (
                        GuidGenerator.Create (),
                        permission.Key,
                        providerName,
                        permission.Value,
                        providerKey,
                        tenantId
                    )
                );
            }
        }
    }
}