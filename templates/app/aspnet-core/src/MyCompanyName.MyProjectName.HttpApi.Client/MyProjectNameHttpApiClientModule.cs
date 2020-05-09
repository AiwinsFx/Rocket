using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.Account;
using Aiwins.Rocket.FeatureManagement;
using Aiwins.Rocket.Identity;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.PermissionManagement;
using Aiwins.Rocket.TenantManagement;

namespace MyCompanyName.MyProjectName
{
    [DependsOn(
        typeof(MyProjectNameApplicationContractsModule),
        typeof(RocketAccountHttpApiClientModule),
        typeof(RocketIdentityHttpApiClientModule),
        typeof(RocketPermissionManagementHttpApiClientModule),
        typeof(RocketTenantManagementHttpApiClientModule),
        typeof(RocketFeatureManagementHttpApiClientModule)
    )]
    public class MyProjectNameHttpApiClientModule : RocketModule
    {
        public const string RemoteServiceName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(MyProjectNameApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
