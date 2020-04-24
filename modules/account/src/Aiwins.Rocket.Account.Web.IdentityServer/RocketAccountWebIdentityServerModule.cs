using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.Identity.AspNetCore;
using Aiwins.Rocket.IdentityServer;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.VirtualFileSystem;

namespace Aiwins.Rocket.Account.Web
{
    [DependsOn(
        typeof(RocketAccountWebModule),
        typeof(RocketIdentityServerDomainModule)
        )]
    public class RocketAccountWebIdentityServerModule : RocketModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<RocketIdentityAspNetCoreOptions>(options =>
            {
                options.ConfigureAuthentication = false;
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(RocketAccountWebIdentityServerModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<RocketAccountWebIdentityServerModule>("Aiwins.Rocket.Account.Web");
            });

            //TODO: Try to reuse from RocketIdentityAspNetCoreModule
            context.Services
                .AddAuthentication(o =>
                {
                    o.DefaultScheme = IdentityConstants.ApplicationScheme;
                    o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                })
                .AddIdentityCookies();
        }
    }
}
