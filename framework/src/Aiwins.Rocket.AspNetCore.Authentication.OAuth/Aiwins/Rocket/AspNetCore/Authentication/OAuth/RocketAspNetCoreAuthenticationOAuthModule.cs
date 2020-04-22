using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Security;

namespace Aiwins.Rocket.AspNetCore.Authentication.OAuth
{
    [DependsOn(typeof(RocketSecurityModule))]
    public class RocketAspNetCoreAuthenticationOAuthModule : RocketModule
    {

    }
}
