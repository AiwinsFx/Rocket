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
    public class BloggingDbContext : RocketDbContext<BloggingDbContext>, IBloggingDbContext
    {
        public DbSet<BlogUser> Users { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<PostTag> PostTags { get; set; }

        public DbSet<Comment> Comments { get; set; }
        
        public BloggingDbContext(DbContextOptions<BloggingDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureBlogging();
        }
    }
}