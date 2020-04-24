using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Users.MongoDB;

namespace Aiwins.Rocket.Identity.MongoDB
{
    [DependsOn(
        typeof(RocketIdentityDomainModule),
        typeof(RocketUsersMongoDbModule)
        )]
    public class RocketIdentityMongoDbModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<RocketIdentityMongoDbContext>(options =>
            {
                options.AddRepository<IdentityUser, MongoIdentityUserRepository>();
                options.AddRepository<IdentityRole, MongoIdentityRoleRepository>();
                options.AddRepository<IdentityClaimType, MongoIdentityRoleRepository>();
            });
        }
    }
}
