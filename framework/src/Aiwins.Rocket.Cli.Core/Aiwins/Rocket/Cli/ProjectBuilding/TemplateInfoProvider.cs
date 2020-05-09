using System;
using Aiwins.Rocket.Cli.ProjectBuilding.Building;
using Aiwins.Rocket.Cli.ProjectBuilding.Templates.App;
using Aiwins.Rocket.Cli.ProjectBuilding.Templates.MvcModule;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.Cli.ProjectBuilding
{
    public class TemplateInfoProvider : ITemplateInfoProvider, ITransientDependency
    {
        public TemplateInfo GetDefault()
        {
            return Get(AppTemplate.TemplateName);
        }

        public TemplateInfo Get(string name)
        {
            switch (name)
            {
                case AppTemplate.TemplateName:
                    return new AppTemplate();
                case AppProTemplate.TemplateName:
                    return new AppProTemplate();
                case ModuleTemplate.TemplateName:
                    return new ModuleTemplate();
                case ModuleProTemplate.TemplateName:
                    return new ModuleProTemplate();
                default:
                    throw new Exception("There is no template found with given name: " + name);
            }
        }
    }
}