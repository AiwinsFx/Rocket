using System;
using Aiwins.Rocket.Modularity.PlugIns;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Aiwins.Rocket.Modularity {
    public interface IModuleLoader {
        [NotNull]
        IRocketModuleDescriptor[] LoadModules (
            [NotNull] IServiceCollection services, [NotNull] Type startupModuleType, [NotNull] PlugInSourceList plugInSources
        );
    }
}