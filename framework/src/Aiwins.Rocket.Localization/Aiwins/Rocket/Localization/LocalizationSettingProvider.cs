using Aiwins.Rocket.Localization.Resources.RocketLocalization;
using Aiwins.Rocket.Settings;

namespace Aiwins.Rocket.Localization {
    public class LocalizationSettingProvider : SettingDefinitionProvider {
        public override void Define (ISettingDefinitionContext context) {
            context.Add (
                new SettingDefinition (LocalizationSettingNames.DefaultLanguage,
                    "en",
                    L ("DisplayName:Rocket.Localization.DefaultLanguage"),
                    L ("Description:Rocket.Localization.DefaultLanguage"),
                    isVisibleToClients : true)
            );
        }

        private static LocalizableString L (string name) {
            return LocalizableString.Create<RocketLocalizationResource> (name);
        }
    }
}