using System;
using Aiwins.Rocket.Json;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.MultiTenancy;
using Aiwins.Rocket.Serialization;
using Aiwins.Rocket.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.Caching {
    [DependsOn (
        typeof (RocketThreadingModule),
        typeof (RocketSerializationModule),
        typeof (RocketMultiTenancyModule),
        typeof (RocketJsonModule))]
    public class RocketCachingModule : RocketModule {
        public override void ConfigureServices (ServiceConfigurationContext context) {
            context.Services.AddMemoryCache ();
            context.Services.AddDistributedMemoryCache ();

            context.Services.AddSingleton (typeof (IDistributedCache<>), typeof (DistributedCache<>));
            context.Services.AddSingleton (typeof (IDistributedCache<,>), typeof (DistributedCache<,>));

            context.Services.Configure<RocketDistributedCacheOptions> (cacheOptions => {
                cacheOptions.GlobalCacheEntryOptions.SlidingExpiration = TimeSpan.FromMinutes (20);
            });
        }
    }
}