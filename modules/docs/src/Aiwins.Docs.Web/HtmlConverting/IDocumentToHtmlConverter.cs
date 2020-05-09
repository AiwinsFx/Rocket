using Aiwins.Docs.Documents;
using Aiwins.Docs.Projects;

namespace Aiwins.Docs.HtmlConverting
{
    public interface IDocumentToHtmlConverter
    {
        string Convert(ProjectDto project, DocumentWithDetailsDto document, string version, string languageCode);
    }
}