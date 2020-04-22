using Aiwins.Rocket.Caching;
using Aiwins.Rocket.Http.Client;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.AspNetCore.Mvc.Client {
    [DependsOn (
        typeof (RocketHttpClientModule),
        typeof (RocketAspNetCoreMvcContractsModule),
        typeof (RocketCachingModule),
        typeof (RocketLocalizationModule)
    )]
    public class RocketAspNetCoreMvcClientModule : RocketModule {
        public const string RemoteServiceName = "RocketMvcClient";

        public override void ConfigureServices (ServiceConfigurationContext context) {
            context.Services.AddHttpClientProxies (
                typeof (RocketAspNetCoreMvcContractsModule).Assembly,
                RemoteServiceName,
                asDefaultServices : false
            );

            Configure<RocketLocalizationOptions> (options => {
                options.GlobalContributors.Add<RemoteLocalizationContributor> ();
            });
        }
    }
}