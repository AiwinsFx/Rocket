using System.Threading.Tasks;
using Aiwins.Rocket.Caching;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Domain.Entities.Events;
using Aiwins.Rocket.EventBus.Local;

namespace Aiwins.Rocket.SettingManagement {
    public class SettingCacheItemInvalidator : ILocalEventHandler<EntityChangedEventData<Setting>>, ITransientDependency {
        protected IDistributedCache<SettingCacheItem> Cache { get; }

        public SettingCacheItemInvalidator (IDistributedCache<SettingCacheItem> cache) {
            Cache = cache;
        }

        public virtual async Task HandleEventAsync (EntityChangedEventData<Setting> eventData) {
            var cacheKey = CalculateCacheKey (
                eventData.Entity.Name,
                eventData.Entity.ProviderName,
                eventData.Entity.ProviderKey
            );

            await Cache.RemoveAsync (cacheKey);
        }

        protected virtual string CalculateCacheKey (string name, string providerName, string providerKey) {
            return SettingCacheItem.CalculateCacheKey (name, providerName, providerKey);
        }
    }
}