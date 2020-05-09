using System.IO;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Cli.Utils
{
    public static class PathHelper
    {
        public static string NormalizePath([CanBeNull] string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }

            if (Path.IsPathRooted(path))
            {
                return path;
            }

            return Path.Combine(Directory.GetCurrentDirectory(), path);
        }
    }
}
