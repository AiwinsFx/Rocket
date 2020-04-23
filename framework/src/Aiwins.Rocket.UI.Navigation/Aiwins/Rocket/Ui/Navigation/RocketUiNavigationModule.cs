using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.UI.Navigation;
using Aiwins.Rocket.UI.Navigation.Localization.Resource;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.Rocket.UI.Navigation
{
    [DependsOn(typeof(RocketUiModule))]
    public class RocketUiNavigationModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<RocketUiNavigationModule>();
            });

            Configure<RocketLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<RocketUiNavigationResource>("en")
                    .AddVirtualJson("/Aiwins/Rocket/Ui/Navigation/Localization/Resource");
            });

            Configure<RocketNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new DefaultMenuContributor());
            });
        }
    }
}
