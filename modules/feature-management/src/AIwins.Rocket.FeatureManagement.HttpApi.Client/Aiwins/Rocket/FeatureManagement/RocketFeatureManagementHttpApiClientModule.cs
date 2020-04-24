using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.Http.Client;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.FeatureManagement
{
    [DependsOn(
        typeof(RocketFeatureManagementApplicationContractsModule),
        typeof(RocketHttpClientModule))]
    public class RocketFeatureManagementHttpApiClientModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(RocketFeatureManagementApplicationContractsModule).Assembly,
                FeatureManagementRemoteServiceConsts.RemoteServiceName
            );
        }
    }
}
