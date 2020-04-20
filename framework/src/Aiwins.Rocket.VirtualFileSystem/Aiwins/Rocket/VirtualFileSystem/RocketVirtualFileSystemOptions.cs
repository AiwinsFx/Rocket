namespace Aiwins.Rocket.VirtualFileSystem {
    public class RocketVirtualFileSystemOptions {
        public VirtualFileSetList FileSets { get; }

        public RocketVirtualFileSystemOptions () {
            FileSets = new VirtualFileSetList ();
        }
    }
}