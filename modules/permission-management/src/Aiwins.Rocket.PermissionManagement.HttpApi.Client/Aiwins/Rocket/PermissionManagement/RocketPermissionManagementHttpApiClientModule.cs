using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.Http.Client;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.PermissionManagement
{
    [DependsOn(
        typeof(RocketPermissionManagementApplicationContractsModule),
        typeof(RocketHttpClientModule))]
    public class RocketPermissionManagementHttpApiClientModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(RocketPermissionManagementApplicationContractsModule).Assembly,
                PermissionManagementRemoteServiceConsts.RemoteServiceName
            );
        }
    }
}
