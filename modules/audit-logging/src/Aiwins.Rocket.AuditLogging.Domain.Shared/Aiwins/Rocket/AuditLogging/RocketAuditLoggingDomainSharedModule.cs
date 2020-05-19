using Aiwins.Rocket.AuditLogging.Localization;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.Users;

namespace Aiwins.Rocket.AuditLogging {
    [DependsOn (
        typeof (RocketUsersDomainSharedModule)
    )]
    public class RocketAuditLoggingDomainSharedModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            Configure<RocketLocalizationOptions> (options => {
                options.Resources.Add<AuditLoggingResource> ("zh-Hans");
            });
        }
    }
}