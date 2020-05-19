using System;
using System.IdentityModel.Tokens.Jwt;
using Aiwins.Rocket.Identity;
using Aiwins.Rocket.IdentityServer.AspNetIdentity;
using Aiwins.Rocket.Security.Claims;
using IdentityModel;
using IdentityServer4.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Aiwins.Rocket.IdentityServer {
    public static class RocketIdentityServerBuilderExtensions {
        public static IIdentityServerBuilder AddRocketIdentityServer (
            this IIdentityServerBuilder builder,
            RocketIdentityServerBuilderOptions options = null) {
            if (options == null) {
                options = new RocketIdentityServerBuilderOptions ();
            }

            // Identity
            if (options.IntegrateToAspNetIdentity) {
                builder.AddRocketAspNetIdentity();
            }

            builder.Services.Replace (ServiceDescriptor.Transient<IClaimsService, RocketClaimsService> ());

            if (options.UpdateRocketClaimTypes) {
                RocketClaimTypes.UserId = JwtClaimTypes.Subject;
                RocketClaimTypes.Name = JwtClaimTypes.GivenName;
                RocketClaimTypes.UserName = JwtClaimTypes.Name;
                RocketClaimTypes.Role = JwtClaimTypes.Role;
                RocketClaimTypes.Email = JwtClaimTypes.Email;
                RocketClaimTypes.PhoneNumber = JwtClaimTypes.PhoneNumber;
            }

            if (options.UpdateJwtSecurityTokenHandlerDefaultInboundClaimTypeMap) {
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap[RocketClaimTypes.UserId] = RocketClaimTypes.UserId;
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap[RocketClaimTypes.Name] = RocketClaimTypes.Name;
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap[RocketClaimTypes.UserName] = RocketClaimTypes.UserName;
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap[RocketClaimTypes.Role] = RocketClaimTypes.Role;
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap[RocketClaimTypes.Email] = RocketClaimTypes.Email;
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap[RocketClaimTypes.PhoneNumber] = RocketClaimTypes.PhoneNumber;
            }

            return builder;
        }
    }
}