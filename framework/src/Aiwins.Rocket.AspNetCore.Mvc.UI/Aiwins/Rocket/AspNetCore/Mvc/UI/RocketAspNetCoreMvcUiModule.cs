using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.UI.Navigation;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI
{
    [DependsOn(typeof(RocketAspNetCoreMvcModule))]
    [DependsOn(typeof(RocketUiNavigationModule))]
    public class RocketAspNetCoreMvcUiModule : RocketModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(RocketAspNetCoreMvcUiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<RocketAspNetCoreMvcUiModule>();
            });
        }
    }
}
