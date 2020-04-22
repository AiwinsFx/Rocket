using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Aiwins.Rocket.MultiTenancy;

namespace Aiwins.Rocket.AspNetCore.MultiTenancy
{
    public class RouteTenantResolveContributor : HttpTenantResolveContributorBase
    {
        public const string ContributorName = "Route";

        public override string Name => ContributorName;

        protected override string GetTenantIdOrNameFromHttpContextOrNull(ITenantResolveContext context, HttpContext httpContext)
        {
            var tenantId = httpContext.GetRouteValue(context.GetRocketAspNetCoreMultiTenancyOptions().TenantKey);
            if (tenantId == null)
            {
                return null;
            }

            return Convert.ToString(tenantId);
        }
    }
}