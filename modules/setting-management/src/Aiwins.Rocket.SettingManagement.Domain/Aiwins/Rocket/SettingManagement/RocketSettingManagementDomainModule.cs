using Aiwins.Rocket.Caching;
using Aiwins.Rocket.Domain;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Settings;

namespace Aiwins.Rocket.SettingManagement {
    [DependsOn (
        typeof (RocketSettingsModule),
        typeof (RocketDddDomainModule),
        typeof (RocketSettingManagementDomainSharedModule),
        typeof (RocketCachingModule)
    )]
    public class RocketSettingManagementDomainModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            Configure<SettingManagementOptions> (options => {
                options.Providers.Add<DefaultValueSettingManagementProvider> ();
                options.Providers.Add<ConfigurationSettingManagementProvider> ();
                options.Providers.Add<GlobalSettingManagementProvider> ();
                options.Providers.Add<TenantSettingManagementProvider> ();
                options.Providers.Add<UserSettingManagementProvider> ();
            });
        }
    }
}