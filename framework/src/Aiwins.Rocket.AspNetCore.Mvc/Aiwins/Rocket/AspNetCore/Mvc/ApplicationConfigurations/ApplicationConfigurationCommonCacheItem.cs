using System;
using Aiwins.Rocket.AspNetCore.Mvc.ApplicationConfigurations.ObjectExtending;
using Aiwins.Rocket.AspNetCore.Mvc.MultiTenancy;
using JetBrains.Annotations;

namespace Aiwins.Rocket.AspNetCore.Mvc.ApplicationConfigurations {
    [Serializable]
    public class ApplicationCommonConfigurationCacheItem {
        public ApplicationLocalizationConfigurationDto Localization { get; set; }

        public ApplicationCommonConfigurationCacheItem () {

        }

        public static string CalculateCacheKey (string name = "CommonConfiguration") => name;
    }
}