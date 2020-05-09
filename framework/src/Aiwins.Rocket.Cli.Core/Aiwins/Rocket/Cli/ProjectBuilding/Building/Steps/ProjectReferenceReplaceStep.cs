﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using Aiwins.Rocket.Cli.ProjectBuilding.Files;
using Aiwins.Rocket.Cli.Utils;

namespace Aiwins.Rocket.Cli.ProjectBuilding.Building.Steps
{
    public class ProjectReferenceReplaceStep : ProjectBuildPipelineStep
    {
        public override void Execute(ProjectBuildContext context)
        {
            if (context.BuildArgs.ExtraProperties.ContainsKey("local-framework-ref"))
            {
                var localRocketRepoPath = context.BuildArgs.RocketGitHubLocalRepositoryPath;

                if (string.IsNullOrWhiteSpace(localRocketRepoPath))
                {
                    return;
                }

                new ProjectReferenceReplacer.LocalProjectPathReferenceReplacer(
                    context.Files,
                    context.Module?.Namespace ?? "MyCompanyName.MyProjectName",
                    localRocketRepoPath
                ).Run();
            }
            else
            {
                var nugetPackageVersion = context.TemplateFile.RepositoryNugetVersion;

                if (IsBranchName(nugetPackageVersion))
                {
                    nugetPackageVersion = context.TemplateFile.LatestVersion;
                }

                new ProjectReferenceReplacer.NugetReferenceReplacer(
                    context.Files,
                    context.Module?.Namespace ?? "MyCompanyName.MyProjectName",
                    nugetPackageVersion
                ).Run();
            }
        }

        private bool IsBranchName(string versionOrBranchName)
        {
            Check.NotNullOrWhiteSpace(versionOrBranchName, nameof(versionOrBranchName));

            if (char.IsDigit(versionOrBranchName[0]))
            {
                return false;
            }

            if (versionOrBranchName[0].IsIn('v', 'V') &&
                versionOrBranchName.Length > 1 &&
                char.IsDigit(versionOrBranchName[1]))
            {
                return false;
            }

            return true;
        }

        private abstract class ProjectReferenceReplacer
        {
            private readonly List<FileEntry> _entries;
            private readonly string _projectName;

            protected ProjectReferenceReplacer(
                List<FileEntry> entries,
                string projectName)
            {
                _entries = entries;
                _projectName = projectName;
            }

            public void Run()
            {
                foreach (var fileEntry in _entries)
                {
                    if (fileEntry.Name.EndsWith(".csproj"))
                    {
                        fileEntry.SetContent(ProcessFileContent(fileEntry.Content));
                    }
                }
            }

            private string ProcessFileContent(string content)
            {
                Check.NotNull(content, nameof(content));

                var doc = new XmlDocument() { PreserveWhitespace = true };

                doc.Load(StreamHelper.GenerateStreamFromString(content));

                return ProcessReferenceNodes(doc, content);
            }

            private string ProcessReferenceNodes(XmlDocument doc, string content)
            {
                Check.NotNull(content, nameof(content));

                var nodes = doc.SelectNodes("/Project/ItemGroup/ProjectReference[@Include]");

                foreach (XmlNode oldNode in nodes)
                {
                    var oldNodeIncludeValue = oldNode.Attributes["Include"].Value;

                    // ReSharper disable once PossibleNullReferenceException : Can not be null because nodes are selected with include attribute filter in previous method
                    if (oldNodeIncludeValue.Contains(_projectName) && _entries.Any(e=>e.Name.EndsWith(GetProjectNameWithExtensionFromProjectReference(oldNodeIncludeValue))))
                    {
                        continue;
                    }

                    XmlNode newNode = null;

                    newNode = GetNewReferenceNode(doc, oldNodeIncludeValue);

                    oldNode.ParentNode.ReplaceChild(newNode, oldNode);
                }

                return doc.OuterXml;
            }

            private string GetProjectNameWithExtensionFromProjectReference(string oldNodeIncludeValue)
            {
                if (string.IsNullOrWhiteSpace(oldNodeIncludeValue))
                {
                    return oldNodeIncludeValue;
                }

                return oldNodeIncludeValue.Split('\\', '/').Last();
            }

            protected abstract XmlElement GetNewReferenceNode(XmlDocument doc, string oldNodeIncludeValue);


            public class NugetReferenceReplacer : ProjectReferenceReplacer
            {
                private readonly string _nugetPackageVersion;

                public NugetReferenceReplacer(List<FileEntry> entries, string projectName, string nugetPackageVersion)
                    : base(entries, projectName)
                {
                    _nugetPackageVersion = nugetPackageVersion;
                }

                protected override XmlElement GetNewReferenceNode(XmlDocument doc, string oldNodeIncludeValue)
                {
                    var newNode = doc.CreateElement("PackageReference");

                    var includeAttr = doc.CreateAttribute("Include");
                    includeAttr.Value = ConvertToNugetReference(oldNodeIncludeValue);
                    newNode.Attributes.Append(includeAttr);

                    var versionAttr = doc.CreateAttribute("Version");
                    versionAttr.Value = _nugetPackageVersion;
                    newNode.Attributes.Append(versionAttr);
                    return newNode;
                }

                private string ConvertToNugetReference(string oldValue)
                {
                    var newValue = Regex.Match(oldValue, @"\\((?!.+?\\).+?)\.csproj", RegexOptions.CultureInvariant | RegexOptions.Compiled);
                    if (newValue.Success && newValue.Groups.Count == 2)
                    {
                        return newValue.Groups[1].Value;
                    }

                    return oldValue;
                }
            }


            public class LocalProjectPathReferenceReplacer : ProjectReferenceReplacer
            {
                private readonly string _gitHubLocalRepositoryPath;

                public LocalProjectPathReferenceReplacer(List<FileEntry> entries, string projectName, string gitHubLocalRepositoryPath)
                    : base(entries, projectName)
                {
                    _gitHubLocalRepositoryPath = gitHubLocalRepositoryPath;
                }

                protected override XmlElement GetNewReferenceNode(XmlDocument doc, string oldNodeIncludeValue)
                {
                    var newNode = doc.CreateElement("ProjectReference");

                    var includeAttr = doc.CreateAttribute("Include");
                    includeAttr.Value = SetGithubPath(oldNodeIncludeValue);
                    newNode.Attributes.Append(includeAttr);

                    return newNode;
                }

                private string SetGithubPath(string includeValue)
                {
                    while (includeValue.StartsWith("..\\"))
                    {
                        includeValue = includeValue.TrimStart('.');
                        includeValue = includeValue.TrimStart('\\');
                    }

                    includeValue = _gitHubLocalRepositoryPath.EnsureEndsWith('\\') + includeValue;

                    return includeValue;
                }
            }
        }
    }
}