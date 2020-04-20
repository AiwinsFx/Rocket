namespace Aiwins.Rocket.Emailing.Templates
{
    public interface IEmailTemplateDefinitionContext
    {
        EmailTemplateDefinition GetOrNull(string name);

        void Add(params EmailTemplateDefinition[] definitions);
    }
}