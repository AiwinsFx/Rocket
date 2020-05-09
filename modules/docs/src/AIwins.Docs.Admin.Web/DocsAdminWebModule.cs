using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.AspNetCore.Mvc.Localization;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap;
using Aiwins.Rocket.AutoMapper;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.UI.Navigation;
using Aiwins.Rocket.VirtualFileSystem;
using Aiwins.Docs.Admin.Navigation;
using Aiwins.Docs.Localization;

namespace Aiwins.Docs.Admin
{
    [DependsOn(
        typeof(DocsAdminHttpApiModule),
        typeof(RocketAspNetCoreMvcUiBootstrapModule)
        )]
    public class DocsAdminWebModule : RocketModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<RocketMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(DocsResource), typeof(DocsAdminWebModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(DocsAdminWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {

            Configure<RocketNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new DocsMenuContributor());
            });

            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DocsAdminWebModule>("Aiwins.Docs.Admin");
            });

            context.Services.AddAutoMapperObjectMapper<DocsAdminWebModule>();
            Configure<RocketAutoMapperOptions>(options =>
            {
                options.AddProfile<DocsAdminWebAutoMapperProfile>(validate: true);
            });
        }
    }
}
