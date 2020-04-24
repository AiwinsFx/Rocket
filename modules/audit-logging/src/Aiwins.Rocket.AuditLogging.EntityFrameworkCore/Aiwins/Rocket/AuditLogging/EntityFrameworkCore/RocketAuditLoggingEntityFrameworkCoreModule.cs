using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.AuditLogging.EntityFrameworkCore
{
    [DependsOn(typeof(RocketAuditLoggingDomainModule))]
    [DependsOn(typeof(RocketEntityFrameworkCoreModule))]
    public class RocketAuditLoggingEntityFrameworkCoreModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddRocketDbContext<RocketAuditLoggingDbContext>(options =>
            {
                options.AddRepository<AuditLog, EfCoreAuditLogRepository>();
            });
        }
    }
}
