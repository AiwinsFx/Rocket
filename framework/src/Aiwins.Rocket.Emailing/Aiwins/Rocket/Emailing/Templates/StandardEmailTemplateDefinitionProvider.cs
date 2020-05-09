using Aiwins.Rocket.Emailing.Localization;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.TextTemplating;

namespace Aiwins.Rocket.Emailing.Templates
{
    public class StandardEmailTemplateDefinitionProvider : TemplateDefinitionProvider
    {
        public override void Define(ITemplateDefinitionContext context)
        {
            context.Add(
                new TemplateDefinition(
                    StandardEmailTemplates.Layout,
                    displayName: LocalizableString.Create<EmailingResource>("TextTemplate:StandardEmailTemplates.Layout"),
                    isLayout: true
                ).WithVirtualFilePath("/Aiwins/Rocket/Emailing/Templates/Layout.tpl", true)
            );

            context.Add(
                new TemplateDefinition(
                    StandardEmailTemplates.Message,
                    displayName: LocalizableString.Create<EmailingResource>("TextTemplate:StandardEmailTemplates.Message"),
                    layout: StandardEmailTemplates.Layout
                ).WithVirtualFilePath("/Aiwins/Rocket/Emailing/Templates/Message.tpl", true)
            );
        }
    }
}