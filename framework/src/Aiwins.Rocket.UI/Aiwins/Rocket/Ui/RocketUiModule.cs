using Localization.Resources.RocketUi;
using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.Rocket.UI
{
    [DependsOn(
        typeof(RocketLocalizationModule)
    )]
    public class RocketUiModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<RocketUiModule>();
            });

            Configure<RocketLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<RocketUiResource>("en")
                    .AddVirtualJson("/Localization/Resources/RocketUi");
            });
        }
    }
}
