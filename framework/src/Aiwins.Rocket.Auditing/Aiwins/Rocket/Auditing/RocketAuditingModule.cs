using Aiwins.Rocket.Data;
using Aiwins.Rocket.Json;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.MultiTenancy;
using Aiwins.Rocket.Security;
using Aiwins.Rocket.Threading;
using Aiwins.Rocket.Timing;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.Auditing {
    [DependsOn (
        typeof (RocketDataModule),
        typeof (RocketJsonModule),
        typeof (RocketTimingModule),
        typeof (RocketSecurityModule),
        typeof (RocketThreadingModule),
        typeof (RocketMultiTenancyModule)
    )]
    public class RocketAuditingModule : RocketModule {
        public override void PreConfigureServices (ServiceConfigurationContext context) {
            context.Services.OnRegistred (AuditingInterceptorRegistrar.RegisterIfNeeded);
        }
    }
}