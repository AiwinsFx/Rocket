using System;
using Aiwins.Rocket.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection {
    public static class RocketIdentityServiceCollectionExtensions {
        public static IdentityBuilder AddRocketIdentity (this IServiceCollection services) {
            return services.AddRocketIdentity (setupAction: null);
        }

        public static IdentityBuilder AddRocketIdentity (this IServiceCollection services, Action<IdentityOptions> setupAction) {
            //RocketRoleManager
            services.TryAddScoped<IdentityRoleManager> ();
            services.TryAddScoped (typeof (RoleManager<IdentityRole>), provider => provider.GetService (typeof (IdentityRoleManager)));

            //RocketUserManager
            services.TryAddScoped<IdentityUserManager> ();
            services.TryAddScoped (typeof (UserManager<IdentityUser>), provider => provider.GetService (typeof (IdentityUserManager)));

            //RocketUserStore
            services.TryAddScoped<IdentityUserStore> ();
            services.TryAddScoped (typeof (IUserStore<IdentityUser>), provider => provider.GetService (typeof (IdentityUserStore)));

            //RocketRoleStore
            services.TryAddScoped<IdentityRoleStore> ();
            services.TryAddScoped (typeof (IRoleStore<IdentityRole>), provider => provider.GetService (typeof (IdentityRoleStore)));

            return services
                .AddIdentityCore<IdentityUser> (setupAction)
                .AddRoles<IdentityRole> ()
            .AddClaimsPrincipalFactory<RocketUserClaimsPrincipalFactory> ();
        }
    }
}