using System.Threading.Tasks;

namespace Aiwins.Rocket.TextTemplating.VirtualFiles {
    public interface ILocalizedTemplateContentReaderFactory {
        Task<ILocalizedTemplateContentReader> CreateAsync (TemplateDefinition templateDefinition);
    }
}