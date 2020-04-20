using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.Localization {
    public class RocketStringLocalizerFactory : IStringLocalizerFactory {
        private readonly ResourceManagerStringLocalizerFactory _innerFactory;
        private readonly RocketLocalizationOptions _abpLocalizationOptions;
        private readonly IServiceProvider _serviceProvider;

        private readonly ConcurrentDictionary<Type, StringLocalizerCacheItem> _localizerCache;

        //TODO: 考虑通过装饰器模式实现，而非ResourceManagerStringLocalizerFactory。
        public RocketStringLocalizerFactory (
            ResourceManagerStringLocalizerFactory innerFactory,
            IOptions<RocketLocalizationOptions> abpLocalizationOptions,
            IServiceProvider serviceProvider) {
            _innerFactory = innerFactory;
            _serviceProvider = serviceProvider;
            _abpLocalizationOptions = abpLocalizationOptions.Value;

            _localizerCache = new ConcurrentDictionary<Type, StringLocalizerCacheItem> ();
        }

        public virtual IStringLocalizer Create (Type resourceType) {
            var resource = _abpLocalizationOptions.Resources.GetOrDefault (resourceType);
            if (resource == null) {
                return _innerFactory.Create (resourceType);
            }

            if (_localizerCache.TryGetValue (resourceType, out var cacheItem)) {
                return cacheItem.Localizer;
            }

            lock (_localizerCache) {
                return _localizerCache.GetOrAdd (
                    resourceType,
                    _ => CreateStringLocalizerCacheItem (resource)
                ).Localizer;
            }
        }

        private StringLocalizerCacheItem CreateStringLocalizerCacheItem (LocalizationResource resource) {
            foreach (var globalContributor in _abpLocalizationOptions.GlobalContributors) {
                resource.Contributors.Add ((ILocalizationResourceContributor) Activator.CreateInstance (globalContributor));
            }

            using (var scope = _serviceProvider.CreateScope ()) {
                var context = new LocalizationResourceInitializationContext (resource, scope.ServiceProvider);

                foreach (var contributor in resource.Contributors) {
                    contributor.Initialize (context);
                }
            }

            return new StringLocalizerCacheItem (
                new RocketDictionaryBasedStringLocalizer (
                    resource,
                    resource.BaseResourceTypes.Select (Create).ToList ()
                )
            );
        }

        public virtual IStringLocalizer Create (string baseName, string location) {
            //TODO: 跟踪下何时调用?

            return _innerFactory.Create (baseName, location);
        }

        internal static void Replace (IServiceCollection services) {
            services.Replace (ServiceDescriptor.Singleton<IStringLocalizerFactory, RocketStringLocalizerFactory> ());
            services.AddSingleton<ResourceManagerStringLocalizerFactory> ();
        }

        private class StringLocalizerCacheItem {
            public RocketDictionaryBasedStringLocalizer Localizer { get; }

            public StringLocalizerCacheItem (RocketDictionaryBasedStringLocalizer localizer) {
                Localizer = localizer;
            }
        }
    }
}