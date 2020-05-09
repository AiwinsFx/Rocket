using System;

namespace Aiwins.Rocket.AspNetCore.Mvc.ApplicationConfigurations.ObjectExtending {
    [Serializable]
    public class ExtensionPropertyApiCreateDto {
        /// <summary>
        /// Default: true.
        /// </summary>
        public bool IsAvailable { get; set; } = true;
    }
}