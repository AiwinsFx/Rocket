using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.FeatureManagement.Tenant {
    [DependsOn (
        typeof (RocketFeatureManagementDomainModule)
    )]
    public class RocketFeatureManagementDomainTenantModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            Configure<FeatureManagementOptions> (options => {
                options.Providers.Add<TenantFeatureManagementProvider> ();
                options.ProviderPolicies[TenantFeatureValueProvider.ProviderName] = "RocketTenantManagement.Tenants.ManageFeatures";
            });

        }
    }
}