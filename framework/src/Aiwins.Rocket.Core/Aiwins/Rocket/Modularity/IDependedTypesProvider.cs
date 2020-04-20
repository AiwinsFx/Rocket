using System;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Modularity {
    public interface IDependedTypesProvider {
        [NotNull]
        Type[] GetDependedTypes ();
    }
}