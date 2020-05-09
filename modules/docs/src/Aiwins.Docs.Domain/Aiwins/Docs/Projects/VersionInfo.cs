using System;

namespace Aiwins.Docs.Projects
{
    [Serializable] //Serialization needed because this object is stored in cache
    public class VersionInfo
    {
        public string DisplayName { get; set; }

        public string Name { get; set; }
    }
}