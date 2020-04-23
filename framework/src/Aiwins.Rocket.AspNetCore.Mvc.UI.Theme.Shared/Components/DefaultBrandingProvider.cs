using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared.Components
{
    public class DefaultBrandingProvider : IBrandingProvider, ITransientDependency
    {
        public virtual string AppName => "MyApplication";

        public virtual string LogoUrl => null;
    }
}