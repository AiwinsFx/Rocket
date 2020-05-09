using Aiwins.Rocket.Identity.Localization;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Localization.ExceptionHandling;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Users;
using Aiwins.Rocket.Validation;
using Aiwins.Rocket.Validation.Localization;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.Rocket.Identity {
    [DependsOn (
        typeof (RocketUsersDomainSharedModule),
        typeof (RocketValidationModule)
    )]
    public class RocketIdentityDomainSharedModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            Configure<RocketVirtualFileSystemOptions> (options => {
                options.FileSets.AddEmbedded<RocketIdentityDomainSharedModule> ();
            });

            Configure<RocketLocalizationOptions> (options => {
                options.Resources
                    .Add<IdentityResource> ("zh-Hans")
                    .AddBaseTypes (
                        typeof (RocketValidationResource)
                    ).AddVirtualJson ("/Aiwins/Rocket/Identity/Localization");
            });

            Configure<RocketExceptionLocalizationOptions> (options => {
                options.MapCodeNamespace ("Aiwins.Rocket.Identity", typeof (IdentityResource));
            });
        }
    }
}