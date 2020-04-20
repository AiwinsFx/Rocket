using System;
using System.Linq;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Modularity.PlugIns {
    public static class PlugInSourceExtensions {
        [NotNull]
        public static Type[] GetModulesWithAllDependencies ([NotNull] this IPlugInSource plugInSource) {
            Check.NotNull (plugInSource, nameof (plugInSource));

            return plugInSource
                .GetModules ()
                .SelectMany (RocketModuleHelper.FindAllModuleTypes)
                .Distinct ()
                .ToArray ();
        }
    }
}