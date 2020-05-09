using System;
using System.Collections.Generic;
using System.Linq;
using Aiwins.Rocket.Cli.ProjectBuilding.Files;

namespace Aiwins.Rocket.Cli.ProjectBuilding.Building.Steps
{
    public class LicenseCodeReplaceStep : ProjectBuildPipelineStep
    {
        public override void Execute(ProjectBuildContext context)
        {
            var licenseCode = context.BuildArgs.ExtraProperties.GetOrDefault("license-code");

            var appSettingsJsonFiles = context.Files.Where(f =>
                f.Name.EndsWith("appsettings.json", StringComparison.OrdinalIgnoreCase));

            foreach (var appSettingsJson in appSettingsJsonFiles)
            {
                appSettingsJson.ReplaceText(@"<LICENSE_CODE/>", licenseCode);
            }
        }
    }
}
