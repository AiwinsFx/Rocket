using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.MultiTenancy;

namespace Aiwins.Rocket.AspNetCore.MultiTenancy
{
    [DependsOn(
        typeof(RocketMultiTenancyModule), 
        typeof(RocketAspNetCoreModule)
        )]
    public class RocketAspNetCoreMultiTenancyModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketTenantResolveOptions>(options =>
            {
                options.TenantResolvers.Add(new QueryStringTenantResolveContributor());
                options.TenantResolvers.Add(new RouteTenantResolveContributor());
                options.TenantResolvers.Add(new HeaderTenantResolveContributor());
                options.TenantResolvers.Add(new CookieTenantResolveContributor());
            });
        }
    }
}
