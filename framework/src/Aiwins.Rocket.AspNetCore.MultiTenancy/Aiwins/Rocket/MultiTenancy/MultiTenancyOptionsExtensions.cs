using System.Collections.Generic;
using Aiwins.Rocket.AspNetCore.MultiTenancy;

namespace Aiwins.Rocket.MultiTenancy
{
    public static class RocketMultiTenancyOptionsExtensions
    {
        public static void AddDomainTenantResolver(this RocketTenantResolveOptions options, string domainFormat)
        {
            options.TenantResolvers.InsertAfter(
                r => r is CurrentUserTenantResolveContributor,
                new DomainTenantResolveContributor(domainFormat)
            );
        }
    }
}