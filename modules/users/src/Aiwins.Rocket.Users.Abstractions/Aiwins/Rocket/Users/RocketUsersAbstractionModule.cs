using Aiwins.Rocket.EventBus;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.MultiTenancy;

namespace Aiwins.Rocket.Users {
    //TODO: Consider to (somehow) move this to the framework to the same assemblily of ICurrentUser!

    [DependsOn (
        typeof (RocketMultiTenancyModule),
        typeof (RocketEventBusModule)
    )]
    public class RocketUsersAbstractionModule : RocketModule {

    }
}