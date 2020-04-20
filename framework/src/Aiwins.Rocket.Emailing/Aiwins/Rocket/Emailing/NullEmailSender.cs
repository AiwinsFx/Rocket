using System.Net.Mail;
using System.Threading.Tasks;
using Aiwins.Rocket.BackgroundJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Aiwins.Rocket.Emailing {
    /// <summary>
    /// 实现了 <see cref="IEmailSender"/> 接口
    /// 不发送邮件仅记录日志信息
    /// </summary>
    public class NullEmailSender : EmailSenderBase {
        public ILogger<NullEmailSender> Logger { get; set; }

        /// <summary>
        /// 创建一个空 <see cref="NullEmailSender"/> 实例
        /// </summary>
        public NullEmailSender (IEmailSenderConfiguration configuration, IBackgroundJobManager backgroundJobManager) : base (configuration, backgroundJobManager) {
            Logger = NullLogger<NullEmailSender>.Instance;
        }

        protected override Task SendEmailAsync (MailMessage mail) {
            Logger.LogWarning ("USING NullEmailSender!");
            Logger.LogDebug ("SendEmailAsync:");
            LogEmail (mail);
            return Task.FromResult (0);
        }

        private void LogEmail (MailMessage mail) {
            Logger.LogDebug (mail.To.ToString ());
            Logger.LogDebug (mail.CC.ToString ());
            Logger.LogDebug (mail.Subject);
            Logger.LogDebug (mail.Body);
        }
    }
}