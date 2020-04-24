using System;
using System.IdentityModel.Tokens.Jwt;
using IdentityModel;
using IdentityServer4.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Aiwins.Rocket.Identity;
using Aiwins.Rocket.IdentityServer.AspNetIdentity;
using Aiwins.Rocket.Security.Claims;

namespace Aiwins.Rocket.IdentityServer
{
    public static class RocketIdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddRocketIdentityServer(
            this IIdentityServerBuilder builder,
            RocketIdentityServerBuilderOptions options = null)
        {
            if (options == null)
            {
                options = new RocketIdentityServerBuilderOptions();
            }

            //TODO: AspNet Identity integration lines. Can be extracted to a extension method
            if (options.IntegrateToAspNetIdentity)
            {
                builder.AddAspNetIdentity<IdentityUser>();
                builder.AddProfileService<RocketProfileService>();
                builder.AddResourceOwnerValidator<RocketResourceOwnerPasswordValidator>();
            }

            builder.Services.Replace(ServiceDescriptor.Transient<IClaimsService, RocketClaimsService>());

            if (options.UpdateRocketClaimTypes)
            {
                RocketClaimTypes.UserId = JwtClaimTypes.Subject;
                RocketClaimTypes.UserName = JwtClaimTypes.Name;
                RocketClaimTypes.Role = JwtClaimTypes.Role;
                RocketClaimTypes.Email = JwtClaimTypes.Email;
            }

            if (options.UpdateJwtSecurityTokenHandlerDefaultInboundClaimTypeMap)
            {
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap[RocketClaimTypes.UserId] = RocketClaimTypes.UserId;
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap[RocketClaimTypes.UserName] = RocketClaimTypes.UserName;
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap[RocketClaimTypes.Role] = RocketClaimTypes.Role;
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap[RocketClaimTypes.Email] = RocketClaimTypes.Email;
            }

            return builder;
        }
    }
}