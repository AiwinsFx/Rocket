using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.EntityFrameworkCore.SqlServer {
    [DependsOn (
        typeof (RocketEntityFrameworkCoreModule)
    )]
    public class RocketEntityFrameworkCoreSqlServerModule : RocketModule {

    }
}