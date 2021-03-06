﻿using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Aiwins.Rocket.Cli.Args;
using Aiwins.Rocket.Cli.ProjectModification;
using Aiwins.Rocket.Cli.Utils;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.Cli.Commands
{
    public class AddPackageCommand : IConsoleCommand, ITransientDependency
    {
        public ILogger<AddPackageCommand> Logger { get; set; }

        protected ProjectNugetPackageAdder ProjectNugetPackageAdder { get; }

        public AddPackageCommand(ProjectNugetPackageAdder projectNugetPackageAdder)
        {
            ProjectNugetPackageAdder = projectNugetPackageAdder;
            Logger = NullLogger<AddPackageCommand>.Instance;
        }

        public virtual async Task ExecuteAsync(CommandLineArgs commandLineArgs)
        {
            if (commandLineArgs.Target == null)
            {
                throw new CliUsageException(
                    "Package name is missing!" +
                    Environment.NewLine + Environment.NewLine +
                    GetUsageInfo()
                );
            }

            await ProjectNugetPackageAdder.AddAsync(
                GetProjectFile(commandLineArgs),
                commandLineArgs.Target
            );
        }

        public string GetUsageInfo()
        {
            var sb = new StringBuilder();

            sb.AppendLine("");
            sb.AppendLine("'add-package' command is used to add an ROCKET package to a project.");
            sb.AppendLine("It should be used in a folder containing a .csproj file.");
            sb.AppendLine("");
            sb.AppendLine("Usage:");
            sb.AppendLine("");
            sb.AppendLine("  rocket add-package <package-name> [options]");
            sb.AppendLine("");
            sb.AppendLine("Options:");
            sb.AppendLine("");
            sb.AppendLine("  -p|--project <project-file>    Specify the project file explicitly.");
            sb.AppendLine("");
            sb.AppendLine("Examples:");
            sb.AppendLine("");
            sb.AppendLine("  rocket add-package Aiwins.Rocket.FluentValidation                                  Adds the package to the current project.");
            sb.AppendLine("  rocket add-package Aiwins.Rocket.FluentValidation -p Acme.BookStore.Application    Adds the package to the given project.");
            sb.AppendLine("");
            sb.AppendLine("See the documentation for more info: https://docs.aiwins.cn/en/rocket/latest/CLI");

            return sb.ToString();
        }

        public string GetShortDescription()
        {
            return "Add a new ROCKET package to a project by adding related NuGet package dependencies and [DependsOn(...)] attributes.";
        }

        protected virtual string GetProjectFile(CommandLineArgs commandLineArgs)
        {
            var providedProjectFile = PathHelper.NormalizePath(
                commandLineArgs.Options.GetOrNull(
                    Options.Project.Short,
                    Options.Project.Long
                )
            );

            if (!providedProjectFile.IsNullOrWhiteSpace())
            {
                return providedProjectFile;
            }

            var foundProjectFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.csproj");
            if (foundProjectFiles.Length == 1)
            {
                return foundProjectFiles[0];
            }

            if (foundProjectFiles.Length == 0)
            {
                throw new CliUsageException("'rocket add-package' command should be used inside a folder contaning a .csproj file!");
            }

            //foundProjectFiles.Length > 1

            var sb = new StringBuilder("There are multiple project (.csproj) files in the current directory. Please specify one of the files below:");

            foreach (var foundProjectFile in foundProjectFiles)
            {
                sb.AppendLine("* " + foundProjectFile);
            }

            sb.AppendLine("Example:");
            sb.AppendLine($"rocket add-package {commandLineArgs.Target} -p {foundProjectFiles[0]}");

            throw new CliUsageException(sb.ToString());
        }


        public static class Options
        {
            public static class Project
            {
                public const string Short = "p";
                public const string Long = "project";
            }
        }
    }
}