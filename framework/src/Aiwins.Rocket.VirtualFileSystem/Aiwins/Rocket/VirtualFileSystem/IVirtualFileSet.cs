using System.Collections.Generic;
using Microsoft.Extensions.FileProviders;

namespace Aiwins.Rocket.VirtualFileSystem {
    public interface IVirtualFileSet {
        void AddFiles (Dictionary<string, IFileInfo> files);
    }
}