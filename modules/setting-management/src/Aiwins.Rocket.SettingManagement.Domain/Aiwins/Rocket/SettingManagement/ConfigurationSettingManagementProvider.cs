using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Settings;
using Microsoft.Extensions.Configuration;

namespace Aiwins.Rocket.SettingManagement {
    public class ConfigurationSettingManagementProvider : ISettingManagementProvider, ITransientDependency {
        public string Name => ConfigurationSettingValueProvider.ProviderName;

        protected IConfiguration Configuration { get; }

        public ConfigurationSettingManagementProvider (IConfiguration configuration) {
            Configuration = configuration;
        }

        public virtual Task<string> GetOrNullAsync (SettingDefinition setting, string providerKey) {
            return Task.FromResult (Configuration[ConfigurationSettingValueProvider.ConfigurationNamePrefix + setting.Name]);
        }

        public virtual Task SetAsync (SettingDefinition setting, string value, string providerKey) {
            throw new RocketException ($"Can not set a setting value to the application configuration.");
        }

        public virtual Task ClearAsync (SettingDefinition setting, string providerKey) {
            throw new RocketException ($"Can not set a setting value to the application configuration.");
        }
    }
}