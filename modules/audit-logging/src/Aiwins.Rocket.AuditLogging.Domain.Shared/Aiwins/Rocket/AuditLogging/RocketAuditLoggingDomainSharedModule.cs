using Aiwins.Rocket.AuditLogging.Localization;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.AuditLogging
{
    public class RocketAuditLoggingDomainSharedModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketLocalizationOptions>(options =>
            {
                options.Resources.Add<AuditLoggingResource>("en");
            });
        }
    }
}
