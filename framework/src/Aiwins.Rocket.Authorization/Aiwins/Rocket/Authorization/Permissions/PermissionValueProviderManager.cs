using System;
using System.Collections.Generic;
using System.Linq;
using Aiwins.Rocket.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.Authorization.Permissions {
    public class PermissionValueProviderManager : IPermissionValueProviderManager, ISingletonDependency {
        public IReadOnlyList<IPermissionValueProvider> ValueProviders => _lazyProviders.Value;
        private readonly Lazy<List<IPermissionValueProvider>> _lazyProviders;

        protected RocketPermissionOptions Options { get; }

        public PermissionValueProviderManager (
            IServiceProvider serviceProvider,
            IOptions<RocketPermissionOptions> options) {
            Options = options.Value;

            _lazyProviders = new Lazy<List<IPermissionValueProvider>> (
                () => Options
                .ValueProviders
                .Select (c => serviceProvider.GetRequiredService (c) as IPermissionValueProvider)
                .ToList (),
                true
            );
        }
    }
}