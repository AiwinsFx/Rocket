using Aiwins.Rocket.Cli.ProjectBuilding.Building;
using Aiwins.Rocket.Cli.ProjectBuilding.Building.Steps;
using Aiwins.Rocket.Cli.ProjectBuilding.Files;

namespace Aiwins.Rocket.Cli.ProjectBuilding.Templates.App
{
    public class AppTemplateProjectRenameStep : ProjectBuildPipelineStep
    {
        private readonly string _oldProjectName;
        private readonly string _newProjectName;

        public AppTemplateProjectRenameStep(
            string oldProjectName,
            string newProjectName)
        {
            _oldProjectName = oldProjectName;
            _newProjectName = newProjectName;
        }

        public override void Execute(ProjectBuildContext context)
        {
            context
                .GetFile("/aspnet-core/MyCompanyName.MyProjectName.sln")
                .ReplaceText(_oldProjectName, _newProjectName);
            
            RenameHelper.RenameAll(context.Files, _oldProjectName, _newProjectName);
        }
    }
}
