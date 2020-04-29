using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Aiwins.Rocket.Authorization.Permissions {
    public class NullPermissionStore : IPermissionStore, ISingletonDependency {
        public ILogger<NullPermissionStore> Logger { get; set; }

        public NullPermissionStore () {
            Logger = NullLogger<NullPermissionStore>.Instance;
        }

        public Task<PermissionGrantResult> GetResultAsync (string name, string providerName, string providerKey) {
            return Task.FromResult(PermissionGrantResult.Prohibited);
        }
    }
}