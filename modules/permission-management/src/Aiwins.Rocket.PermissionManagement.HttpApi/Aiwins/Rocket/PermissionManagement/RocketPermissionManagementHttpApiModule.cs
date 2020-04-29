using Aiwins.Rocket.AspNetCore.Mvc;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.PermissionManagement.Localization;
using Localization.Resources.RocketUi;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.PermissionManagement.HttpApi {
    [DependsOn (
        typeof (RocketPermissionManagementApplicationContractsModule),
        typeof (RocketAspNetCoreMvcModule)
    )]
    public class RocketPermissionManagementHttpApiModule : RocketModule {
        public override void PreConfigureServices (ServiceConfigurationContext context) {
            PreConfigure<IMvcBuilder> (mvcBuilder => {
                mvcBuilder.AddApplicationPartIfNotExists (typeof (RocketPermissionManagementHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices (ServiceConfigurationContext context) {
            Configure<RocketLocalizationOptions> (options => {
                options.Resources
                    .Get<RocketPermissionManagementResource> ()
                    .AddBaseTypes (
                        typeof (RocketUiResource)
                    );
            });
        }
    }
}