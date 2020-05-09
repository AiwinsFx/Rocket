using Aiwins.Rocket.Application;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.VirtualFileSystem;
using Aiwins.Rocket.Authorization;

namespace MyCompanyName.MyProjectName
{
    [DependsOn(
        typeof(MyProjectNameDomainSharedModule),
        typeof(RocketDddApplicationContractsModule),
        typeof(RocketAuthorizationModule)
        )]
    public class MyProjectNameApplicationContractsModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<MyProjectNameApplicationContractsModule>("MyCompanyName.MyProjectName");
            });
        }
    }
}
