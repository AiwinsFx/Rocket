using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Features;

namespace Aiwins.Rocket.FeatureManagement {
    public class FeatureStore : IFeatureStore, ITransientDependency {
        protected IFeatureManagementStore FeatureManagementStore { get; }

        public FeatureStore (IFeatureManagementStore featureManagementStore) {
            FeatureManagementStore = featureManagementStore;
        }

        public virtual Task<string> GetOrNullAsync (
            string name,
            string providerName,
            string providerKey) {
            return FeatureManagementStore.GetOrNullAsync (name, providerName, providerKey);
        }
    }
}