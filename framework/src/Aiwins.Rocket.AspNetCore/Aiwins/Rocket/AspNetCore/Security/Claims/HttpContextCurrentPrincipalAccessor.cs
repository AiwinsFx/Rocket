using System.Security.Claims;
using Aiwins.Rocket.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Aiwins.Rocket.AspNetCore.Security.Claims {
    public class HttpContextCurrentPrincipalAccessor : ThreadCurrentPrincipalAccessor {
        public override ClaimsPrincipal Principal => _httpContextAccessor.HttpContext?.User ?? base.Principal;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextCurrentPrincipalAccessor (IHttpContextAccessor httpContextAccessor) {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}