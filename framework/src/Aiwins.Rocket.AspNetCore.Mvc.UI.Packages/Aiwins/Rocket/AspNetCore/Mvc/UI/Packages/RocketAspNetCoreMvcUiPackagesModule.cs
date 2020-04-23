using Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Packages
{
    [DependsOn(typeof(RocketAspNetCoreMvcUiBundlingModule))]
    public class RocketAspNetCoreMvcUiPackagesModule : RocketModule
    {

    }
}
