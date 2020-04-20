using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.Settings {
    public abstract class SettingValueProvider : ISettingValueProvider, ITransientDependency {
        public abstract string Name { get; }

        protected ISettingStore SettingStore { get; }

        protected SettingValueProvider (ISettingStore settingStore) {
            SettingStore = settingStore;
        }

        public abstract Task<string> GetOrNullAsync (SettingDefinition setting);
    }
}