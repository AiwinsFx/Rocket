using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.EntityFrameworkCore.PostgreSql {
    [DependsOn (
        typeof (RocketEntityFrameworkCoreModule)
    )]
    public class RocketEntityFrameworkCorePostgreSqlModule : RocketModule {

    }
}