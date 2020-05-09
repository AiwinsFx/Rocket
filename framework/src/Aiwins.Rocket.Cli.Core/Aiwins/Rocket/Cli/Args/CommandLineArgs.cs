using System;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Cli.Args
{
    public class CommandLineArgs
    {
        [CanBeNull]
        public string Command { get; }

        [CanBeNull]
        public string Target { get; }

        [NotNull]
        public RocketCommandLineOptions Options { get; }

        public CommandLineArgs([CanBeNull] string command = null, [CanBeNull] string target = null)
        {
            Command = command;
            Target = target;
            Options = new RocketCommandLineOptions();
        }

        public static CommandLineArgs Empty()
        {
            return new CommandLineArgs();
        }

        public bool IsCommand(string command)
        {
            return string.Equals(Command, command, StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (Command != null)
            {
                sb.AppendLine($"Command: {Command}");
            }

            if (Target != null)
            {
                sb.AppendLine($"Target: {Target}");
            }

            if (Options.Any())
            {
                sb.AppendLine("Options:");
                foreach (var option in Options)
                {
                    sb.AppendLine($" - {option.Key} = {option.Value}");
                }
            }

            if (sb.Length <= 0)
            {
                sb.Append("<EMPTY>");
            }

            return sb.ToString();
        }
    }
}