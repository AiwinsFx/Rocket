using Aiwins.Rocket.Modularity.PlugIns;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket {
    public class RocketApplicationCreationOptions {
        [NotNull]
        public IServiceCollection Services { get; }

        [NotNull]
        public PlugInSourceList PlugInSources { get; }

        [NotNull]
        public RocketConfigurationBuilderOptions Configuration { get; }

        public RocketApplicationCreationOptions ([NotNull] IServiceCollection services) {
            Services = Check.NotNull (services, nameof (services));
            PlugInSources = new PlugInSourceList ();
            Configuration = new RocketConfigurationBuilderOptions ();
        }
    }
}