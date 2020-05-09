using Aiwins.Rocket.Emailing.Localization;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Settings;

namespace Aiwins.Rocket.Emailing {
    /// <summary>
    /// Defines settings to send emails.
    /// <see cref="EmailSettingNames"/> for all available configurations.
    /// </summary>
    internal class EmailSettingProvider : SettingDefinitionProvider {
        public override void Define (ISettingDefinitionContext context) {
            context.Add (
                new SettingDefinition (
                    EmailSettingNames.Smtp.Host,
                    "127.0.0.1",
                    L ("DisplayName:Rocket.Mailing.Smtp.Host"),
                    L ("Description:Rocket.Mailing.Smtp.Host")),

                new SettingDefinition (EmailSettingNames.Smtp.Port,
                    "25",
                    L ("DisplayName:Rocket.Mailing.Smtp.Port"),
                    L ("Description:Rocket.Mailing.Smtp.Port")),

                new SettingDefinition (
                    EmailSettingNames.Smtp.UserName,
                    displayName : L ("DisplayName:Rocket.Mailing.Smtp.UserName"),
                    description : L ("Description:Rocket.Mailing.Smtp.UserName")),

                new SettingDefinition (
                    EmailSettingNames.Smtp.Password,
                    displayName:
                    L ("DisplayName:Rocket.Mailing.Smtp.Password"),
                    description : L ("Description:Rocket.Mailing.Smtp.Password"),
                    isEncrypted : true),

                new SettingDefinition (
                    EmailSettingNames.Smtp.Domain,
                    displayName : L ("DisplayName:Rocket.Mailing.Smtp.Domain"),
                    description : L ("Description:Rocket.Mailing.Smtp.Domain")),

                new SettingDefinition (
                    EmailSettingNames.Smtp.EnableSsl,
                    "false",
                    L ("DisplayName:Rocket.Mailing.Smtp.EnableSsl"),
                    L ("Description:Rocket.Mailing.Smtp.EnableSsl")),

                new SettingDefinition (
                    EmailSettingNames.Smtp.UseDefaultCredentials,
                    "true",
                    L ("DisplayName:Rocket.Mailing.Smtp.UseDefaultCredentials"),
                    L ("Description:Rocket.Mailing.Smtp.UseDefaultCredentials")),

                new SettingDefinition (
                    EmailSettingNames.DefaultFromAddress,
                    "noreply@rocket.io",
                    L ("DisplayName:Rocket.Mailing.DefaultFromAddress"),
                    L ("Description:Rocket.Mailing.DefaultFromAddress")),

                new SettingDefinition (EmailSettingNames.DefaultFromDisplayName,
                    "ABP application",
                    L ("DisplayName:Rocket.Mailing.DefaultFromDisplayName"),
                    L ("Description:Rocket.Mailing.DefaultFromDisplayName"))
            );
        }

        private static LocalizableString L (string name) {
            return LocalizableString.Create<EmailingResource> (name);
        }
    }
}