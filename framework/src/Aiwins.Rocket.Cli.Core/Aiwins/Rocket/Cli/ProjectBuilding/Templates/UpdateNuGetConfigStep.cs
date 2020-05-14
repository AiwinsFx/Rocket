using System;
using System.Collections.Generic;
using System.Linq;
using Aiwins.Rocket.Cli.ProjectBuilding.Building;
using Aiwins.Rocket.Cli.ProjectBuilding.Files;

namespace Aiwins.Rocket.Cli.ProjectBuilding.Templates
{
    public class UpdateNuGetConfigStep : ProjectBuildPipelineStep
    {
        private readonly string _nugetConfigFilePath;

        public UpdateNuGetConfigStep(string nugetConfigFilePath)
        {
            _nugetConfigFilePath = nugetConfigFilePath;
        }

        public override void Execute(ProjectBuildContext context)
        {
            var file = context.Files.FirstOrDefault(f => f.Name == _nugetConfigFilePath);
            if (file == null)
            {
                return;
            }

            var apiKey = context.BuildArgs.ExtraProperties.GetOrDefault("api-key");
            if (apiKey.IsNullOrEmpty())
            {
                return;
            }

            const string placeHolder = "<!-- {ROCKET_COMMERCIAL_NUGET_SOURCE} -->";
            var nugetSourceTag = $"<add key=\"ROCKET Commercial NuGet Source\" value=\"https://nuget.rocket.cn/{apiKey}/v3/index.json\" />";

            file.ReplaceText(placeHolder, nugetSourceTag);
        }
    }
}