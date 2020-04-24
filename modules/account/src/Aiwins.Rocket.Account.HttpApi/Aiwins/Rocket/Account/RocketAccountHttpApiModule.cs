using Localization.Resources.RocketUi;
using Aiwins.Rocket.Account.Localization;
using Aiwins.Rocket.AspNetCore.Mvc;
using Aiwins.Rocket.Identity;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.Account
{
    [DependsOn(
        typeof(RocketAccountApplicationContractsModule),
        typeof(RocketIdentityHttpApiModule),
        typeof(RocketAspNetCoreMvcModule))]
    public class RocketAccountHttpApiModule : RocketModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(RocketAccountHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<AccountResource>()
                    .AddBaseTypes(typeof(RocketUiResource));
            });
        }
    }
}