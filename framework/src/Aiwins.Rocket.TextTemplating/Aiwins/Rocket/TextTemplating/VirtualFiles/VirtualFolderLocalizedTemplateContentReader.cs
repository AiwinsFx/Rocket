﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aiwins.Rocket.VirtualFileSystem;
using Microsoft.Extensions.FileProviders;

namespace Aiwins.Rocket.TextTemplating.VirtualFiles {
    public class VirtualFolderLocalizedTemplateContentReader : ILocalizedTemplateContentReader {
        private Dictionary<string, string> _dictionary;

        public async Task ReadContentsAsync (
            IVirtualFileProvider virtualFileProvider,
            string virtualPath) {
            _dictionary = new Dictionary<string, string> ();

            var directoryInfo = virtualFileProvider.GetFileInfo (virtualPath);
            if (!directoryInfo.IsDirectory) {
                throw new RocketException ("Given virtual path is not a folder: " + virtualPath);
            }

            foreach (var file in virtualFileProvider.GetDirectoryContents (virtualPath)) {
                if (file.IsDirectory) {
                    continue;
                }

                _dictionary.Add (file.Name.RemovePostFix (".tpl"), await file.ReadAsStringAsync ());
            }
        }

        public string GetContentOrNull (string cultureName) {
            if (cultureName == null) {
                return null;
            }

            return _dictionary.GetOrDefault (cultureName);
        }
    }
}