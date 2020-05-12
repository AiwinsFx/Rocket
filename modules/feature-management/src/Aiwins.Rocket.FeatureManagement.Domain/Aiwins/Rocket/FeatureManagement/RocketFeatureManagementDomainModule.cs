using Aiwins.Rocket.Caching;
using Aiwins.Rocket.FeatureManagement.Localization;
using Aiwins.Rocket.Features;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Localization.ExceptionHandling;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.Rocket.FeatureManagement {
    [DependsOn (
        typeof (RocketFeatureManagementDomainSharedModule),
        typeof (RocketFeaturesModule),
        typeof (RocketCachingModule)
    )]
    public class RocketFeatureManagementDomainModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            Configure<FeatureManagementOptions> (options => {
                options.Providers.Add<DefaultValueFeatureManagementProvider> ();
                options.Providers.Add<EditionFeatureManagementProvider> ();
            });

            Configure<RocketExceptionLocalizationOptions> (options => {
                options.MapCodeNamespace ("RocketFeatureManagement", typeof (RocketFeatureManagementResource));
            });
        }
    }
}