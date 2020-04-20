using System;

namespace Aiwins.Rocket.Settings {
    [Serializable]
    public class SettingValue : NameValue {
        public SettingValue () {

        }

        public SettingValue (string name, string value) {
            Name = name;
            Value = value;
        }
    }
}