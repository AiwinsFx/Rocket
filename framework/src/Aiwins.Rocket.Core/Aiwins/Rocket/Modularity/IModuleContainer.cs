using System.Collections.Generic;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Modularity {
    public interface IModuleContainer {
        [NotNull]
        IReadOnlyList<IRocketModuleDescriptor> Modules { get; }
    }
}