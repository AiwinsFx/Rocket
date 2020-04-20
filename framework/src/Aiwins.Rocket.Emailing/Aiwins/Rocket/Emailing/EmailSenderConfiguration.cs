using System;
using System.Threading.Tasks;
using Aiwins.Rocket.Settings;

namespace Aiwins.Rocket.Emailing
{
    /// <summary>
    /// Base implementation of <see cref="IEmailSenderConfiguration"/> that reads settings
    /// from <see cref="ISettingProvider"/>.
    /// </summary>
    public abstract class EmailSenderConfiguration : IEmailSenderConfiguration
    {
        protected ISettingProvider SettingProvider { get; }

        /// <summary>
        /// Creates a new <see cref="EmailSenderConfiguration"/>.
        /// </summary>
        protected EmailSenderConfiguration(ISettingProvider settingProvider)
        {
            SettingProvider = settingProvider;
        }

        public Task<string> GetDefaultFromAddressAsync()
        {
            return GetNotEmptySettingValueAsync(EmailSettingNames.DefaultFromAddress);
        }

        public Task<string> GetDefaultFromDisplayNameAsync()
        {
            return GetNotEmptySettingValueAsync(EmailSettingNames.DefaultFromDisplayName);
        }

        /// <summary>
        /// Gets a setting value by checking. Throws <see cref="RocketException"/> if it's null or empty.
        /// </summary>
        /// <param name="name">Name of the setting</param>
        /// <returns>Value of the setting</returns>
        protected async Task<string> GetNotEmptySettingValueAsync(string name)
        {
            var value = await SettingProvider.GetOrNullAsync(name);

            if (value.IsNullOrEmpty())
            {
                throw new RocketException($"Setting value for '{name}' is null or empty!");
            }

            return value;
        }
    }
}