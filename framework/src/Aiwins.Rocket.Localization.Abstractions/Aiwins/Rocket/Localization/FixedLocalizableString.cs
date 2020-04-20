﻿using Microsoft.Extensions.Localization;

namespace Aiwins.Rocket.Localization {
    public class FixedLocalizableString : ILocalizableString {
        public string Value { get; }

        public FixedLocalizableString (string value) {
            Value = value;
        }

        public LocalizedString Localize (IStringLocalizerFactory stringLocalizerFactory) {
            return new LocalizedString (Value, Value);
        }
    }
}