using System;
using Aiwins.Rocket.AspNetCore.Mvc.MultiTenancy;

namespace Aiwins.Rocket.AspNetCore.Mvc.ApplicationConfigurations {
    [Serializable]
    public class ApplicationConfigurationDto {
        public ApplicationLocalizationConfigurationDto Localization { get; set; }

        public ApplicationAuthConfigurationDto Auth { get; set; }

        public ApplicationSettingConfigurationDto Setting { get; set; }

        public CurrentUserDto CurrentUser { get; set; }

        public ApplicationFeatureConfigurationDto Features { get; set; }

        public MultiTenancyInfoDto MultiTenancy { get; set; }

        public CurrentTenantDto CurrentTenant { get; set; }
    }
}