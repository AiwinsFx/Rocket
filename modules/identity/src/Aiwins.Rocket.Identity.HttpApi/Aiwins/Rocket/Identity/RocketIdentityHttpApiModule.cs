using Aiwins.Rocket.AspNetCore.Mvc;
using Aiwins.Rocket.Identity.Localization;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Localization.Resources.RocketUi;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.Identity {
    [DependsOn (
        typeof (RocketIdentityApplicationContractsModule),
        typeof (RocketAspNetCoreMvcModule)
    )]
    public class RocketIdentityHttpApiModule : RocketModule {
        public override void PreConfigureServices (ServiceConfigurationContext context) {
            PreConfigure<IMvcBuilder> (mvcBuilder => {
                mvcBuilder.AddApplicationPartIfNotExists (typeof (RocketIdentityHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices (ServiceConfigurationContext context) {
            Configure<RocketLocalizationOptions> (options => {
                options.Resources
                    .Get<IdentityResource> ()
                    .AddBaseTypes (
                        typeof (RocketUiResource)
                    );
            });
        }
    }
}