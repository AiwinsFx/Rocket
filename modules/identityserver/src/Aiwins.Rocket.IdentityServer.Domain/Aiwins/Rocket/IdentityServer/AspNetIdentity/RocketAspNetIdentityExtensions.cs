using Aiwins.Rocket.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.IdentityServer.AspNetIdentity {
    public static class RocketAspNetIdentityExtensions {
        public static IIdentityServerBuilder AddRocketAspNetIdentity (this IIdentityServerBuilder builder) {

            return builder.AddAspNetIdentity<IdentityUser> ()
                .AddProfileService<RocketProfileService> ()
                .AddResourceOwnerValidator<RocketResourceOwnerPasswordValidator> ();
        }
    }
}