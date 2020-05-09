using Aiwins.Rocket.Application;
using Aiwins.Rocket.Authorization.Permissions;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.VirtualFileSystem;
using Aiwins.Docs.Localization;

namespace Aiwins.Docs.Admin
{
    [DependsOn(
        typeof(DocsDomainSharedModule),
        typeof(RocketDddApplicationModule)
        )]
    public class DocsAdminApplicationContractsModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DocsAdminApplicationContractsModule>();
            });

            Configure<RocketLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<DocsResource>()
                    .AddVirtualJson("Aiwins/Docs/Admin/Localization/Resources/Docs/ApplicationContracts");
            });
        }
    }
}
