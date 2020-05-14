using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Aiwins.Rocket.Cli.Args;
using Aiwins.Rocket.Cli.Commands.Services;
using Aiwins.Rocket.Cli.Utils;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.Cli.Commands
{
    public class SuiteCommand : IConsoleCommand, ITransientDependency
    {
        private readonly RocketNuGetIndexUrlService _nuGetIndexUrlService;
        private const string SuitePackageName = "Aiwins.Rocket.Suite";
        public ILogger<SuiteCommand> Logger { get; set; }

        public SuiteCommand(RocketNuGetIndexUrlService nuGetIndexUrlService)
        {
            _nuGetIndexUrlService = nuGetIndexUrlService;
            Logger = NullLogger<SuiteCommand>.Instance;
        }

        public async Task ExecuteAsync(CommandLineArgs commandLineArgs)
        {
            var operationType = NamespaceHelper.NormalizeNamespace(commandLineArgs.Target);

            switch (operationType)
            {
                case "":
                case null:
                    RunSuite();
                    break;

                case "install":
                    Logger.LogInformation("Installing ROCKET Suite...");
                    await InstallSuiteAsync();
                    break;

                case "update":
                    Logger.LogInformation("Updating ROCKET Suite...");
                    await UpdateSuiteAsync();
                    break;

                case "remove":
                    Logger.LogInformation("Removing ROCKET Suite...");
                    RemoveSuite();
                    break;
            }
        }

        private async Task InstallSuiteAsync()
        {
            var nugetIndexUrl = await _nuGetIndexUrlService.GetAsync();

            if (nugetIndexUrl == null)
            {
                return;
            }

            var result = CmdHelper.RunCmd("dotnet tool install " + SuitePackageName + " --add-source " + nugetIndexUrl + " -g");

            if (result == 0)
            {
                Logger.LogInformation("ROCKET Suite has been successfully installed.");
                Logger.LogInformation("You can run it with the CLI command \"rocket suite\"");
            }
        }

        private async Task UpdateSuiteAsync()
        {
            var nugetIndexUrl = await _nuGetIndexUrlService.GetAsync();

            if (nugetIndexUrl == null)
            {
                return;
            }

            CmdHelper.RunCmd("dotnet tool update " + SuitePackageName + " --add-source " + nugetIndexUrl + " -g");
        }

        private static void RemoveSuite()
        {
            CmdHelper.RunCmd("dotnet tool uninstall " + SuitePackageName + " -g");
        }

        private void RunSuite()
        {
            try
            {
                if (!GlobalToolHelper.IsGlobalToolInstalled("rocket-suite"))
                {
                    Logger.LogWarning("ROCKET Suite is not installed! To install it you can run the command: \"rocket suite install\"");
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.LogWarning("Couldn't check ROCKET Suite installed status: " + ex.Message);
            }

            CmdHelper.RunCmd("rocket-suite");
        }

        public string GetUsageInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine("");
            sb.AppendLine("Usage:");
            sb.AppendLine("");
            sb.AppendLine("  rocket suite [options]");
            sb.AppendLine("");
            sb.AppendLine("Options:");
            sb.AppendLine("");
            sb.AppendLine("<no argument>                          (run ROCKET Suite)");
            sb.AppendLine("install                                (install ROCKET Suite as a dotnet global tool)");
            sb.AppendLine("update                                 (update ROCKET Suite to the latest)");
            sb.AppendLine("remove                                 (uninstall ROCKET Suite)");
            sb.AppendLine("");
            sb.AppendLine("Examples:");
            sb.AppendLine("");
            sb.AppendLine("  rocket suite");
            sb.AppendLine("  rocket suite install");
            sb.AppendLine("  rocket suite update");
            sb.AppendLine("  rocket suite remove");
            sb.AppendLine("");

            return sb.ToString();
        }

        public string GetShortDescription()
        {
            return "Install, update, remove or start ROCKET Suite. See https://commercial.aiwins.cn/tools/suite.";
        }
    }
}