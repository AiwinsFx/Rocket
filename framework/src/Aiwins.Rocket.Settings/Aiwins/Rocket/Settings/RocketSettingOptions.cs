using Aiwins.Rocket.Collections;

namespace Aiwins.Rocket.Settings {
    public class RocketSettingOptions {
        public ITypeList<ISettingDefinitionProvider> DefinitionProviders { get; }

        public ITypeList<ISettingValueProvider> ValueProviders { get; }

        public RocketSettingOptions () {
            DefinitionProviders = new TypeList<ISettingDefinitionProvider> ();
            ValueProviders = new TypeList<ISettingValueProvider> ();
        }
    }
}