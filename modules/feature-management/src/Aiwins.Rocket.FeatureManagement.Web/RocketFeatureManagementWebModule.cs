using Aiwins.Rocket.AspNetCore.Mvc.Localization;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared;
using Aiwins.Rocket.AutoMapper;
using Aiwins.Rocket.FeatureManagement.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.VirtualFileSystem;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.FeatureManagement {
    [DependsOn (
        typeof (RocketFeatureManagementHttpApiModule),
        typeof (RocketAspNetCoreMvcUiThemeSharedModule),
        typeof (RocketAutoMapperModule)
    )]
    public class RocketFeatureManagementWebModule : RocketModule {
        public override void PreConfigureServices (ServiceConfigurationContext context) {
            context.Services.PreConfigure<RocketMvcDataAnnotationsLocalizationOptions> (options => {
                options.AddAssemblyResource (typeof (RocketFeatureManagementResource), typeof (RocketFeatureManagementWebModule).Assembly);
            });

            PreConfigure<IMvcBuilder> (mvcBuilder => {
                mvcBuilder.AddApplicationPartIfNotExists (typeof (RocketFeatureManagementWebModule).Assembly);
            });
        }

        public override void ConfigureServices (ServiceConfigurationContext context) {
            Configure<RocketVirtualFileSystemOptions> (options => {
                options.FileSets.AddEmbedded<RocketFeatureManagementWebModule> ("Aiwins.Rocket.FeatureManagement");
            });

            context.Services.AddAutoMapperObjectMapper<RocketFeatureManagementWebModule> ();
            Configure<RocketAutoMapperOptions> (options => {
                options.AddProfile<FeatureManagementWebAutoMapperProfile> (validate: true);
            });

            Configure<RazorPagesOptions> (options => {
                //Configure authorization.
            });
        }
    }
}