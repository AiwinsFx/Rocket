using System.Collections.Generic;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Features {
    public interface IFeatureDefinitionManager {
        [NotNull]
        FeatureDefinition Get ([NotNull] string name);

        IReadOnlyList<FeatureDefinition> GetAll ();

        FeatureDefinition GetOrNull (string name);
    }
}