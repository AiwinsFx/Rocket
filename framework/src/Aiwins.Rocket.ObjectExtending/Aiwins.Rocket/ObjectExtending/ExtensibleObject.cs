using System;
using System.Collections.Generic;
using Aiwins.Rocket.Data;

namespace Aiwins.Rocket.ObjectExtending {
    [Serializable]
    public class ExtensibleObject : IHasExtraProperties {
        public Dictionary<string, object> ExtraProperties { get; protected set; }

        public ExtensibleObject () {
            ExtraProperties = new Dictionary<string, object> ();
        }
    }
}