using System;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Localization {
    public interface IInheritedResourceTypesProvider {
        [NotNull]
        Type[] GetInheritedResourceTypes ();
    }
}