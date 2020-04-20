using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Aiwins.Rocket.Settings {
    [Dependency (TryRegister = true)]
    public class NullSettingStore : ISettingStore, ISingletonDependency {
        public ILogger<NullSettingStore> Logger { get; set; }

        public NullSettingStore () {
            Logger = NullLogger<NullSettingStore>.Instance;
        }

        public Task<string> GetOrNullAsync (string name, string providerName, string providerKey) {
            return Task.FromResult ((string) null);
        }
    }
}