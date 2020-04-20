using Aiwins.Rocket.Castle.DynamicProxy;
using Aiwins.Rocket.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.Castle {
    public class RocketCastleCoreModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            context.Services.AddTransient (typeof (RocketAsyncDeterminationInterceptor<>));
        }
    }
}