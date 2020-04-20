using Aiwins.Rocket.Authorization;
using Aiwins.Rocket.Domain;
using Aiwins.Rocket.Features;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.ObjectMapping;
using Aiwins.Rocket.Security;
using Aiwins.Rocket.Settings;
using Aiwins.Rocket.Validation;

namespace Aiwins.Rocket.Application {
    [DependsOn (
        typeof (RocketDddDomainModule),
        typeof (RocketDddApplicationContractsModule),
        typeof (RocketSecurityModule),
        typeof (RocketObjectMappingModule),
        typeof (RocketValidationModule),
        typeof (RocketAuthorizationModule),
        // typeof (RocketHttpAbstractionsModule),
        typeof (RocketSettingsModule),
        typeof (RocketFeaturesModule)
    )]
    public class RocketDddApplicationModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            // Configure<RocketApiDescriptionModelOptions> (options => {
            //     //TODO: 考虑迁移到各自的业务模块?
            //     options.IgnoredInterfaces.AddIfNotContains (typeof (IRemoteService));
            //     options.IgnoredInterfaces.AddIfNotContains (typeof (IApplicationService));
            //     options.IgnoredInterfaces.AddIfNotContains (typeof (IUnitOfWorkEnabled));
            // });
        }
    }
}