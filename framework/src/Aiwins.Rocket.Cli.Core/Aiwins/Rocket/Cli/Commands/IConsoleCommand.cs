using System.Threading.Tasks;
using Aiwins.Rocket.Cli.Args;

namespace Aiwins.Rocket.Cli.Commands
{
    public interface IConsoleCommand
    {
        Task ExecuteAsync(CommandLineArgs commandLineArgs);

        string GetUsageInfo();

        string GetShortDescription();
    }
}