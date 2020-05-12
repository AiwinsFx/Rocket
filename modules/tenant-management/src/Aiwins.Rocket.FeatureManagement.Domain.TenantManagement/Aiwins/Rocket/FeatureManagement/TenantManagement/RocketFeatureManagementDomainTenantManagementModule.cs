using Aiwins.Rocket.Features;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.FeatureManagement.TenantManagement {
    [DependsOn (
        typeof (RocketFeatureManagementDomainModule)
    )]
    public class RocketFeatureManagementDomainTenantManagementModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            Configure<FeatureManagementOptions> (options => {
                options.Providers.Add<TenantFeatureManagementProvider> ();
                options.ProviderPolicies[TenantFeatureValueProvider.ProviderName] = "RocketTenantManagement.Tenants.ManageFeatures";
            });

        }
    }
}