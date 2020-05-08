using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.MongoDB;

namespace Aiwins.Rocket.Users.MongoDB {
    [DependsOn (
        typeof (RocketUsersDomainModule),
        typeof (RocketMongoDbModule)
    )]
    public class RocketUsersMongoDbModule : RocketModule {

    }
}