using System.Threading.Tasks;

namespace Aiwins.Rocket.TextTemplating {
    public interface ITemplateContentContributor {
        Task<string> GetOrNullAsync (TemplateContentContributorContext context);
    }
}