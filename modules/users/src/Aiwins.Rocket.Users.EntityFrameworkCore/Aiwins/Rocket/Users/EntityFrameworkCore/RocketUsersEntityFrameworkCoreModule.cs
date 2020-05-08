using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.Users.EntityFrameworkCore {
    [DependsOn (
        typeof (RocketUsersDomainModule),
        typeof (RocketEntityFrameworkCoreModule)
    )]
    public class RocketUsersEntityFrameworkCoreModule : RocketModule {

    }
}