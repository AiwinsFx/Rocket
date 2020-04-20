using System;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Aiwins.Rocket.BackgroundJobs;

namespace Aiwins.Rocket.Emailing {
    /// <summary>
    /// 实现了 <see cref="IEmailSender"/> 接口的基类
    /// </summary>
    public abstract class EmailSenderBase : IEmailSender {
        protected IEmailSenderConfiguration Configuration { get; }

        protected IBackgroundJobManager BackgroundJobManager { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected EmailSenderBase (IEmailSenderConfiguration configuration, IBackgroundJobManager backgroundJobManager) {
            Configuration = configuration;
            BackgroundJobManager = backgroundJobManager;
        }

        public virtual async Task SendAsync (string to, string subject, string body, bool isBodyHtml = true) {
            await SendAsync (new MailMessage {
                To = { to },
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = isBodyHtml
            });
        }

        public virtual async Task SendAsync (string from, string to, string subject, string body, bool isBodyHtml = true) {
            await SendAsync (new MailMessage (from, to, subject, body) { IsBodyHtml = isBodyHtml });
        }

        public virtual async Task SendAsync (MailMessage mail, bool normalize = true) {
            if (normalize) {
                await NormalizeMailAsync (mail);
            }

            await SendEmailAsync (mail);
        }

        public virtual async Task QueueAsync (string to, string subject, string body, bool isBodyHtml = true) {
            if (!BackgroundJobManager.IsAvailable ()) {
                await SendAsync (to, subject, body, isBodyHtml);
                return;
            }

            await BackgroundJobManager.EnqueueAsync (
                new BackgroundEmailSendingJobArgs {
                    To = to,
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = isBodyHtml
                }
            );
        }

        /// <summary>
        /// 调用此方法发送邮件
        /// </summary>
        /// <param name="mail">邮件信息</param>
        protected abstract Task SendEmailAsync (MailMessage mail);

        /// <summary>
        /// 对邮件进行格式化处理
        /// 如果邮件消息体未设置发送方 <see cref="MailMessage.From"/> 的信息，则以UTF8编码格式设置邮件发送方的信息
        /// </summary>
        /// <param name="mail">邮件信息</param>
        protected virtual async Task NormalizeMailAsync (MailMessage mail) {
            if (mail.From == null || mail.From.Address.IsNullOrEmpty ()) {
                mail.From = new MailAddress (
                    await Configuration.GetDefaultFromAddressAsync (),
                    await Configuration.GetDefaultFromDisplayNameAsync (),
                    Encoding.UTF8
                );
            }

            if (mail.HeadersEncoding == null) {
                mail.HeadersEncoding = Encoding.UTF8;
            }

            if (mail.SubjectEncoding == null) {
                mail.SubjectEncoding = Encoding.UTF8;
            }

            if (mail.BodyEncoding == null) {
                mail.BodyEncoding = Encoding.UTF8;
            }
        }
    }
}