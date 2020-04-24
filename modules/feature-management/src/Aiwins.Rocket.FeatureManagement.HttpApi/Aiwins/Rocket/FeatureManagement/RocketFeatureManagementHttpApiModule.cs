using Localization.Resources.RocketUi;
using Aiwins.Rocket.AspNetCore.Mvc;
using Aiwins.Rocket.FeatureManagement.Localization;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.FeatureManagement
{
    [DependsOn(
        typeof(RocketFeatureManagementApplicationContractsModule),
        typeof(RocketAspNetCoreMvcModule))]
    public class RocketFeatureManagementHttpApiModule : RocketModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(RocketFeatureManagementHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<RocketFeatureManagementResource>()
                    .AddBaseTypes(typeof(RocketUiResource));
            });
        }
    }
}
