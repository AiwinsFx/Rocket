using Aiwins.Rocket.Emailing.Templates.VirtualFiles;

namespace Aiwins.Rocket.Emailing.Templates {
    public class DefaultEmailTemplateProvider : EmailTemplateDefinitionProvider {
        public override void Define (IEmailTemplateDefinitionContext context) {
            context.Add (new EmailTemplateDefinition (StandardEmailTemplates.DefaultLayout, defaultCultureName: "en", isLayout : true, layout : null)
                .AddTemplateVirtualFiles ("/Aiwins/Rocket/Emailing/Templates/DefaultEmailTemplates/Layout"));

            context.Add (new EmailTemplateDefinition (StandardEmailTemplates.SimpleMessage, defaultCultureName: "en")
                .AddTemplateVirtualFiles ("/Aiwins/Rocket/Emailing/Templates/DefaultEmailTemplates/Message"));
        }
    }
}