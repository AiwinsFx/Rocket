using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.MongoDB;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.AuditLogging.MongoDB {
    [DependsOn (typeof (RocketAuditLoggingDomainModule))]
    [DependsOn (typeof (RocketMongoDbModule))]
    public class RocketAuditLoggingMongoDbModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            context.Services.AddMongoDbContext<AuditLoggingMongoDbContext> (options => {
                options.AddRepository<AuditLog, MongoAuditLogRepository> ();
            });
        }
    }
}