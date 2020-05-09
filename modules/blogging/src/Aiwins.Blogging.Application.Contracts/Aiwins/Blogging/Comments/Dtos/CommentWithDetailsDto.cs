using System;
using Aiwins.Rocket.Application.Dtos;
using Aiwins.Blogging.Posts;

namespace Aiwins.Blogging.Comments.Dtos
{
    public class CommentWithDetailsDto : FullAuditedEntityDto<Guid>
    {
        public Guid? RepliedCommentId { get; set; }

        public string Text { get; set; }

        public BlogUserDto Writer { get; set; }
    }
}
