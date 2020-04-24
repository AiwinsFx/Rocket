using Aiwins.Rocket.FeatureManagement.Localization;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Validation;
using Aiwins.Rocket.Validation.Localization;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.Rocket.FeatureManagement
{
    [DependsOn(
        typeof(RocketValidationModule)
        )]
    public class RocketFeatureManagementDomainSharedModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<RocketFeatureManagementDomainSharedModule>();
            });

            Configure<RocketLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<RocketFeatureManagementResource>("en")
                    .AddBaseTypes(
                        typeof(RocketValidationResource)
                    ).AddVirtualJson("Aiwins/Rocket/FeatureManagement/Localization/Domain");
            });
        }
    }
}
