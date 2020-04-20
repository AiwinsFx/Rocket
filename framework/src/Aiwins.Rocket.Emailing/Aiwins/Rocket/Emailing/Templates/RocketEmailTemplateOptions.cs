using Aiwins.Rocket.Collections;

namespace Aiwins.Rocket.Emailing.Templates {
    public class RocketEmailTemplateOptions {
        public string DefaultLayout { get; set; }

        public ITypeList<IEmailTemplateDefinitionProvider> DefinitionProviders { get; }

        public RocketEmailTemplateOptions () {
            DefaultLayout = StandardEmailTemplates.DefaultLayout;

            DefinitionProviders = new TypeList<IEmailTemplateDefinitionProvider> ();
        }
    }
}