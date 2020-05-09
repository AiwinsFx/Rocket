using System;
using System.Collections.Generic;

namespace Aiwins.Rocket.AspNetCore.Mvc.ApplicationConfigurations {
    [Serializable]
    public class ApplicationSettingConfigurationDto {
        public Dictionary<string, string> Values { get; set; }
    }
}