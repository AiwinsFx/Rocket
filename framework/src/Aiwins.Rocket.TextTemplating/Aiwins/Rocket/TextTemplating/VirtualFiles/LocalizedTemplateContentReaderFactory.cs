﻿using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.VirtualFileSystem;
using Nito.AsyncEx;

namespace Aiwins.Rocket.TextTemplating.VirtualFiles {
    public class LocalizedTemplateContentReaderFactory : ILocalizedTemplateContentReaderFactory, ISingletonDependency {
        protected IVirtualFileProvider VirtualFileProvider { get; }
        protected ConcurrentDictionary<string, ILocalizedTemplateContentReader> ReaderCache { get; }
        protected SemaphoreSlim SyncObj;

        public LocalizedTemplateContentReaderFactory (IVirtualFileProvider virtualFileProvider) {
            VirtualFileProvider = virtualFileProvider;
            ReaderCache = new ConcurrentDictionary<string, ILocalizedTemplateContentReader> ();
            SyncObj = new SemaphoreSlim (1, 1);
        }

        public virtual async Task<ILocalizedTemplateContentReader> CreateAsync (TemplateDefinition templateDefinition) {
            if (ReaderCache.TryGetValue (templateDefinition.Name, out var reader)) {
                return reader;
            }

            using (await SyncObj.LockAsync ()) {
                if (ReaderCache.TryGetValue (templateDefinition.Name, out reader)) {
                    return reader;
                }

                reader = await CreateInternalAsync (templateDefinition);
                ReaderCache[templateDefinition.Name] = reader;
                return reader;
            }
        }

        protected virtual async Task<ILocalizedTemplateContentReader> CreateInternalAsync (
            TemplateDefinition templateDefinition) {
            var virtualPath = templateDefinition.GetVirtualFilePathOrNull ();
            if (virtualPath == null) {
                return NullLocalizedTemplateContentReader.Instance;
            }

            var fileInfo = VirtualFileProvider.GetFileInfo (virtualPath);
            if (!fileInfo.Exists) {
                throw new RocketException ("Could not find a file/folder at the location: " + virtualPath);
            }

            if (fileInfo.IsDirectory) {
                var folderReader = new VirtualFolderLocalizedTemplateContentReader ();
                await folderReader.ReadContentsAsync (VirtualFileProvider, virtualPath);
                return folderReader;
            } else //File
            {
                var singleFileReader = new FileInfoLocalizedTemplateContentReader ();
                await singleFileReader.ReadContentsAsync (fileInfo);
                return singleFileReader;
            }
        }
    }
}