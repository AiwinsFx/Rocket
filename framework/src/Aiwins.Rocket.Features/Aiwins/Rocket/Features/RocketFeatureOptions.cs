using Aiwins.Rocket.Collections;

namespace Aiwins.Rocket.Features {
    public class RocketFeatureOptions {
        public ITypeList<IFeatureDefinitionProvider> DefinitionProviders { get; }

        public ITypeList<IFeatureValueProvider> ValueProviders { get; }

        public RocketFeatureOptions () {
            DefinitionProviders = new TypeList<IFeatureDefinitionProvider> ();
            ValueProviders = new TypeList<IFeatureValueProvider> ();
        }
    }
}