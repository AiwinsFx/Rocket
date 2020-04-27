﻿using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.MultiTenancy;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.Caching {
    public class DistributedCacheKeyNormalizer : IDistributedCacheKeyNormalizer, ITransientDependency {
        protected ICurrentTenant CurrentTenant { get; }

        protected RocketDistributedCacheOptions DistributedCacheOptions { get; }

        public DistributedCacheKeyNormalizer (
            ICurrentTenant currentTenant,
            IOptions<RocketDistributedCacheOptions> distributedCacheOptions) {
            CurrentTenant = currentTenant;
            DistributedCacheOptions = distributedCacheOptions.Value;
        }

        public virtual string NormalizeKey (DistributedCacheKeyNormalizeArgs args) {
            var normalizedKey = $"c:{args.CacheName},k:{DistributedCacheOptions.KeyPrefix}{args.Key}";

            if (!args.IgnoreMultiTenancy && CurrentTenant.Id.HasValue) {
                normalizedKey = $"t:{CurrentTenant.Id.Value},{normalizedKey}";
            }

            return normalizedKey;
        }
    }
}