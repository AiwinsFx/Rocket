using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Validation;
using Aiwins.Rocket.Validation.Localization;
using Aiwins.Blogging.Localization;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.Blogging
{
    [DependsOn(typeof(RocketValidationModule))]
    public class BloggingDomainSharedModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<BloggingDomainSharedModule>();
            });

            Configure<RocketLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<BloggingResource>("en")
                    .AddBaseTypes(typeof(RocketValidationResource))
                    .AddVirtualJson("Aiwins/Blogging/Localization/Resources");
            });
        }
    }
}
