using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Aiwins.Rocket.Authorization.Permissions {
    public class NullPermissionStore : IPermissionStore, ISingletonDependency {
        public ILogger<NullPermissionStore> Logger { get; set; }

        public NullPermissionStore () {
            Logger = NullLogger<NullPermissionStore>.Instance;
        }

        public Task<bool> IsGrantedAsync (string name, string providerName, string providerKey) {
            return TaskCache.FalseResult;
        }
    }
}