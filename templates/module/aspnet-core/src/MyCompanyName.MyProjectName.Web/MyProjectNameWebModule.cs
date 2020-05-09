using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using MyCompanyName.MyProjectName.Localization;
using Aiwins.Rocket.AspNetCore.Mvc.Localization;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared;
using Aiwins.Rocket.AutoMapper;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.UI.Navigation;
using Aiwins.Rocket.VirtualFileSystem;

namespace MyCompanyName.MyProjectName.Web
{
    [DependsOn(
        typeof(MyProjectNameHttpApiModule),
        typeof(RocketAspNetCoreMvcUiThemeSharedModule),
        typeof(RocketAutoMapperModule)
        )]
    public class MyProjectNameWebModule : RocketModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<RocketMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(MyProjectNameResource), typeof(MyProjectNameWebModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(MyProjectNameWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new MyProjectNameMenuContributor());
            });

            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<MyProjectNameWebModule>("MyCompanyName.MyProjectName.Web");
            });

            context.Services.AddAutoMapperObjectMapper<MyProjectNameWebModule>();
            Configure<RocketAutoMapperOptions>(options =>
            {
                options.AddMaps<MyProjectNameWebModule>(validate: true);
            });

            Configure<RazorPagesOptions>(options =>
            {
                //Configure authorization.
            });
        }
    }
}
