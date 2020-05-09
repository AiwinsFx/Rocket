using System.Collections.Generic;
using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Docs.HtmlConverting
{
    public interface IDocumentSectionRenderer: ITransientDependency
    {
        Task<string> RenderAsync(string doucment, DocumentRenderParameters parameters = null, List<DocumentPartialTemplateContent> partialTemplates = null);

        Task<Dictionary<string, List<string>>> GetAvailableParametersAsync(string document);

        Task<List<DocumentPartialTemplateWithValuesDto>> GetPartialTemplatesInDocumentAsync(string documentContent);
    }
}
