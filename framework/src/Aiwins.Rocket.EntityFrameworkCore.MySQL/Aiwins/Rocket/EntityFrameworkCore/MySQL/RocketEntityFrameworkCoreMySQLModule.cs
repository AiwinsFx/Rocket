using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.EntityFrameworkCore.MySQL {
    [DependsOn (
        typeof (RocketEntityFrameworkCoreModule)
    )]
    public class RocketEntityFrameworkCoreMySQLModule : RocketModule {

    }
}