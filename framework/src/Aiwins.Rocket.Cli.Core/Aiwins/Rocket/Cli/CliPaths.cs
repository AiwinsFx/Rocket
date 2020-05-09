using System;
using System.IO;

namespace Aiwins.Rocket.Cli
{
    public static class CliPaths
    {
        public static string TemplateCache => Path.Combine(RocketRootPath, "templates"); //TODO: Move somewhere else?
        public static string Log => Path.Combine(RocketRootPath, "cli", "logs");
        public static string Root => Path.Combine(RocketRootPath, "cli");
        public static string AccessToken => Path.Combine(RocketRootPath, "cli", "access-token.bin");

        private static readonly string RocketRootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".rocket");
    }
}   
