using Aiwins.Rocket.Authorization;
using Aiwins.Rocket.Caching;
using Aiwins.Rocket.Domain;
using Aiwins.Rocket.Json;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.PermissionManagement {
    [DependsOn (typeof (RocketAuthorizationModule))]
    [DependsOn (typeof (RocketDddDomainModule))]
    [DependsOn (typeof (RocketPermissionManagementDomainSharedModule))]
    [DependsOn (typeof (RocketCachingModule))]
    [DependsOn (typeof (RocketJsonModule))]
    public class RocketPermissionManagementDomainModule : RocketModule {

    }
}