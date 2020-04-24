using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.AspNetCore.Mvc.Localization;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap;
using Aiwins.Rocket.AutoMapper;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.PermissionManagement.HttpApi;
using Aiwins.Rocket.PermissionManagement.Localization;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.Rocket.PermissionManagement.Web
{
    [DependsOn(typeof(RocketPermissionManagementHttpApiModule))]
    [DependsOn(typeof(RocketAspNetCoreMvcUiBootstrapModule))]
    [DependsOn(typeof(RocketAutoMapperModule))]
    public class RocketPermissionManagementWebModule : RocketModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<RocketMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(RocketPermissionManagementResource));
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(RocketPermissionManagementWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<RocketPermissionManagementWebModule>("Aiwins.Rocket.PermissionManagement.Web");
            });

            context.Services.AddAutoMapperObjectMapper<RocketPermissionManagementWebModule>();
            Configure<RocketAutoMapperOptions>(options =>
            {
                options.AddProfile<RocketPermissionManagementWebAutoMapperProfile>(validate: true);
            });
        }
    }
}
