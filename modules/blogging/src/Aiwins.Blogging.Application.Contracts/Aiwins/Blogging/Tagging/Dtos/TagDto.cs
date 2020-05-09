using System;
using Aiwins.Rocket.Application.Dtos;

namespace Aiwins.Blogging.Tagging.Dtos
{
    public class TagDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int UsageCount { get; set; }
    }
}