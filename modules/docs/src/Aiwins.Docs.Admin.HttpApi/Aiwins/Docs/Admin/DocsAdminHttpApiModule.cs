using Localization.Resources.RocketUi;
using Aiwins.Rocket.AspNetCore.Mvc;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Docs.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Docs.Admin
{
    [DependsOn(
        typeof(DocsAdminApplicationContractsModule),
        typeof(RocketAspNetCoreMvcModule)
        )]
    public class DocsAdminHttpApiModule : RocketModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(DocsAdminHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<DocsResource>()
                    .AddBaseTypes(typeof(RocketUiResource));
            });
        }
    }
}
