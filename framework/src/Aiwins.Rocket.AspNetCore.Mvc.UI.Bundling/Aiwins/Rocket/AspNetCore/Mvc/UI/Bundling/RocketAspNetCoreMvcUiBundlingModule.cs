using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap;
using Aiwins.Rocket.Minify;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling
{
    [DependsOn(typeof(RocketAspNetCoreMvcUiBootstrapModule), typeof(RocketMinifyModule))]
    public class RocketAspNetCoreMvcUiBundlingModule : RocketModule
    {

    }
}
