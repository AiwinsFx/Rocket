using Aiwins.Rocket.AspNetCore.MultiTenancy;

namespace Microsoft.AspNetCore.Builder
{
    public static class RocketAspNetCoreMultiTenancyApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMultiTenancy(this IApplicationBuilder app)
        {
            return app
                .UseMiddleware<MultiTenancyMiddleware>();
        }
    }
}
