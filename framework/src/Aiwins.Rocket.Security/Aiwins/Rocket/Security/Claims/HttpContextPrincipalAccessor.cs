using System.Security.Claims;
using System.Threading;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.Security.Claims {
    public class HttpContextPrincipalAccessor : ICurrentPrincipalAccessor, ISingletonDependency {
        public virtual ClaimsPrincipal Principal { get; }

        public HttpContextPrincipalAccessor (IHttpContextAccessor httpContextAccessor) {
            Principal = httpContextAccessor.HttpContext?.User;
        }
    }
}