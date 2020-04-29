using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aiwins.Rocket.PermissionManagement {
    public interface IPermissionDataSeeder {
        Task SeedAsync (
            string providerName,
            string providerKey,
            Dictionary<string, string> permissions,
            Guid? tenantId = null
        );
    }
}