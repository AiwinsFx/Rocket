using System.Net.Mail;
using System.Threading.Tasks;

namespace Aiwins.Rocket.Emailing {
    /// <summary>
    /// 电子邮件发送方
    /// </summary>
    public interface IEmailSender {
        /// <summary>
        /// 发送邮件
        /// </summary>
        Task SendAsync (string to, string subject, string body, bool isBodyHtml = true);

        /// <summary>
        /// 发送邮件
        /// </summary>
        Task SendAsync (string from, string to, string subject, string body, bool isBodyHtml = true);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mail">邮件信息</param>
        /// <param name="normalize">
        /// 是否规范化电子邮件信息?
        /// 如果设置为true, 则会以UTF-8编码格式设置发送方的信息
        /// </param>
        Task SendAsync (MailMessage mail, bool normalize = true);

        /// <summary>
        /// 添加到后台任务队列
        /// </summary>
        Task QueueAsync (string to, string subject, string body, bool isBodyHtml = true);

        //TODO: 考虑添加其他队列方法. 问题: MailMessage不能序列化，因此不能在后台作业中使用
    }
}