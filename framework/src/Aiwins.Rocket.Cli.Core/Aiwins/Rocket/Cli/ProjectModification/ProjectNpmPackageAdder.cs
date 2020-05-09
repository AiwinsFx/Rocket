﻿using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Aiwins.Rocket.Cli.Utils;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.IO;

namespace Aiwins.Rocket.Cli.ProjectModification
{
    public class ProjectNpmPackageAdder : ITransientDependency
    {
        public ILogger<ProjectNpmPackageAdder> Logger { get; set; }

        public ProjectNpmPackageAdder()
        {
            Logger = NullLogger<ProjectNpmPackageAdder>.Instance;
        }

        public Task AddAsync(string directory, NpmPackageInfo npmPackage)
        {
            var packageJsonFilePath = Path.Combine(directory, "package.json");
            if (!File.Exists(packageJsonFilePath) || File.ReadAllText(packageJsonFilePath).Contains($"\"{npmPackage.Name}\""))
            {
                return Task.CompletedTask;
            }

            Logger.LogInformation($"Installing '{npmPackage.Name}' package to the project '{packageJsonFilePath}'...");

            using (DirectoryHelper.ChangeCurrentDirectory(directory))
            {
                Logger.LogInformation("yarn add " + npmPackage.Name);
                CmdHelper.RunCmd("yarn add " + npmPackage.Name);

                Logger.LogInformation("gulp");
                CmdHelper.RunCmd("gulp");
            }

            return Task.CompletedTask;
        }
    }
}