using Aiwins.Rocket.IdentityModel;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.Http.Client.IdentityModel {
    [DependsOn (
        typeof (RocketHttpClientModule),
        typeof (RocketIdentityModelModule)
    )]
    public class RocketHttpClientIdentityModelModule : RocketModule {

    }
}