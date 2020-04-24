using Localization.Resources.RocketUi;
using Aiwins.Rocket.AspNetCore.Mvc;
using Aiwins.Rocket.FeatureManagement;
using Aiwins.Rocket.FeatureManagement.Localization;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.TenantManagement.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.TenantManagement
{
    [DependsOn(
        typeof(RocketTenantManagementApplicationContractsModule),
        typeof(RocketFeatureManagementHttpApiModule),
        typeof(RocketAspNetCoreMvcModule)
        )]
    public class RocketTenantManagementHttpApiModule : RocketModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(RocketTenantManagementHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<RocketTenantManagementResource>()
                    .AddBaseTypes(
                        typeof(RocketFeatureManagementResource),
                        typeof(RocketUiResource));
            });
        }
    }
}