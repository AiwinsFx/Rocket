using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.VirtualFileSystem;
using Aiwins.Blogging.Localization;

namespace Aiwins.Blogging
{
    [DependsOn(typeof(BloggingDomainSharedModule))]
    public class BloggingApplicationContractsModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<BloggingApplicationContractsModule>();
            });
            Configure<RocketLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<BloggingResource>()
                    .AddVirtualJson("Aiwins/Blogging/Localization/Resources/Blogging/ApplicationContracts");
            });
        }
    }
}
