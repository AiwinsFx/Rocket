using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.TenantManagement.EntityFrameworkCore
{
    [DependsOn(typeof(RocketTenantManagementDomainModule))]
    [DependsOn(typeof(RocketEntityFrameworkCoreModule))]
    public class RocketTenantManagementEntityFrameworkCoreModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddRocketDbContext<TenantManagementDbContext>(options =>
            {
                options.AddDefaultRepositories<ITenantManagementDbContext>();
            });
        }
    }
}
