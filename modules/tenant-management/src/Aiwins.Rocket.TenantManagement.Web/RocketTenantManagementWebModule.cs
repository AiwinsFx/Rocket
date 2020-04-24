using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.AspNetCore.Mvc.Localization;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap;
using Aiwins.Rocket.AutoMapper;
using Aiwins.Rocket.FeatureManagement;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.TenantManagement.Localization;
using Aiwins.Rocket.TenantManagement.Web.Navigation;
using Aiwins.Rocket.UI.Navigation;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.Rocket.TenantManagement.Web
{
    [DependsOn(typeof(RocketTenantManagementHttpApiModule))]
    [DependsOn(typeof(RocketAspNetCoreMvcUiBootstrapModule))]
    [DependsOn(typeof(RocketFeatureManagementWebModule))]
    [DependsOn(typeof(RocketAutoMapperModule))]
    public class RocketTenantManagementWebModule : RocketModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<RocketMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(RocketTenantManagementResource), typeof(RocketTenantManagementWebModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(RocketTenantManagementWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new RocketTenantManagementWebMainMenuContributor());
            });

            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<RocketTenantManagementWebModule>("Aiwins.Rocket.TenantManagement.Web");
            });

            context.Services.AddAutoMapperObjectMapper<RocketTenantManagementWebModule>();
            Configure<RocketAutoMapperOptions>(options =>
            {
                options.AddProfile<RocketTenantManagementWebAutoMapperProfile>(validate: true);
            });

            Configure<RazorPagesOptions>(options =>
            {
                options.Conventions.AuthorizePage("/TenantManagement/Tenants/Index", TenantManagementPermissions.Tenants.Default);
                options.Conventions.AuthorizePage("/TenantManagement/Tenants/CreateModal", TenantManagementPermissions.Tenants.Create);
                options.Conventions.AuthorizePage("/TenantManagement/Tenants/EditModal", TenantManagementPermissions.Tenants.Update);
                options.Conventions.AuthorizePage("/TenantManagement/Tenants/ConnectionStrings", TenantManagementPermissions.Tenants.ManageConnectionStrings);
            });
        }
    }
}