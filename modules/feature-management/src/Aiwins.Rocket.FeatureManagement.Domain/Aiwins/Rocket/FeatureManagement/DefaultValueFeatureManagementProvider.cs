using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Features;

namespace Aiwins.Rocket.FeatureManagement {
    public class DefaultValueFeatureManagementProvider : IFeatureManagementProvider, ISingletonDependency {
        public string Name => DefaultValueFeatureValueProvider.ProviderName;

        public virtual Task<string> GetOrNullAsync (FeatureDefinition feature, string providerKey) {
            return Task.FromResult (feature.DefaultValue);
        }

        public virtual Task SetAsync (FeatureDefinition feature, string value, string providerKey) {
            throw new RocketException ($"Can not set default value of a feature. It is only possible while defining the feature in a {typeof(IFeatureDefinitionProvider)} implementation.");
        }

        public virtual Task ClearAsync (FeatureDefinition feature, string providerKey) {
            throw new RocketException ($"Can not clear default value of a feature. It is only possible while defining the feature in a {typeof(IFeatureDefinitionProvider)} implementation.");
        }
    }
}