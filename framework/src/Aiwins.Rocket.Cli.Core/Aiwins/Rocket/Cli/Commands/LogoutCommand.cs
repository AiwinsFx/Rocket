using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Aiwins.Rocket.Cli.Args;
using Aiwins.Rocket.Cli.Auth;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.Cli.Commands
{
    public class LogoutCommand : IConsoleCommand, ITransientDependency
    {
        public ILogger<LogoutCommand> Logger { get; set; }

        protected AuthService AuthService { get; }

        public LogoutCommand(AuthService authService)
        {
            AuthService = authService;
            Logger = NullLogger<LogoutCommand>.Instance;
        }

        public Task ExecuteAsync(CommandLineArgs commandLineArgs)
        {
            return AuthService.LogoutAsync();
        }

        public string GetUsageInfo()
        {
            return string.Empty;
        }

        public string GetShortDescription()
        {
            return "Sign out from " + CliUrls.AccountRocketIo + ".";
        }
    }
}