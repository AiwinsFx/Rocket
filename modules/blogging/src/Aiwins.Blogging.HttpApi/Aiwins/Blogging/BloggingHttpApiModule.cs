using Localization.Resources.RocketUi;
using Aiwins.Rocket.AspNetCore.Mvc;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Blogging.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Blogging
{
    [DependsOn(
        typeof(BloggingApplicationContractsModule),
        typeof(RocketAspNetCoreMvcModule))]
    public class BloggingHttpApiModule : RocketModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(BloggingHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<BloggingResource>()
                    .AddBaseTypes(typeof(RocketUiResource));
            });
        }
    }
}
