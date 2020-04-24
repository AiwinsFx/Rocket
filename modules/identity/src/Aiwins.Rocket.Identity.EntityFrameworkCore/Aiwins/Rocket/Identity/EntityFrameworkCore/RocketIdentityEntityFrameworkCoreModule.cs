using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Users.EntityFrameworkCore;

namespace Aiwins.Rocket.Identity.EntityFrameworkCore
{
    [DependsOn(
        typeof(RocketIdentityDomainModule), 
        typeof(RocketUsersEntityFrameworkCoreModule))]
    public class RocketIdentityEntityFrameworkCoreModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddRocketDbContext<IdentityDbContext>(options =>
            {
                options.AddRepository<IdentityUser, EfCoreIdentityUserRepository>();
                options.AddRepository<IdentityRole, EfCoreIdentityRoleRepository>();
                options.AddRepository<IdentityClaimType, EfCoreIdentityClaimTypeRepository>();
            });
        }
    }
}
