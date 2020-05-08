using Aiwins.Rocket.Account.Localization;
using Aiwins.Rocket.Localization;
using Aiwins.Rocket.Settings;

namespace Aiwins.Rocket.Account.Settings {
    public class AccountSettingDefinitionProvider : SettingDefinitionProvider {
        public override void Define (ISettingDefinitionContext context) {
            context.Add (
                new SettingDefinition (
                    AccountSettingNames.IsSelfRegistrationEnabled,
                    "true",
                    L ("DisplayName:Rocket.Account.IsSelfRegistrationEnabled"),
                    L ("Description:Rocket.Account.IsSelfRegistrationEnabled"), isVisibleToClients : true)
            );

            context.Add (
                new SettingDefinition (
                    AccountSettingNames.EnableLocalLogin,
                    "true",
                    L ("DisplayName:Rocket.Account.EnableLocalLogin"),
                    L ("Description:Rocket.Account.EnableLocalLogin"), isVisibleToClients : true)
            );
        }

        private static LocalizableString L (string name) {
            return LocalizableString.Create<AccountResource> (name);
        }
    }
}