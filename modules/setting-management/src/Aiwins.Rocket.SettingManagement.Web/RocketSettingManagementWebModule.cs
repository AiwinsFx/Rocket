using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.SettingManagement.Web.Navigation;
using Aiwins.Rocket.UI.Navigation;
using Aiwins.Rocket.VirtualFileSystem;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.SettingManagement.Web {
    [DependsOn (
        typeof (RocketAspNetCoreMvcUiThemeSharedModule),
        typeof (RocketSettingManagementDomainSharedModule)
    )]
    public class RocketSettingManagementWebModule : RocketModule {
        public override void PreConfigureServices (ServiceConfigurationContext context) {
            PreConfigure<IMvcBuilder> (mvcBuilder => {
                mvcBuilder.AddApplicationPartIfNotExists (typeof (RocketSettingManagementWebModule).Assembly);
            });
        }

        public override void ConfigureServices (ServiceConfigurationContext context) {
            Configure<RocketNavigationOptions> (options => {
                options.MenuContributors.Add (new SettingManagementMainMenuContributor ());
            });

            Configure<RocketVirtualFileSystemOptions> (options => {
                options.FileSets.AddEmbedded<RocketSettingManagementWebModule> ("Aiwins.Rocket.SettingManagement.Web");
            });
        }
    }
}