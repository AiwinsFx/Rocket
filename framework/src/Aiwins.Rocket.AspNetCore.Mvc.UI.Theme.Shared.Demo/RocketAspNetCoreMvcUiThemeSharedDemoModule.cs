using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Demo
{
    [DependsOn(
        typeof(RocketAspNetCoreMvcUiThemeSharedModule)
        )]
    public class RocketAspNetCoreMvcUiThemeSharedDemoModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<RocketAspNetCoreMvcUiThemeSharedDemoModule>("Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Demo");
            });
        }
    }
}
