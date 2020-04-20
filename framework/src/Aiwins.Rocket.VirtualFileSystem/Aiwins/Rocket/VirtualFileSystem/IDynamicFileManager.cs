using Microsoft.Extensions.FileProviders;

namespace Aiwins.Rocket.VirtualFileSystem {
    public interface IDynamicFileProvider : IFileProvider {
        void AddOrUpdate (IFileInfo fileInfo);

        bool Delete (string filePath);
    }
}