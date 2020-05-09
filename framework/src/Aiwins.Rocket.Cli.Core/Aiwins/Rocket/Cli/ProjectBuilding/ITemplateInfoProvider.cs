using Aiwins.Rocket.Cli.ProjectBuilding.Building;

namespace Aiwins.Rocket.Cli.ProjectBuilding
{
    public interface ITemplateInfoProvider
    {
        TemplateInfo GetDefault();

        TemplateInfo Get(string name);
    }
}