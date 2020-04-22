using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Aiwins.Rocket.MultiTenancy;

namespace Aiwins.Rocket.AspNetCore.MultiTenancy
{
    public static class TenantResolveContextExtensions
    {
        public static RocketAspNetCoreMultiTenancyOptions GetRocketAspNetCoreMultiTenancyOptions(this ITenantResolveContext context)
        {
            return context.ServiceProvider.GetRequiredService<IOptionsSnapshot<RocketAspNetCoreMultiTenancyOptions>>().Value;
        }
    }
}