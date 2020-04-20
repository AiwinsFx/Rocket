using System.Net.Mail;
using System.Threading.Tasks;
using Aiwins.Rocket.BackgroundJobs;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.Emailing;
using Aiwins.Rocket.Emailing.Smtp;
using MailKit.Security;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Aiwins.Rocket.MailKit {
    [Dependency (ServiceLifetime.Transient, ReplaceServices = true)]
    public class MailKitSmtpEmailSender : EmailSenderBase, IMailKitSmtpEmailSender {
        protected RocketMailKitOptions RocketMailKitOptions { get; }

        protected ISmtpEmailSenderConfiguration SmtpConfiguration { get; }

        public MailKitSmtpEmailSender (ISmtpEmailSenderConfiguration smtpConfiguration,
            IBackgroundJobManager backgroundJobManager,
            IOptions<RocketMailKitOptions> abpMailKitConfiguration) : base (smtpConfiguration, backgroundJobManager) {
            RocketMailKitOptions = abpMailKitConfiguration.Value;
            SmtpConfiguration = smtpConfiguration;
        }

        protected override async Task SendEmailAsync (MailMessage mail) {
            using (var client = await BuildClientAsync ()) {
                var message = MimeMessage.CreateFromMailMessage (mail);
                await client.SendAsync (message);
                await client.DisconnectAsync (true);
            }
        }

        public async Task<SmtpClient> BuildClientAsync () {
            var client = new SmtpClient ();

            try {
                await ConfigureClient (client);
                return client;
            } catch {
                client.Dispose ();
                throw;
            }
        }

        protected virtual async Task ConfigureClient (SmtpClient client) {
            client.Connect (
                await SmtpConfiguration.GetHostAsync (),
                await SmtpConfiguration.GetPortAsync (),
                await GetSecureSocketOption ()
            );

            if (await SmtpConfiguration.GetUseDefaultCredentialsAsync ()) {
                return;
            }

            client.Authenticate (
                await SmtpConfiguration.GetUserNameAsync (),
                await SmtpConfiguration.GetPasswordAsync ()
            );
        }

        protected virtual async Task<SecureSocketOptions> GetSecureSocketOption () {
            if (RocketMailKitOptions.SecureSocketOption.HasValue) {
                return RocketMailKitOptions.SecureSocketOption.Value;
            }

            return await SmtpConfiguration.GetEnableSslAsync () ?
                SecureSocketOptions.SslOnConnect :
                SecureSocketOptions.StartTlsWhenAvailable;
        }
    }
}