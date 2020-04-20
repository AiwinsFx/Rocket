using System;
using Aiwins.Rocket.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Aiwins.Rocket.Localization {
    public class LocalizationContext : IServiceProviderAccessor {
        public IServiceProvider ServiceProvider { get; }

        public IStringLocalizerFactory LocalizerFactory { get; }

        public LocalizationContext (IServiceProvider serviceProvider) {
            ServiceProvider = serviceProvider;
            LocalizerFactory = ServiceProvider.GetRequiredService<IStringLocalizerFactory> ();
        }
    }
}