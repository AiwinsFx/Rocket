using System;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Modularity.PlugIns {
    public interface IPlugInSource {
        [NotNull]
        Type[] GetModules ();
    }
}