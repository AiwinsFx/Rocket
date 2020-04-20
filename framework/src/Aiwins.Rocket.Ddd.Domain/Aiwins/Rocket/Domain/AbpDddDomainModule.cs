using Aiwins.Rocket.Auditing;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.EventBus;
using Aiwins.Rocket.Guids;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.MultiTenancy;
using Aiwins.Rocket.ObjectMapping;
using Aiwins.Rocket.Threading;
using Aiwins.Rocket.Timing;
using Aiwins.Rocket.Uow;

namespace Aiwins.Rocket.Domain
{
    [DependsOn(
        typeof(RocketAuditingModule),
        typeof(RocketDataModule),
        typeof(RocketEventBusModule),
        typeof(RocketGuidsModule),
        typeof(RocketMultiTenancyModule),
        typeof(RocketThreadingModule),
        typeof(RocketTimingModule),
        typeof(RocketUnitOfWorkModule),
        typeof(RocketObjectMappingModule)
        )]
    public class RocketDddDomainModule : RocketModule
    {

    }
}
