﻿using System;
using System.Collections.Generic;

namespace Aiwins.Rocket.AspNetCore.Mvc.ApplicationConfigurations.ObjectExtending {
    [Serializable]
    public class ExtensionPropertyAttributeDto {
        public string TypeSimple { get; set; }

        public Dictionary<string, object> Config { get; set; }
    }
}