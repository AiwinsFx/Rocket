using System.Threading.Tasks;

namespace Aiwins.Rocket.Emailing.Templates {
    public interface IEmailTemplateProvider {
        Task<EmailTemplate> GetAsync (string name);

        Task<EmailTemplate> GetAsync (string name, string cultureName);
    }
}