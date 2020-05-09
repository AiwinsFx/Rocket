using System.Collections.Generic;

namespace Aiwins.Rocket.Cli.ProjectBuilding.Building
{
    public class ProjectBuildPipeline
    {
        public ProjectBuildContext Context { get; }

        public List<ProjectBuildPipelineStep> Steps { get; }

        public ProjectBuildPipeline(ProjectBuildContext context)
        {
            Context = context;
            Steps = new List<ProjectBuildPipelineStep>();
        }

        public void Execute()
        {
            foreach (var step in Steps)
            {
                step.Execute(Context);
            }
        }
    }
}