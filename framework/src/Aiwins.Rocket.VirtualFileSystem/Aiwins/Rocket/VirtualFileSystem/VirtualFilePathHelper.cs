using System;
using System.Collections.Generic;
using System.Linq;

namespace Aiwins.Rocket.VirtualFileSystem {
    internal static class VirtualFilePathHelper {
        //TODO: 性能优化!

        public static string NormalizePath (string fullPath) {
            var fileName = fullPath;
            var extension = "";

            if (fileName.Contains (".")) {
                extension = fullPath.Substring (fileName.LastIndexOf (".", StringComparison.Ordinal));
                if (extension.Contains ("/")) {
                    //清除目录扩展名
                    extension = "";
                } else {
                    fileName = fullPath.Substring (0, fullPath.Length - extension.Length);
                }
            }

            return NormalizeChars (fileName) + extension;
        }

        private static string NormalizeChars (string fileName) {
            var folderParts = fileName.Replace (".", "/").Split ("/");

            if (folderParts.Length == 1) {
                return folderParts[0];
            }

            return folderParts.Take (folderParts.Length - 1).Select (s => s.Replace ("-", "_")).JoinAsString ("/") + "/" + folderParts.Last ();
        }
    }
}