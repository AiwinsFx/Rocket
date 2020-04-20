﻿using System.Collections.Generic;

namespace Aiwins.Rocket.IdentityModel {
    public class IdentityClientConfigurationDictionary : Dictionary<string, IdentityClientConfiguration> {
        public const string DefaultName = "Default";

        public IdentityClientConfiguration Default {
            get => this.GetOrDefault (DefaultName);
            set => this [DefaultName] = value;
        }
    }
}