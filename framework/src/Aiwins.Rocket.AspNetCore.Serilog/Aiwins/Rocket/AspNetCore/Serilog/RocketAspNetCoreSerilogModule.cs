using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.MultiTenancy;

namespace Aiwins.Rocket.AspNetCore.Serilog {
    [DependsOn (
        typeof (RocketMultiTenancyModule),
        typeof (RocketAspNetCoreModule)
    )]
    public class RocketAspNetCoreSerilogModule : RocketModule { }
}