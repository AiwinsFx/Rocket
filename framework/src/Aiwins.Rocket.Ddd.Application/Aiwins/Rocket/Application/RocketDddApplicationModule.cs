using System.Collections.Generic;
using Aiwins.Rocket.Application.Services;
using Aiwins.Rocket.Authorization;
using Aiwins.Rocket.Domain;
using Aiwins.Rocket.Features;
using Aiwins.Rocket.Http;
using Aiwins.Rocket.Http.Modeling;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.ObjectMapping;
using Aiwins.Rocket.Security;
using Aiwins.Rocket.Settings;
using Aiwins.Rocket.Uow;
using Aiwins.Rocket.Validation;

namespace Aiwins.Rocket.Application
{
    [DependsOn(
        typeof(RocketDddDomainModule),
        typeof(RocketDddApplicationContractsModule),
        typeof(RocketSecurityModule),
        typeof(RocketObjectMappingModule),
        typeof(RocketValidationModule),
        typeof(RocketAuthorizationModule),
        typeof(RocketHttpAbstractionsModule),
        typeof(RocketSettingsModule),
        typeof(RocketFeaturesModule)
        )]
    public class RocketDddApplicationModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketApiDescriptionModelOptions>(options =>
            {
                //TODO: Should we move related items to their own projects?
                options.IgnoredInterfaces.AddIfNotContains(typeof(IRemoteService));
                options.IgnoredInterfaces.AddIfNotContains(typeof(IApplicationService));
                options.IgnoredInterfaces.AddIfNotContains(typeof(IUnitOfWorkEnabled));
            });
        }
    }
}
