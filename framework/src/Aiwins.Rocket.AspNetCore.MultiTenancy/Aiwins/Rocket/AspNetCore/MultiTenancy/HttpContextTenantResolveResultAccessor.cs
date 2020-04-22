using Microsoft.AspNetCore.Http;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.MultiTenancy;

namespace Aiwins.Rocket.AspNetCore.MultiTenancy
{
    [Dependency(ReplaceServices = true)]
    public class HttpContextTenantResolveResultAccessor : ITenantResolveResultAccessor, ITransientDependency
    {
        public const string HttpContextItemName = "__RocketTenantResolveResult";

        public TenantResolveResult Result
        {
            get => _httpContextAccessor.HttpContext?.Items[HttpContextItemName] as TenantResolveResult;
            set
            {
                if (_httpContextAccessor.HttpContext == null)
                {
                    return;
                }

                _httpContextAccessor.HttpContext.Items[HttpContextItemName] = value;
            }
        }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextTenantResolveResultAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}