using System.Threading.Tasks;

namespace Aiwins.Rocket.Emailing {
    /// <summary>
    /// 定义发送邮件的配置
    /// </summary>
    public interface IEmailSenderConfiguration {
        Task<string> GetDefaultFromAddressAsync ();

        Task<string> GetDefaultFromDisplayNameAsync ();
    }
}