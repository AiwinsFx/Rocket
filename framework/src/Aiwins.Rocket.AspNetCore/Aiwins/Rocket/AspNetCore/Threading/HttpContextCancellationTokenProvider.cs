using System.Threading;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Threading;
using Microsoft.AspNetCore.Http;

namespace Aiwins.Rocket.AspNetCore.Threading {
    [Dependency (ReplaceServices = true)]
    public class HttpContextCancellationTokenProvider : ICancellationTokenProvider, ITransientDependency {
        public CancellationToken Token => _httpContextAccessor.HttpContext?.RequestAborted ?? CancellationToken.None;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextCancellationTokenProvider (IHttpContextAccessor httpContextAccessor) {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}