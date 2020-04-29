using System.Collections.Generic;
using System.Threading.Tasks;
using Aiwins.Rocket.Settings;
using JetBrains.Annotations;

namespace Aiwins.Rocket.SettingManagement {
    public static class DefaultValueSettingManagerExtensions {
        public static Task<string> GetOrNullDefaultAsync (this ISettingManager settingManager, [NotNull] string name, bool fallback = true) {
            return settingManager.GetOrNullAsync (name, DefaultValueSettingValueProvider.ProviderName, null, fallback);
        }

        public static Task<List<SettingValue>> GetAllDefaultAsync (this ISettingManager settingManager, bool fallback = true) {
            return settingManager.GetAllAsync (DefaultValueSettingValueProvider.ProviderName, null, fallback);
        }
    }
}