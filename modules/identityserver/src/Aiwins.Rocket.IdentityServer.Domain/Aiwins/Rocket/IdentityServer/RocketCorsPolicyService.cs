using System;
using System.Linq;
using System.Threading.Tasks;
using Aiwins.Rocket.Caching;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.IdentityServer.Clients;
using IdentityServer4.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Aiwins.Rocket.IdentityServer {
    public class RocketCorsPolicyService : ICorsPolicyService {
        public ILogger<RocketCorsPolicyService> Logger { get; set; }
        protected IHybridServiceScopeFactory HybridServiceScopeFactory { get; }
        protected IDistributedCache<AllowedCorsOriginsCacheItem> Cache { get; }

        public RocketCorsPolicyService (
            IDistributedCache<AllowedCorsOriginsCacheItem> cache,
            IHybridServiceScopeFactory hybridServiceScopeFactory) {
            Cache = cache;
            HybridServiceScopeFactory = hybridServiceScopeFactory;
            Logger = NullLogger<RocketCorsPolicyService>.Instance;
        }

        public virtual async Task<bool> IsOriginAllowedAsync (string origin) {
            var cacheItem = await Cache.GetOrAddAsync (AllowedCorsOriginsCacheItem.AllOrigins, CreateCacheItemAsync);

            var isAllowed = cacheItem.AllowedOrigins.Contains (origin, StringComparer.OrdinalIgnoreCase);

            if (!isAllowed) {
                Logger.LogWarning ($"Origin is not allowed: {origin}");
            }

            return isAllowed;
        }

        protected virtual async Task<AllowedCorsOriginsCacheItem> CreateCacheItemAsync () {
            // doing this here and not in the ctor because: https://github.com/aspnet/AspNetCore/issues/2377
            using (var scope = HybridServiceScopeFactory.CreateScope ()) {
                var clientRepository = scope.ServiceProvider.GetRequiredService<IClientRepository> ();

                return new AllowedCorsOriginsCacheItem {
                    AllowedOrigins = (await clientRepository.GetAllDistinctAllowedCorsOriginsAsync ()).ToArray ()
                };
            }
        }
    }
}