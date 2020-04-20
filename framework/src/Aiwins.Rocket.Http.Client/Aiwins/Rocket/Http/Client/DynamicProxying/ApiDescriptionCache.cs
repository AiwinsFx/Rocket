﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Http.Modeling;
using Aiwins.Rocket.Threading;
using Nito.AsyncEx;

namespace Aiwins.Rocket.Http.Client.DynamicProxying {
    public class ApiDescriptionCache : IApiDescriptionCache, ISingletonDependency {
        protected ICancellationTokenProvider CancellationTokenProvider { get; }

        private readonly Dictionary<string, ApplicationApiDescriptionModel> _cache;
        private readonly SemaphoreSlim _semaphore;

        public ApiDescriptionCache (ICancellationTokenProvider cancellationTokenProvider) {
            CancellationTokenProvider = cancellationTokenProvider;
            _cache = new Dictionary<string, ApplicationApiDescriptionModel> ();
            _semaphore = new SemaphoreSlim (1, 1);
        }

        public async Task<ApplicationApiDescriptionModel> GetAsync (
            string baseUrl,
            Func<Task<ApplicationApiDescriptionModel>> factory) {
            using (await _semaphore.LockAsync (CancellationTokenProvider.Token)) {
                var model = _cache.GetOrDefault (baseUrl);
                if (model == null) {
                    _cache[baseUrl] = model = await factory ();
                }

                return model;
            }
        }
    }
}