using Microsoft.EntityFrameworkCore;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Blogging.Blogs;
using Aiwins.Blogging.Comments;
using Aiwins.Blogging.Posts;
using Aiwins.Blogging.Tagging;
using Aiwins.Blogging.Users;

namespace Aiwins.Blogging.EntityFrameworkCore
{
    [ConnectionStringName(BloggingDbProperties.ConnectionStringName)]
    public interface IBloggingDbContext : IEfCoreDbContext
    {
        DbSet<BlogUser> Users { get; }

        DbSet<Blog> Blogs { get; set; }

        DbSet<Post> Posts { get; set; }

        DbSet<Comment> Comments { get; set; }

        DbSet<PostTag> PostTags { get; set; }

        DbSet<Tag> Tags { get; set; }
    }
}