using System.Collections.Generic;
using JetBrains.Annotations;
using Aiwins.Rocket.Cli.ProjectBuilding.Building;

namespace Aiwins.Rocket.Cli.ProjectBuilding
{
    public class ProjectBuildArgs
    {
        [NotNull]
        public SolutionName SolutionName { get; }

        [CanBeNull]
        public string TemplateName { get; set; }

        [CanBeNull]
        public string Version { get; set; }

        public DatabaseProvider DatabaseProvider { get; set; }

        public UiFramework UiFramework { get; set; }

        public MobileApp? MobileApp { get; set; }

        [CanBeNull]
        public string RocketGitHubLocalRepositoryPath { get; set; }

        [CanBeNull]
        public string TemplateSource { get; set; }

        [CanBeNull]
        public string ConnectionString { get; set; }

        [NotNull]
        public Dictionary<string, string> ExtraProperties { get; set; }

        public ProjectBuildArgs(
            [NotNull] SolutionName solutionName,
            [CanBeNull] string templateName = null,
            [CanBeNull] string version = null,
            DatabaseProvider databaseProvider = DatabaseProvider.NotSpecified,
            UiFramework uiFramework = UiFramework.NotSpecified,
            MobileApp? mobileApp = null,
            [CanBeNull] string rocketGitHubLocalRepositoryPath = null,
            [CanBeNull] string templateSource = null,
            Dictionary<string, string> extraProperties = null,
            [CanBeNull] string connectionString = null)
        {
            SolutionName = Check.NotNull(solutionName, nameof(solutionName));
            TemplateName = templateName;
            Version = version;
            DatabaseProvider = databaseProvider;
            UiFramework = uiFramework;
            MobileApp = mobileApp;
            RocketGitHubLocalRepositoryPath = rocketGitHubLocalRepositoryPath;
            TemplateSource = templateSource;
            ExtraProperties = extraProperties ?? new Dictionary<string, string>();
            ConnectionString = connectionString;
        }
    }
}