using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Cli.ProjectBuilding
{
    public interface ISourceCodeStore
    {
        Task<TemplateFile> GetAsync(
            string name,
            string type,
            [CanBeNull] string version = null,
            [CanBeNull] string templateSource = null
        );
    }
}