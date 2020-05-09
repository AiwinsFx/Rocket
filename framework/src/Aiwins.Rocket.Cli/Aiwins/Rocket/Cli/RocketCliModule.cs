using Aiwins.Rocket.Autofac;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.Cli
{
    [DependsOn(
        typeof(RocketCliCoreModule),
        typeof(RocketAutofacModule)
    )]
    public class RocketCliModule : RocketModule
    {

    }
}