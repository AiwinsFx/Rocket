using Aiwins.Rocket.Application;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.AspNetCore.Mvc
{
    [DependsOn(
        typeof(RocketDddApplicationModule)
        )]
    public class RocketAspNetCoreMvcContractsModule : RocketModule
    {

    }
}
