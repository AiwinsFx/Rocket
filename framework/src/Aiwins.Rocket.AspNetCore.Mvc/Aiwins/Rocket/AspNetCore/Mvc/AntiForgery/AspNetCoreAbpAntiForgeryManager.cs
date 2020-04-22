using Aiwins.Rocket.DependencyInjection;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.AspNetCore.Mvc.AntiForgery {
    public class AspNetCoreRocketAntiForgeryManager : IRocketAntiForgeryManager, ITransientDependency {
        public RocketAntiForgeryOptions Options { get; }

        public HttpContext HttpContext => _httpContextAccessor.HttpContext;

        private readonly IAntiforgery _antiforgery;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AspNetCoreRocketAntiForgeryManager (
            IAntiforgery antiforgery,
            IHttpContextAccessor httpContextAccessor,
            IOptions<RocketAntiForgeryOptions> options) {
            _antiforgery = antiforgery;
            _httpContextAccessor = httpContextAccessor;
            Options = options.Value;
        }

        public void SetCookie () {
            HttpContext.Response.Cookies.Append (Options.TokenCookieName, GenerateToken ());
        }

        public string GenerateToken () {
            return _antiforgery.GetAndStoreTokens (_httpContextAccessor.HttpContext).RequestToken;
        }
    }
}