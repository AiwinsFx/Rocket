using Aiwins.Rocket.AspNetCore.Mvc.Localization;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared;
using Aiwins.Rocket.AutoMapper;
using Aiwins.Rocket.Identity.Localization;
using Aiwins.Rocket.Identity.Web.Navigation;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.UI.Navigation;
using Aiwins.Rocket.VirtualFileSystem;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.Identity.Web {
    [DependsOn (typeof (RocketIdentityHttpApiModule))]
    [DependsOn (typeof (RocketAspNetCoreMvcUiBootstrapModule))]
    [DependsOn (typeof (RocketAutoMapperModule))]
    [DependsOn (typeof (RocketAspNetCoreMvcUiThemeSharedModule))]
    public class RocketIdentityWebModule : RocketModule {
        public override void PreConfigureServices (ServiceConfigurationContext context) {
            context.Services.PreConfigure<RocketMvcDataAnnotationsLocalizationOptions> (options => {
                options.AddAssemblyResource (typeof (IdentityResource), typeof (RocketIdentityWebModule).Assembly);
            });

            PreConfigure<IMvcBuilder> (mvcBuilder => {
                mvcBuilder.AddApplicationPartIfNotExists (typeof (RocketIdentityWebModule).Assembly);
            });
        }

        public override void ConfigureServices (ServiceConfigurationContext context) {
            Configure<RocketNavigationOptions> (options => {
                options.MenuContributors.Add (new RocketIdentityWebMainMenuContributor ());
            });

            Configure<RocketVirtualFileSystemOptions> (options => {
                options.FileSets.AddEmbedded<RocketIdentityWebModule> ("Aiwins.Rocket.Identity.Web");
            });

            context.Services.AddAutoMapperObjectMapper<RocketIdentityWebModule> ();

            Configure<RocketAutoMapperOptions> (options => {
                options.AddProfile<RocketIdentityWebAutoMapperProfile> (validate: true);
            });

            Configure<RazorPagesOptions> (options => {
                options.Conventions.AuthorizePage ("/Identity/Users/Index", IdentityPermissions.Users.Default);
                options.Conventions.AuthorizePage ("/Identity/Users/CreateModal", IdentityPermissions.Users.Create);
                options.Conventions.AuthorizePage ("/Identity/Users/EditModal", IdentityPermissions.Users.Update);
                options.Conventions.AuthorizePage ("/Identity/Roles/Index", IdentityPermissions.Roles.Default);
                options.Conventions.AuthorizePage ("/Identity/Roles/CreateModal", IdentityPermissions.Roles.Create);
                options.Conventions.AuthorizePage ("/Identity/Roles/EditModal", IdentityPermissions.Roles.Update);
            });
        }
    }
}