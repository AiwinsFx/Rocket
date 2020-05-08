using Aiwins.Rocket.Auditing;
using Aiwins.Rocket.Domain;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.AuditLogging {
    [DependsOn (typeof (RocketAuditingModule))]
    [DependsOn (typeof (RocketDddDomainModule))]
    [DependsOn (typeof (RocketAuditLoggingDomainSharedModule))]
    public class RocketAuditLoggingDomainModule : RocketModule {

    }
}