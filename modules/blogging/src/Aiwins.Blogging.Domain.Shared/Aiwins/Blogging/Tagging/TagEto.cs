using System;
using JetBrains.Annotations;

namespace Aiwins.Blogging.Tagging
{
    [Serializable]
    public class TagEto
    {
        public Guid Id { get; set; }

        public Guid BlogId { get; set; }

        [NotNull]
        public string Name { get; set; }

        public string Description { get; set; }

        public int UsageCount { get; set; }
    }
}