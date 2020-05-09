using System;

namespace Aiwins.Docs.Documents
{
    [Serializable]
    public class DocumentUpdateInfo
    {
        public virtual string Name { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public virtual DateTime LastUpdatedTime { get; set; }

        public DateTime? LastSignificantUpdateTime { get; set; }
    }
}