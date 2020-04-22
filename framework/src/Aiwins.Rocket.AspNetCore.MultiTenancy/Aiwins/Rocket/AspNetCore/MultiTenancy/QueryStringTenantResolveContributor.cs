using Microsoft.AspNetCore.Http;
using Aiwins.Rocket.MultiTenancy;

namespace Aiwins.Rocket.AspNetCore.MultiTenancy
{
    public class QueryStringTenantResolveContributor : HttpTenantResolveContributorBase
    {
        public const string ContributorName = "QueryString";

        public override string Name => ContributorName;

        protected override string GetTenantIdOrNameFromHttpContextOrNull(ITenantResolveContext context, HttpContext httpContext)
        {
            if (httpContext.Request == null || !httpContext.Request.QueryString.HasValue)
            {
                return null;
            }

            return httpContext.Request.Query[context.GetRocketAspNetCoreMultiTenancyOptions().TenantKey];
        }
    }
}
