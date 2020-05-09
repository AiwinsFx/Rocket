using System.Collections.Generic;

namespace Aiwins.Rocket.TextTemplating {
    public interface ITemplateDefinitionContext {
        IReadOnlyList<TemplateDefinition> GetAll (string name);

        TemplateDefinition GetOrNull (string name);

        void Add (params TemplateDefinition[] definitions);
    }
}