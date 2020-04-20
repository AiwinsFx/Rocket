using System;
using System.Collections.Generic;

namespace Aiwins.Rocket.Localization.ExceptionHandling {
    public class RocketExceptionLocalizationOptions {
        public Dictionary<string, Type> ErrorCodeNamespaceMappings { get; }

        public RocketExceptionLocalizationOptions () {
            ErrorCodeNamespaceMappings = new Dictionary<string, Type> ();
        }

        public void MapCodeNamespace (string errorCodeNamespace, Type type) {
            ErrorCodeNamespaceMappings[errorCodeNamespace] = type;
        }
    }
}