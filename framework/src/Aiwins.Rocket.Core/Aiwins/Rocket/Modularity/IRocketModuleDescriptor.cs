using System;
using System.Collections.Generic;
using System.Reflection;

namespace Aiwins.Rocket.Modularity {
    public interface IRocketModuleDescriptor {
        Type Type { get; }

        Assembly Assembly { get; }

        IRocketModule Instance { get; }

        bool IsLoadedAsPlugIn { get; }

        IReadOnlyList<IRocketModuleDescriptor> Dependencies { get; }
    }
}