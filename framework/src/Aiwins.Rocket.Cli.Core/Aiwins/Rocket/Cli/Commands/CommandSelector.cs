using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Aiwins.Rocket.Cli.Args;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.Cli.Commands
{
    public class CommandSelector : ICommandSelector, ITransientDependency
    {
        protected RocketCliOptions Options { get; }

        public CommandSelector(IOptions<RocketCliOptions> options)
        {
            Options = options.Value;
        }

        public Type Select(CommandLineArgs commandLineArgs)
        {
            if (commandLineArgs.Command.IsNullOrWhiteSpace())
            {
                return typeof(HelpCommand);
            }

            return Options.Commands.GetOrDefault(commandLineArgs.Command)
                   ?? typeof(HelpCommand);
        }
    }
}