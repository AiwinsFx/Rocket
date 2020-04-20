using Aiwins.Rocket.Data;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.MultiTenancy.ConfigurationStore;
using Aiwins.Rocket.Security;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.MultiTenancy {
    //TODO: 考虑创建一个 Aiwins.Rocket.MultiTenancy.Abstractions 项目?

    [DependsOn (
        typeof (RocketDataModule),
        typeof (RocketSecurityModule)
    )]
    public class RocketMultiTenancyModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            var configuration = context.Services.GetConfiguration ();
            Configure<RocketDefaultTenantStoreOptions> (configuration);
        }
    }
}