using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap
{
    [DependsOn(typeof(RocketAspNetCoreMvcUiModule))]
    public class RocketAspNetCoreMvcUiBootstrapModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<RocketAspNetCoreMvcUiBootstrapModule>("Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap");
            });
        }
    }
}
