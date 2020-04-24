using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Features;
using Aiwins.Rocket.MultiTenancy;

namespace Aiwins.Rocket.FeatureManagement
{
    public class TenantFeatureManagementProvider : FeatureManagementProvider, ITransientDependency
    {
        public override string Name => TenantFeatureValueProvider.ProviderName;

        protected ICurrentTenant CurrentTenant { get; }

        public TenantFeatureManagementProvider(
            IFeatureManagementStore store,
            ICurrentTenant currentTenant)
            : base(store)
        {
            CurrentTenant = currentTenant;
        }

        protected override string NormalizeProviderKey(string providerKey)
        {
            if (providerKey != null)
            {
                return providerKey;
            }

            return CurrentTenant.Id?.ToString();
        }
    }
}