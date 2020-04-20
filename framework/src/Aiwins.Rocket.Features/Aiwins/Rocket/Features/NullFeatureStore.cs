using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Aiwins.Rocket.Features {
    [Dependency (TryRegister = true)]
    public class NullFeatureStore : IFeatureStore, ISingletonDependency {
        public ILogger<NullFeatureStore> Logger { get; set; }

        public NullFeatureStore () {
            Logger = NullLogger<NullFeatureStore>.Instance;
        }

        public Task<string> GetOrNullAsync (string name, string providerName, string providerKey) {
            return Task.FromResult ((string) null);
        }
    }
}