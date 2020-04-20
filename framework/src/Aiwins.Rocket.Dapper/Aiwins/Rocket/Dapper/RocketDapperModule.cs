using Aiwins.Rocket.Domain;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.Dapper {
    [DependsOn (
        typeof (RocketDddDomainModule),
        typeof (RocketEntityFrameworkCoreModule))]
    public class RocketDapperModule : RocketModule { }
}