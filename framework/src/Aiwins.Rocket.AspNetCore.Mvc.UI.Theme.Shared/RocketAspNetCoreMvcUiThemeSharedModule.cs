using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Bundling;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Widgets;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared
{
    [DependsOn(
        typeof(RocketAspNetCoreMvcUiBootstrapModule),
        typeof(RocketAspNetCoreMvcUiPackagesModule),
        typeof(RocketAspNetCoreMvcUiWidgetsModule)
        )]
    public class RocketAspNetCoreMvcUiThemeSharedModule : RocketModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(RocketAspNetCoreMvcUiThemeSharedModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<RocketAspNetCoreMvcUiThemeSharedModule>("Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared");
            });

            Configure<RocketBundlingOptions>(options =>
            {
                options
                    .StyleBundles
                    .Add(StandardBundles.Styles.Global, bundle => { bundle.AddContributors(typeof(SharedThemeGlobalStyleContributor)); });

                options
                    .ScriptBundles
                    .Add(StandardBundles.Scripts.Global, bundle => bundle.AddContributors(typeof(SharedThemeGlobalScriptContributor)));
            });
        }
    }
}
