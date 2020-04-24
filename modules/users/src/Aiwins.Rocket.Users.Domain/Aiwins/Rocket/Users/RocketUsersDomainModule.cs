using Aiwins.Rocket.Domain;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.Users
{
    [DependsOn(
        typeof(RocketUsersDomainSharedModule),
        typeof(RocketUsersAbstractionModule),
        typeof(RocketDddDomainModule)
        )]
    public class RocketUsersDomainModule : RocketModule
    {
        
    }
}
