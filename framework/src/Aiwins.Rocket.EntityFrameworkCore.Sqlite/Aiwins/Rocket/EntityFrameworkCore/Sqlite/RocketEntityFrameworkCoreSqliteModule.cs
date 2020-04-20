using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.EntityFrameworkCore.Sqlite {
    [DependsOn (
        typeof (RocketEntityFrameworkCoreModule)
    )]
    public class RocketEntityFrameworkCoreSqliteModule : RocketModule {

    }
}