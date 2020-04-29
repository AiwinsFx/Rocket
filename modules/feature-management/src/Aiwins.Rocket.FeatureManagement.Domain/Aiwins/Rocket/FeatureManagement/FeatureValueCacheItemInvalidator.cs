using System.Threading.Tasks;
using Aiwins.Rocket.Caching;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Domain.Entities.Events;
using Aiwins.Rocket.EventBus.Local;

namespace Aiwins.Rocket.FeatureManagement {
    public class FeatureValueCacheItemInvalidator:
        ILocalEventHandler<EntityChangedEventData<FeatureValue>>,
        ITransientDependency {
            protected IDistributedCache<FeatureValueCacheItem> Cache { get; }

            public FeatureValueCacheItemInvalidator (IDistributedCache<FeatureValueCacheItem> cache) {
                Cache = cache;
            }

            public virtual async Task HandleEventAsync (EntityChangedEventData<FeatureValue> eventData) {
                var cacheKey = CalculateCacheKey (
                    eventData.Entity.Name,
                    eventData.Entity.ProviderName,
                    eventData.Entity.ProviderKey
                );

                await Cache.RemoveAsync (cacheKey);
            }

            protected virtual string CalculateCacheKey (string name, string providerName, string providerKey) {
                return FeatureValueCacheItem.CalculateCacheKey (name, providerName, providerKey);
            }
        }
}