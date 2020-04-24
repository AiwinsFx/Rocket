using System.Threading.Tasks;
using System.Security.Principal;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Aiwins.Rocket.Identity;
using Aiwins.Rocket.MultiTenancy;
using Aiwins.Rocket.Uow;
using IdentityUser = Aiwins.Rocket.Identity.IdentityUser;

namespace Aiwins.Rocket.IdentityServer.AspNetIdentity
{
    public class RocketProfileService : ProfileService<IdentityUser>
    {
        protected ICurrentTenant CurrentTenant { get; }

        public RocketProfileService(
            IdentityUserManager userManager,
            IUserClaimsPrincipalFactory<IdentityUser> claimsFactory,
            ICurrentTenant currentTenant)
            : base(userManager, claimsFactory)
        {
            CurrentTenant = currentTenant;
        }

        [UnitOfWork]
        public override async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            using (CurrentTenant.Change(context.Subject.FindTenantId()))
            {
                await base.GetProfileDataAsync(context);
            }
        }

        [UnitOfWork]
        public override async Task IsActiveAsync(IsActiveContext context)
        {
            using (CurrentTenant.Change(context.Subject.FindTenantId()))
            {
                await base.IsActiveAsync(context);
            }
        }
    }
}
