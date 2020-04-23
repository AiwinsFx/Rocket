using Aiwins.Rocket.Autofac;
using Aiwins.Rocket.Http.Client;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.AspNetCore.TestBase {
    [DependsOn (typeof (RocketHttpClientModule))]
    [DependsOn (typeof (RocketAspNetCoreModule))]
    [DependsOn (typeof (RocketTestBaseModule))]
    [DependsOn (typeof (RocketAutofacModule))]
    public class RocketAspNetCoreTestBaseModule : RocketModule {

    }
}