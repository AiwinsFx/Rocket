﻿using System.Text;
using System.Threading.Tasks;
using Aiwins.Rocket.Cli.Args;
using Aiwins.Rocket.Cli.ProjectModification;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.Cli.Commands
{
    public class SwitchNightlyPreviewCommand : IConsoleCommand, ITransientDependency
    {
        private readonly PackageSourceSwitcher _packageSourceSwitcher;

        public SwitchNightlyPreviewCommand(PackageSourceSwitcher packageSourceSwitcher)
        {
            _packageSourceSwitcher = packageSourceSwitcher;
        }

        public async Task ExecuteAsync(CommandLineArgs commandLineArgs)
        {
            await _packageSourceSwitcher.SwitchToPreview(commandLineArgs);
        }

        public string GetUsageInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine("");
            sb.AppendLine("Usage:");
            sb.AppendLine("  rocket switch-to-preview [options]");
            sb.AppendLine(""); 
            sb.AppendLine("Options:");
            sb.AppendLine("-sd|--solution-directory");
            sb.AppendLine("");
            sb.AppendLine("See the documentation for more info: https://docs.rocket.cn/en/rocket/latest/CLI");

            return sb.ToString();
        }

        public string GetShortDescription()
        {
            return "Switches packages to nightly preview ROCKET version.";
        }
    }
}