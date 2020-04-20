using System;
using System.Collections.Generic;
using System.Linq;
using Aiwins.Rocket.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Aiwins.Rocket.Settings {
    public class SettingValueProviderManager : ISettingValueProviderManager, ISingletonDependency {
        public List<ISettingValueProvider> Providers => _lazyProviders.Value;
        protected RocketSettingOptions Options { get; }
        private readonly Lazy<List<ISettingValueProvider>> _lazyProviders;

        public SettingValueProviderManager (
            IServiceProvider serviceProvider,
            IOptions<RocketSettingOptions> options) {

            Options = options.Value;

            _lazyProviders = new Lazy<List<ISettingValueProvider>> (
                () => Options
                .ValueProviders
                .Select (type => serviceProvider.GetRequiredService (type) as ISettingValueProvider)
                .ToList (),
                true
            );
        }
    }
}