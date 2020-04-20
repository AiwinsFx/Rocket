using Aiwins.Rocket.Castle;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.Autofac {
    [DependsOn (typeof (RocketCastleCoreModule))]
    public class RocketAutofacModule : RocketModule {

    }
}