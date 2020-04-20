using System;
using System.Threading.Tasks;
using Aiwins.Rocket.Settings;

namespace Aiwins.Rocket.Emailing {
    /// <summary>
    /// 实现了 <see cref="IEmailSenderConfiguration"/> 接口
    /// 通过 <see cref="ISettingProvider"/> 获取配置信息
    /// </summary>
    public abstract class EmailSenderConfiguration : IEmailSenderConfiguration {
        protected ISettingProvider SettingProvider { get; }

        /// <summary>
        /// 创建一个新的 <see cref="EmailSenderConfiguration"/> 实例
        /// </summary>
        protected EmailSenderConfiguration (ISettingProvider settingProvider) {
            SettingProvider = settingProvider;
        }

        public Task<string> GetDefaultFromAddressAsync () {
            return GetNotEmptySettingValueAsync (EmailSettingNames.DefaultFromAddress);
        }

        public Task<string> GetDefaultFromDisplayNameAsync () {
            return GetNotEmptySettingValueAsync (EmailSettingNames.DefaultFromDisplayName);
        }

        /// <summary>
        /// 获取配置信息，如果不存在或者为空则抛出异常 <see cref="RocketException"/> 。
        /// </summary>
        /// <param name="name">配置名称</param>
        /// <returns>配置信息</returns>
        protected async Task<string> GetNotEmptySettingValueAsync (string name) {
            var value = await SettingProvider.GetOrNullAsync (name);

            if (value.IsNullOrEmpty ()) {
                throw new RocketException ($"Setting value for '{name}' is null or empty!");
            }

            return value;
        }
    }
}