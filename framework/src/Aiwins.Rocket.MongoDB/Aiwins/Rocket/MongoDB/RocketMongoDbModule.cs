using Aiwins.Rocket.Domain;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Uow.MongoDB;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Aiwins.Rocket.MongoDB {
    [DependsOn (typeof (RocketDddDomainModule))]
    public class RocketMongoDbModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            context.Services.TryAddTransient (
                typeof (IMongoDbContextProvider<>),
                typeof (UnitOfWorkMongoDbContextProvider<>)
            );
        }
    }
}