using Aiwins.Rocket.Application;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.Rocket.FeatureManagement
{
    [DependsOn(
        typeof(RocketFeatureManagementDomainSharedModule),
        typeof(RocketDddApplicationModule)
        )]
    public class RocketFeatureManagementApplicationContractsModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<RocketFeatureManagementApplicationContractsModule>();
            });
        }
    }
}
