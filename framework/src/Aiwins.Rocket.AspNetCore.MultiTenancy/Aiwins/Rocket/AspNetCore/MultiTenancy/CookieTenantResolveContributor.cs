using Microsoft.AspNetCore.Http;
using Aiwins.Rocket.MultiTenancy;

namespace Aiwins.Rocket.AspNetCore.MultiTenancy
{
    public class CookieTenantResolveContributor : HttpTenantResolveContributorBase
    {
        public const string ContributorName = "Cookie";

        public override string Name => ContributorName;

        protected override string GetTenantIdOrNameFromHttpContextOrNull(ITenantResolveContext context, HttpContext httpContext)
        {
            return httpContext.Request?.Cookies[context.GetRocketAspNetCoreMultiTenancyOptions().TenantKey];
        }
    }
}