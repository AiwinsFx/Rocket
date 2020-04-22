using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Tracing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.AspNetCore.Tracing {
    public class RocketCorrelationIdMiddleware : IMiddleware, ITransientDependency {
        private readonly RocketCorrelationIdOptions _options;
        private readonly ICorrelationIdProvider _correlationIdProvider;

        public RocketCorrelationIdMiddleware (IOptions<RocketCorrelationIdOptions> options,
            ICorrelationIdProvider correlationIdProvider) {
            _options = options.Value;
            _correlationIdProvider = correlationIdProvider;
        }

        public async Task InvokeAsync (HttpContext context, RequestDelegate next) {
            var correlationId = _correlationIdProvider.Get ();

            try {
                await next (context);
            } finally {
                CheckAndSetCorrelationIdOnResponse (context, _options, correlationId);
            }
        }

        protected virtual void CheckAndSetCorrelationIdOnResponse (
            HttpContext httpContext,
            RocketCorrelationIdOptions options,
            string correlationId) {
            if (httpContext.Response.HasStarted) {
                return;
            }

            if (!options.SetResponseHeader) {
                return;
            }

            if (httpContext.Response.Headers.ContainsKey (options.HttpHeaderName)) {
                return;
            }

            httpContext.Response.Headers[options.HttpHeaderName] = correlationId;
        }
    }
}