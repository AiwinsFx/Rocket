using System.IO;
using System.Xml;
using Aiwins.Rocket.Cli.Utils;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.Cli.ProjectModification
{
    public class SolutionRocketVersionFinder : ITransientDependency
    {
        public string Find(string solutionFile)
        {
            var projectFilesUnderSrc = Directory.GetFiles(Path.GetDirectoryName(solutionFile),
                "*.csproj",
                SearchOption.AllDirectories);

            foreach (var projectFile in projectFilesUnderSrc)
            {
                var content = File.ReadAllText(projectFile);
                var doc = new XmlDocument() { PreserveWhitespace = true };

                doc.Load(StreamHelper.GenerateStreamFromString(content));

                var nodes = doc.SelectNodes("/Project/ItemGroup/PackageReference[starts-with(@Include, 'Aiwins.Rocket')]");

                var value = nodes?[0]?.Attributes?["Version"]?.Value;

                if (value != null)
                {
                    return value;
                }
            }

            return null;
        }
    }
}
