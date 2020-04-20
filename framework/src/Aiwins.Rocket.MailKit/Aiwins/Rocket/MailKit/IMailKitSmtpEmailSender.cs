using System.Threading.Tasks;
using Aiwins.Rocket.Emailing;
using MailKit.Net.Smtp;

namespace Aiwins.Rocket.MailKit {
    public interface IMailKitSmtpEmailSender : IEmailSender {
        Task<SmtpClient> BuildClientAsync ();
    }
}