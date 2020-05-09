using System;
using System.Collections.Generic;

namespace Aiwins.Rocket.AspNetCore.Mvc.ApplicationConfigurations.ObjectExtending {
    [Serializable]
    public class ObjectExtensionsDto {
        public Dictionary<string, ModuleExtensionDto> Modules { get; set; }
    }
}