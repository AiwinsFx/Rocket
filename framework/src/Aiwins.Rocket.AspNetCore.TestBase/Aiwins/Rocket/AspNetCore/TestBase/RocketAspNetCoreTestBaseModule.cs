using Aiwins.Rocket.Http.Client;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.AspNetCore.TestBase {
    [DependsOn (typeof (RocketHttpClientModule))]
    [DependsOn (typeof (RocketAspNetCoreModule))]
    public class RocketAspNetCoreTestBaseModule : RocketModule {

    }
}