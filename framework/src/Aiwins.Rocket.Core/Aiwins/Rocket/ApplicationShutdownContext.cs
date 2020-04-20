using System;
using JetBrains.Annotations;

namespace Aiwins.Rocket {
    public class ApplicationShutdownContext {
        public IServiceProvider ServiceProvider { get; }

        public ApplicationShutdownContext ([NotNull] IServiceProvider serviceProvider) {
            Check.NotNull (serviceProvider, nameof (serviceProvider));

            ServiceProvider = serviceProvider;
        }
    }
}