using JetBrains.Annotations;

namespace Aiwins.Rocket.Localization {
    public static class LocalizationSettingHelper {
        /// <summary>
        /// 获取语言设置信息
        /// </summary>
        /// <param name="settingValue"></param>
        /// <returns></returns>
        public static (string cultureName, string uiCultureName) ParseLanguageSetting ([NotNull] string settingValue) {
            Check.NotNull (settingValue, nameof (settingValue));

            if (!settingValue.Contains (";")) {
                return (settingValue, settingValue);
            }

            var splitted = settingValue.Split (';');
            return (splitted[0], splitted[1]);
        }
    }
}