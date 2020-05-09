using System;
using Aiwins.Rocket.Cli.Args;

namespace Aiwins.Rocket.Cli.Commands
{
    public interface ICommandSelector
    {
        Type Select(CommandLineArgs commandLineArgs);
    }
}