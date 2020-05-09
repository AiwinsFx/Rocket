using Localization.Resources.RocketUi;
using MyCompanyName.MyProjectName.Localization;
using Aiwins.Rocket.AspNetCore.Mvc;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace MyCompanyName.MyProjectName
{
    [DependsOn(
        typeof(MyProjectNameApplicationContractsModule),
        typeof(RocketAspNetCoreMvcModule))]
    public class MyProjectNameHttpApiModule : RocketModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(MyProjectNameHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<MyProjectNameResource>()
                    .AddBaseTypes(typeof(RocketUiResource));
            });
        }
    }
}
