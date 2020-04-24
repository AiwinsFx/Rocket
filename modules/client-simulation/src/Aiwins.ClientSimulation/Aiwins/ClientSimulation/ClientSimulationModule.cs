using Aiwins.Rocket.Http.Client.IdentityModel;
using Aiwins.Rocket.Modularity;

namespace Aiwins.ClientSimulation
{
    [DependsOn(
        typeof(RocketHttpClientIdentityModelModule)
        )]
    public class ClientSimulationModule : RocketModule
    {
        
    }
}
