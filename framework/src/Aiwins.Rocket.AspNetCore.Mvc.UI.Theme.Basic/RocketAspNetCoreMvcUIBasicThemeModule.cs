using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling;
using Aiwins.Rocket.AspNetCore.Mvc.UI.MultiTenancy;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Basic.Bundling;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Basic.Toolbars;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Bundling;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theming;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Basic
{
    [DependsOn(
        typeof(RocketAspNetCoreMvcUiThemeSharedModule),
        typeof(RocketAspNetCoreMvcUiMultiTenancyModule)
        )]
    public class RocketAspNetCoreMvcUiBasicThemeModule : RocketModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(RocketAspNetCoreMvcUiBasicThemeModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketThemingOptions>(options =>
            {
                options.Themes.Add<BasicTheme>();

                if (options.DefaultThemeName == null)
                {
                    options.DefaultThemeName = BasicTheme.Name;
                }
            });

            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<RocketAspNetCoreMvcUiBasicThemeModule>("Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Basic");
            });

            Configure<RocketToolbarOptions>(options =>
            {
                options.Contributors.Add(new BasicThemeMainTopToolbarContributor());
            });

            Configure<RocketBundlingOptions>(options =>
            {
                options
                    .StyleBundles
                    .Add(BasicThemeBundles.Styles.Global, bundle =>
                    {
                        bundle
                            .AddBaseBundles(StandardBundles.Styles.Global)
                            .AddContributors(typeof(BasicThemeGlobalStyleContributor));
                    });

                options
                    .ScriptBundles
                    .Add(BasicThemeBundles.Scripts.Global, bundle =>
                    {
                        bundle
                            .AddBaseBundles(StandardBundles.Scripts.Global)
                            .AddContributors(typeof(BasicThemeGlobalScriptContributor));
                    });
            });
        }
    }
}
