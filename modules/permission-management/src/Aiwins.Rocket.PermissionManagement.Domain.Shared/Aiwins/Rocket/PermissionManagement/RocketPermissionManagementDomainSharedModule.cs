using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.PermissionManagement.Localization;
using Aiwins.Rocket.Validation;
using Aiwins.Rocket.Validation.Localization;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.Rocket.PermissionManagement
{
    [DependsOn(
        typeof(RocketValidationModule)
        )]
    public class RocketPermissionManagementDomainSharedModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<RocketPermissionManagementDomainSharedModule>();
            });

            Configure<RocketLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<RocketPermissionManagementResource>("en")
                    .AddBaseTypes(
                        typeof(RocketValidationResource)
                    ).AddVirtualJson("/Aiwins/Rocket/PermissionManagement/Localization/Domain");
            });
        }
    }
}
