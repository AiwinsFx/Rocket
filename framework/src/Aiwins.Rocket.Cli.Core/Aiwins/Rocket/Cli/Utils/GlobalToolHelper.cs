using System;
using System.IO;

namespace Aiwins.Rocket.Cli.Utils {
    public class GlobalToolHelper {
        /// <summary>
        /// Checks whether the tool is installed or not.
        /// </summary>
        /// <param name="toolCommandName">Eg: For RocketSuite tool it's "rocket-suite", for ROCKET CLI tool it's "rocket"</param>
        public static bool IsGlobalToolInstalled (string toolCommandName) {
            string suitePath;

            if (PlatformHelper.GetPlatform () == RuntimePlatform.LinuxOrMacOs) {
                suitePath = Environment
                    .ExpandEnvironmentVariables (
                        Path.Combine ("%HOME%", ".dotnet", "tools", toolCommandName)
                    );
            } else {
                suitePath = Environment
                    .ExpandEnvironmentVariables (
                        Path.Combine (@"%USERPROFILE%", ".dotnet", "tools", toolCommandName + ".exe")
                    );
            }

            return File.Exists (suitePath);
        }
    }
}