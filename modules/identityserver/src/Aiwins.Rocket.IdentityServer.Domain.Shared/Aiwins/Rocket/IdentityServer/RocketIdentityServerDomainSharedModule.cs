using Aiwins.Rocket.IdentityServer.Localization;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Localization.ExceptionHandling;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Validation;
using Aiwins.Rocket.Validation.Localization;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.Rocket.IdentityServer
{
    [DependsOn(
        typeof(RocketValidationModule)
        )]
    public class RocketIdentityServerDomainSharedModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<RocketIdentityServerDomainSharedModule>();
            });

            Configure<RocketLocalizationOptions>(options =>
            {
                options.Resources.Add<RocketIdentityServerResource>("en")
                    .AddBaseTypes(
                        typeof(RocketValidationResource)
                    ).AddVirtualJson("/Aiwins/Rocket/IdentityServer/Localization/Resources");
            });

            Configure<RocketExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("Aiwins.IdentityServer", typeof(RocketIdentityServerResource));
            });
        }
    }
}
