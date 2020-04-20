using System.Net.Mail;
using System.Threading.Tasks;

namespace Aiwins.Rocket.Emailing.Smtp {
    /// <summary>
    /// SMTP协议发送邮件
    /// </summary>
    public interface ISmtpEmailSender : IEmailSender {
        /// <summary>
        /// 创建一个SMTP客户端 <see cref="SmtpClient"/> 实例发送邮件
        /// </summary>
        /// <returns>
        /// 返回一个 <see cref="SmtpClient"/> SMTP客户端对象
        /// </returns>
        Task<SmtpClient> BuildClientAsync ();
    }
}