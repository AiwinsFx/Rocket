using Aiwins.Rocket.MultiTenancy;

namespace Aiwins.Rocket.AspNetCore.MultiTenancy
{
    public class RocketAspNetCoreMultiTenancyOptions
    {
        /// <summary>
        /// Default: <see cref="TenantResolverConsts.DefaultTenantKey"/>.
        /// </summary>
        public string TenantKey { get; set; }

        public RocketAspNetCoreMultiTenancyOptions()
        {
            TenantKey = TenantResolverConsts.DefaultTenantKey;
        }
    }
}