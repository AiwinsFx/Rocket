using System;

namespace Aiwins.Docs.Utils
{
    public static class UrlHelper
    {
        public static bool IsExternalLink(string path)
        {
            if (path.IsNullOrEmpty())
            {
                return false;
            }

            return path.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                   path.StartsWith("https://", StringComparison.OrdinalIgnoreCase);
        }
    }
}
