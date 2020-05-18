using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.AspNetCore.MultiTenancy;
using Aiwins.Rocket.AspNetCore.Mvc.Localization;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling;
using Aiwins.Rocket.AspNetCore.Mvc.UI.MultiTenancy.Localization;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Bundling;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.MultiTenancy
{
    [DependsOn(
        typeof(RocketAspNetCoreMvcUiThemeSharedModule),
        typeof(RocketAspNetCoreMultiTenancyModule)
        )]
    public class RocketAspNetCoreMvcUiMultiTenancyModule : RocketModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<RocketMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(
                    typeof(RocketUiMultiTenancyResource),
                    typeof(RocketAspNetCoreMvcUiMultiTenancyModule).Assembly
                );
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(RocketAspNetCoreMvcUiMultiTenancyModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<RocketAspNetCoreMvcUiMultiTenancyModule>();
            });

            Configure<RocketLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<RocketUiMultiTenancyResource>("zh-Hans")
                    .AddVirtualJson("/Aiwins/Rocket/AspNetCore/Mvc/UI/MultiTenancy/Localization");
            });

            Configure<RocketBundlingOptions>(options =>
            {
                options.ScriptBundles
                    .Get(StandardBundles.Scripts.Global)
                    .AddFiles(
                        "/Pages/Rocket/MultiTenancy/tenant-switch.js"
                    );
            });
        }
    }
}
