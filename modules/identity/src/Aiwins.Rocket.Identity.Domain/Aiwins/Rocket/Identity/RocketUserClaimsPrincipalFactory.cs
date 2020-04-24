using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Security.Claims;
using Aiwins.Rocket.Uow;

namespace Aiwins.Rocket.Identity
{
    public class RocketUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<IdentityUser, IdentityRole>, ITransientDependency
    {
        public RocketUserClaimsPrincipalFactory(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager, 
            IOptions<IdentityOptions> options) 
            : base(
                  userManager, 
                  roleManager, 
                  options)
        {
        }

        [UnitOfWork]
        public override async Task<ClaimsPrincipal> CreateAsync(IdentityUser user)
        {
            var principal = await base.CreateAsync(user);

            if (user.TenantId.HasValue)
            {
                principal.Identities
                    .First()
                    .AddClaim(new Claim(RocketClaimTypes.TenantId, user.TenantId.ToString()));
            }

            return principal;
        }
    }
}
