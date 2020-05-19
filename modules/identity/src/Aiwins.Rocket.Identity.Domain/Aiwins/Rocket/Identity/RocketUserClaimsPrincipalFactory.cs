using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Security.Claims;
using Aiwins.Rocket.Uow;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.Identity {
    public class RocketUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<IdentityUser, IdentityRole>, ITransientDependency {
        public RocketUserClaimsPrincipalFactory (
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> options) : base (
            userManager,
            roleManager,
            options) { }

        [UnitOfWork]
        public override async Task<ClaimsPrincipal> CreateAsync (IdentityUser user) {
            if (user == null) {
                throw new ArgumentNullException (nameof (user));
            }

            var principal = new ClaimsPrincipal (await GenerateIdentityClaimsAsync (user));
            if (user.TenantId.HasValue) {
                principal.Identities
                    .First ()
                    .AddClaim (new Claim (RocketClaimTypes.TenantId, user.TenantId.ToString ()));
            }

            return principal;
        }

        protected virtual async Task<ClaimsIdentity> GenerateIdentityClaimsAsync (IdentityUser user) {
            var id = new ClaimsIdentity ("Identity.Application", Options.ClaimsIdentity.UserNameClaimType, Options.ClaimsIdentity.RoleClaimType);

            id.AddClaim (new Claim (RocketClaimTypes.UserId, user.Id.ToString()));
            id.AddClaim (new Claim (RocketClaimTypes.UserName, user.UserName));
            id.AddClaim (new Claim (RocketClaimTypes.Name, user.Name));

            if (UserManager.SupportsUserSecurityStamp) {
                id.AddClaim (new Claim (Options.ClaimsIdentity.SecurityStampClaimType,user.SecurityStamp));
            }
            if (UserManager.SupportsUserClaim) {
                id.AddClaims (await UserManager.GetClaimsAsync (user));
            }

            if (UserManager.SupportsUserRole) {
                var roles = await UserManager.GetRolesAsync (user);
                foreach (var roleName in roles) {
                    var role = await RoleManager.FindByNameAsync (roleName);
                    if (role != null) {
                        id.AddClaim (new Claim (RocketClaimTypes.RoleId, role.Id.ToString()));
                        id.AddClaim (new Claim (RocketClaimTypes.Role, role.Name));
                        if (RoleManager.SupportsRoleClaims) {
                            id.AddClaims (await RoleManager.GetClaimsAsync (role));
                        }
                    }
                }
            }
            return id;
        }

        // [UnitOfWork]
        // public override async Task<ClaimsPrincipal> CreateAsync (IdentityUser user) {
        //     var principal = await base.CreateAsync (user);

        //     if (user.TenantId.HasValue) {
        //         principal.Identities
        //             .First ()
        //             .AddClaim (new Claim (RocketClaimTypes.TenantId, user.TenantId.ToString ()));
        //     }

        //     return principal;
        // }
    }
}