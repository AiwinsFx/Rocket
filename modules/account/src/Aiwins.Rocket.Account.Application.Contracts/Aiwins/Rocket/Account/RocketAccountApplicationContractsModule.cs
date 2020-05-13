using Aiwins.Rocket.Account.Localization;
using Aiwins.Rocket.Identity;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Localization.ExceptionHandling;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Validation.Localization;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.Rocket.Account {
    [DependsOn (
        typeof (RocketIdentityApplicationContractsModule)
    )]
    public class RocketAccountApplicationContractsModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            Configure<RocketVirtualFileSystemOptions> (options => {
                options.FileSets.AddEmbedded<RocketAccountApplicationContractsModule> ();
            });

            Configure<RocketLocalizationOptions> (options => {
                options.Resources
                    .Add<AccountResource> ("zh-Hans")
                    .AddBaseTypes (typeof (RocketValidationResource))
                    .AddVirtualJson ("/Aiwins/Rocket/Account/Localization/Resources");
            });

            Configure<RocketExceptionLocalizationOptions> (options => {
                options.MapCodeNamespace ("Aiwins.Account", typeof (AccountResource));
            });
        }
    }
}