using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Aiwins.Rocket.Settings {
    public class ConfigurationSettingValueProvider : ISettingValueProvider, ITransientDependency {
        public const string ConfigurationNamePrefix = "Settings:";

        public const string ProviderName = "C";

        public string Name => ProviderName;

        protected IConfiguration Configuration { get; }

        public ConfigurationSettingValueProvider (IConfiguration configuration) {
            Configuration = configuration;
        }

        public virtual Task<string> GetOrNullAsync (SettingDefinition setting) {
            return Task.FromResult (Configuration[ConfigurationNamePrefix + setting.Name]);
        }
    }
}