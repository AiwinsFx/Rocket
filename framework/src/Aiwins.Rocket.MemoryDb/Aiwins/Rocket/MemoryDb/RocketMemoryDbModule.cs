using Microsoft.Extensions.DependencyInjection.Extensions;
using Aiwins.Rocket.Domain;
using Aiwins.Rocket.Domain.Repositories.MemoryDb;
using Aiwins.Rocket.Json;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Uow.MemoryDb;

namespace Aiwins.Rocket.MemoryDb {
    [DependsOn (typeof (RocketDddDomainModule))]
    public class RocketMemoryDbModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            context.Services.TryAddTransient (typeof (IMemoryDatabaseProvider<>), typeof (UnitOfWorkMemoryDatabaseProvider<>));
            context.Services.TryAddTransient (typeof (IMemoryDatabaseCollection<>), typeof (MemoryDatabaseCollection<>));
        }
    }
}