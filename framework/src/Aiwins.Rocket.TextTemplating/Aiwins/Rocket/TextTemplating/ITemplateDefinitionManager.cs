using System.Collections.Generic;
using JetBrains.Annotations;

namespace Aiwins.Rocket.TextTemplating {
    public interface ITemplateDefinitionManager {
        [NotNull]
        TemplateDefinition Get ([NotNull] string name);

        [NotNull]
        IReadOnlyList<TemplateDefinition> GetAll ();

        [CanBeNull]
        TemplateDefinition GetOrNull (string name);
    }
}