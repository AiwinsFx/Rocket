using Aiwins.Rocket.Http.Client;
using Aiwins.Rocket.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.TenantManagement {
    [DependsOn (
        typeof (RocketTenantManagementApplicationContractsModule),
        typeof (RocketHttpClientModule))]
    public class RocketTenantManagementHttpApiClientModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            context.Services.AddHttpClientProxies (
                typeof (RocketTenantManagementApplicationContractsModule).Assembly,
                TenantManagementRemoteServiceConsts.RemoteServiceName
            );
        }
    }
}