using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Security;

namespace Aiwins.Rocket.AspNetCore.Authentication.JwtBearer
{
    [DependsOn(typeof(RocketSecurityModule))]
    public class RocketAspNetCoreAuthenticationJwtBearerModule : RocketModule
    {

    }
}
