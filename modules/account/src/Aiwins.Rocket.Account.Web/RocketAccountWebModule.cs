using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.Account.Localization;
using Aiwins.Rocket.AspNetCore.Mvc.Localization;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Aiwins.Rocket.AutoMapper;
using Aiwins.Rocket.Identity.AspNetCore;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.UI.Navigation;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.Rocket.Account.Web
{
    [DependsOn(
        typeof(RocketAccountHttpApiModule),
        typeof(RocketIdentityAspNetCoreModule),
        typeof(RocketAutoMapperModule),
        typeof(RocketAspNetCoreMvcUiThemeSharedModule)
        )]
    public class RocketAccountWebModule : RocketModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<RocketMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(AccountResource), typeof(RocketAccountWebModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(RocketAccountWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<RocketAccountWebModule>("Aiwins.Rocket.Account.Web");
            });

            Configure<RocketNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new RocketAccountUserMenuContributor());
            });

            Configure<RocketToolbarOptions>(options =>
            {
                options.Contributors.Add(new AccountModuleToolbarContributor());
            });

            Configure<RazorPagesOptions>(options =>
            {
                options.Conventions.AuthorizePage("/Account/Manage");
            });

            context.Services.AddAutoMapperObjectMapper<RocketAccountWebModule>();
            Configure<RocketAutoMapperOptions>(options =>
            {
                options.AddProfile<RocketAccountWebAutoMapperProfile>(validate: true);
            });
        }
    }
}
