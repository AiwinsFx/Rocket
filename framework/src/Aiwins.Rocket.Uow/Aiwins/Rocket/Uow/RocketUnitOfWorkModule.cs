using Aiwins.Rocket.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.Uow {
    public class RocketUnitOfWorkModule : RocketModule {
        public override void PreConfigureServices (ServiceConfigurationContext context) {
            context.Services.OnRegistred (UnitOfWorkInterceptorRegistrar.RegisterIfNeeded);
        }
    }
}