using Aiwins.Rocket.Collections;

namespace Aiwins.Rocket.TextTemplating {
    public class RocketTextTemplatingOptions {
        public ITypeList<ITemplateDefinitionProvider> DefinitionProviders { get; }
        public ITypeList<ITemplateContentContributor> ContentContributors { get; }

        public RocketTextTemplatingOptions () {
            DefinitionProviders = new TypeList<ITemplateDefinitionProvider> ();
            ContentContributors = new TypeList<ITemplateContentContributor> ();
        }
    }
}