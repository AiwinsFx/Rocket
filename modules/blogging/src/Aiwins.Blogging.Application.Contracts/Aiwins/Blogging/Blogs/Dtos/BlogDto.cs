using System;
using Aiwins.Rocket.Application.Dtos;

namespace Aiwins.Blogging.Blogs.Dtos
{
    public class BlogDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }
    }
}