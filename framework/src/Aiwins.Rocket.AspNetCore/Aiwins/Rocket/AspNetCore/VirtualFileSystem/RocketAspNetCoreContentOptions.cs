using System.Collections.Generic;

namespace Aiwins.Rocket.AspNetCore.VirtualFileSystem {
    public class RocketAspNetCoreContentOptions {
        public List<string> AllowedExtraWebContentFolders { get; }
        public List<string> AllowedExtraWebContentFileExtensions { get; }

        public RocketAspNetCoreContentOptions () {
            AllowedExtraWebContentFolders = new List<string> {
                "/Pages",
                "/Views",
                "/Themes"
            };

            AllowedExtraWebContentFileExtensions = new List<string> {
                ".js",
                ".css",
                ".png",
                ".jpg",
                ".jpeg",
                ".woff",
                ".woff2",
                ".tff",
                ".otf"
            };
        }
    }
}