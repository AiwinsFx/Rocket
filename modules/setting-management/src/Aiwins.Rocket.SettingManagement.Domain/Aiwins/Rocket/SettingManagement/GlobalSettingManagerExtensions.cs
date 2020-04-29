using System.Collections.Generic;
using System.Threading.Tasks;
using Aiwins.Rocket.Settings;
using JetBrains.Annotations;

namespace Aiwins.Rocket.SettingManagement {
    public static class GlobalSettingManagerExtensions {
        public static Task<string> GetOrNullGlobalAsync (this ISettingManager settingManager, [NotNull] string name, bool fallback = true) {
            return settingManager.GetOrNullAsync (name, GlobalSettingValueProvider.ProviderName, null, fallback);
        }

        public static Task<List<SettingValue>> GetAllGlobalAsync (this ISettingManager settingManager, bool fallback = true) {
            return settingManager.GetAllAsync (GlobalSettingValueProvider.ProviderName, null, fallback);
        }

        public static Task SetGlobalAsync (this ISettingManager settingManager, [NotNull] string name, [CanBeNull] string value) {
            return settingManager.SetAsync (name, value, GlobalSettingValueProvider.ProviderName, null);
        }
    }
}