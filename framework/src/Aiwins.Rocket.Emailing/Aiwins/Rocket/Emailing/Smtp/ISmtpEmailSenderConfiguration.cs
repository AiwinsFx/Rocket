using System.Threading.Tasks;

namespace Aiwins.Rocket.Emailing.Smtp {
    /// <summary>
    /// SmtpClient客户端配置
    /// </summary>
    public interface ISmtpEmailSenderConfiguration : IEmailSenderConfiguration {
        /// <summary>
        /// SMTP 服务主机/IP地址
        /// </summary>
        Task<string> GetHostAsync ();

        /// <summary>
        /// SMTP 端口号
        /// </summary>
        Task<int> GetPortAsync ();

        /// <summary>
        /// SMTP 服务用户名
        /// </summary>
        Task<string> GetUserNameAsync ();

        /// <summary>
        /// SMTP 服务密码
        /// </summary>
        Task<string> GetPasswordAsync ();

        /// <summary>
        /// SMTP 服务域名
        /// </summary>
        Task<string> GetDomainAsync ();

        /// <summary>
        /// 是否启用SSL
        /// </summary>
        Task<bool> GetEnableSslAsync ();

        /// <summary>
        /// 是否使用默认证书
        /// </summary>
        Task<bool> GetUseDefaultCredentialsAsync ();
    }
}