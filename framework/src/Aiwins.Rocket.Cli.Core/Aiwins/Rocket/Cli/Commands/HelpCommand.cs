using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Aiwins.Rocket.Cli.Args;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.Cli.Commands
{
    public class HelpCommand : IConsoleCommand, ITransientDependency
    {
        public ILogger<HelpCommand> Logger { get; set; }
        protected RocketCliOptions RocketCliOptions { get; }
        protected IHybridServiceScopeFactory ServiceScopeFactory { get; }

        public HelpCommand(IOptions<RocketCliOptions> cliOptions,
            IHybridServiceScopeFactory serviceScopeFactory)
        {
            ServiceScopeFactory = serviceScopeFactory;
            Logger = NullLogger<HelpCommand>.Instance;
            RocketCliOptions = cliOptions.Value;
        }

        public Task ExecuteAsync(CommandLineArgs commandLineArgs)
        {
            if (string.IsNullOrWhiteSpace(commandLineArgs.Target))
            {
                Logger.LogInformation(GetUsageInfo());
                return Task.CompletedTask;
            }

            if (!RocketCliOptions.Commands.ContainsKey(commandLineArgs.Target))
            {
                Logger.LogWarning($"There is no command named {commandLineArgs.Target}.");
                Logger.LogInformation(GetUsageInfo());
                return Task.CompletedTask;
            }

            var commandType = RocketCliOptions.Commands[commandLineArgs.Target];

            using (var scope = ServiceScopeFactory.CreateScope())
            {
                var command = (IConsoleCommand) scope.ServiceProvider.GetRequiredService(commandType);
                Logger.LogInformation(command.GetUsageInfo());
            }

            return Task.CompletedTask;
        }

        public string GetUsageInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine("");
            sb.AppendLine("Usage:");
            sb.AppendLine("");
            sb.AppendLine("    rocket <command> <target> [options]");
            sb.AppendLine("");
            sb.AppendLine("Command List:");
            sb.AppendLine("");

            foreach (var command in RocketCliOptions.Commands.ToArray())
            {
                string shortDescription;

                using (var scope = ServiceScopeFactory.CreateScope())
                {
                    shortDescription = ((IConsoleCommand) scope.ServiceProvider
                            .GetRequiredService(command.Value)).GetShortDescription();
                }

                sb.Append("    > ");
                sb.Append(command.Key);
                sb.Append(string.IsNullOrWhiteSpace(shortDescription) ? "":":");
                sb.Append(" ");
                sb.AppendLine(shortDescription);
            }

            sb.AppendLine("");
            sb.AppendLine("To get a detailed help for a command:");
            sb.AppendLine("");
            sb.AppendLine("    rocket help <command>");
            sb.AppendLine("");
            sb.AppendLine("See the documentation for more info: https://docs.rocket.io/en/rocket/latest/CLI");

            return sb.ToString();
        }

        public string GetShortDescription()
        {
            return "Show command line help. Write ` rocket help <command> `";
        }
    }
}