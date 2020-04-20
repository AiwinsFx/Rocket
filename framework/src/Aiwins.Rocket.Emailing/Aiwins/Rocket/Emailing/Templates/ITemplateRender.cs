using System.Threading.Tasks;

namespace Aiwins.Rocket.Emailing.Templates {
    public interface ITemplateRender {
        Task<string> RenderAsync (string template, object model = null);
    }
}