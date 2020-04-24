using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.Identity.AspNetCore
{
    [DependsOn(
        typeof(RocketIdentityDomainModule)
        )]
    public class RocketIdentityAspNetCoreModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services
                .GetObject<IdentityBuilder>()
                .AddDefaultTokenProviders()
                .AddSignInManager();

            //(TODO: Extract an extension method like IdentityBuilder.AddRocketSecurityStampValidator())
            context.Services.AddScoped<RocketSecurityStampValidator>();
            context.Services.AddScoped(typeof(SecurityStampValidator<IdentityUser>), provider => provider.GetService(typeof(RocketSecurityStampValidator)));
            context.Services.AddScoped(typeof(ISecurityStampValidator), provider => provider.GetService(typeof(RocketSecurityStampValidator)));

            var options = context.Services.ExecutePreConfiguredActions(new RocketIdentityAspNetCoreOptions());

            if (options.ConfigureAuthentication)
            {
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
}
