using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Localization;
using MyCompanyName.MyProjectName.Localization;
using Aiwins.Rocket.Localization.ExceptionHandling;
using Aiwins.Rocket.Validation;
using Aiwins.Rocket.Validation.Localization;
using Aiwins.Rocket.VirtualFileSystem;

namespace MyCompanyName.MyProjectName
{
    [DependsOn(
        typeof(RocketValidationModule)
    )]
    public class MyProjectNameDomainSharedModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<MyProjectNameDomainSharedModule>("MyCompanyName.MyProjectName");
            });

            Configure<RocketLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<MyProjectNameResource>("en")
                    .AddBaseTypes(typeof(RocketValidationResource))
                    .AddVirtualJson("/Localization/MyProjectName");
            });

            Configure<RocketExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("MyProjectName", typeof(MyProjectNameResource));
            });
        }
    }
}
