using System;
using Aiwins.Rocket.AspNetCore.Mvc.ApplicationConfigurations.ObjectExtending;
using Aiwins.Rocket.AspNetCore.Mvc.MultiTenancy;
using JetBrains.Annotations;

namespace Aiwins.Rocket.AspNetCore.Mvc.ApplicationConfigurations {
    [Serializable]
    public class ApplicationPersonalConfigurationCacheItem {
        public ApplicationAuthConfigurationDto Auth { get; set; }

        public ApplicationSettingConfigurationDto Setting { get; set; }

        public ApplicationFeatureConfigurationDto Features { get; set; }

        public ApplicationPersonalConfigurationCacheItem () {

        }

        public static string CalculateCacheKey (string tenantId, string userId, string name = "PersonalConfiguration") {
            if (tenantId.IsNullOrWhiteSpace () && userId.IsNullOrWhiteSpace ()) {
                return "ct:T,cu:U,n:" + name;
            }
            if (tenantId.IsNullOrWhiteSpace ()) {
                return "ct:T,cu:" + userId + ",n:" + name;
            }
            return "ct:" + tenantId + ",cu:" + userId + ",n:" + name;
        }
    }
}