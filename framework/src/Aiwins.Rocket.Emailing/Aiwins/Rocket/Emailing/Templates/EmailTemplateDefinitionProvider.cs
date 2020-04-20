using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.Emailing.Templates {
    public abstract class EmailTemplateDefinitionProvider : IEmailTemplateDefinitionProvider, ITransientDependency {
        public abstract void Define (IEmailTemplateDefinitionContext context);
    }
}