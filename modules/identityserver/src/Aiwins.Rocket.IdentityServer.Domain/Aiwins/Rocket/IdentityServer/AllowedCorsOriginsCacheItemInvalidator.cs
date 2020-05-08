using System.Threading.Tasks;
using Aiwins.Rocket.Caching;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Domain.Entities.Events;
using Aiwins.Rocket.EventBus.Local;
using Aiwins.Rocket.IdentityServer.Clients;

namespace Aiwins.Rocket.IdentityServer {
    public class AllowedCorsOriginsCacheItemInvalidator:
        ILocalEventHandler<EntityChangedEventData<Client>>,
        ILocalEventHandler<EntityChangedEventData<ClientCorsOrigin>>,
        ITransientDependency {
            protected IDistributedCache<AllowedCorsOriginsCacheItem> Cache { get; }

            public AllowedCorsOriginsCacheItemInvalidator (IDistributedCache<AllowedCorsOriginsCacheItem> cache) {
                Cache = cache;
            }

            public virtual async Task HandleEventAsync (EntityChangedEventData<Client> eventData) {
                await Cache.RemoveAsync (AllowedCorsOriginsCacheItem.AllOrigins);
            }

            public virtual async Task HandleEventAsync (EntityChangedEventData<ClientCorsOrigin> eventData) {
                await Cache.RemoveAsync (AllowedCorsOriginsCacheItem.AllOrigins);
            }
        }
}