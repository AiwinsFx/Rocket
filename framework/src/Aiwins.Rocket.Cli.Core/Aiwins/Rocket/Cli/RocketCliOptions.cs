using System;
using System.Collections.Generic;

namespace Aiwins.Rocket.Cli {
    public class RocketCliOptions {
        public Dictionary<string, Type> Commands { get; }

        /// <summary>
        /// Default value: true.
        /// </summary>
        public bool CacheTemplates { get; set; } = true;

        /// <summary>
        /// Default value: "CLI".
        /// </summary>
        public string ToolName { get; set; } = "CLI";

        public RocketCliOptions () {
            Commands = new Dictionary<string, Type> (StringComparer.OrdinalIgnoreCase);
        }
    }
}