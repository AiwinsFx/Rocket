using System.Threading.Tasks;

namespace Aiwins.Rocket.Cli.ProjectBuilding
{
    public interface IProjectBuilder
    {
        Task<ProjectBuildResult> BuildAsync(ProjectBuildArgs args);
    }
}