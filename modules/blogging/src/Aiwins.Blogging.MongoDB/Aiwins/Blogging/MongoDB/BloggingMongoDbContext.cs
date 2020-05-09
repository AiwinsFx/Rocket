using MongoDB.Driver;
using Aiwins.Rocket.Data;
using Aiwins.Rocket.MongoDB;
using Aiwins.Blogging.Blogs;
using Aiwins.Blogging.Comments;
using Aiwins.Blogging.Posts;
using Aiwins.Blogging.Users;

namespace Aiwins.Blogging.MongoDB
{
    [ConnectionStringName(BloggingDbProperties.ConnectionStringName)]
    public class BloggingMongoDbContext : RocketMongoDbContext, IBloggingMongoDbContext
    {
        public IMongoCollection<BlogUser> Users => Collection<BlogUser>();

        public IMongoCollection<Blog> Blogs => Collection<Blog>();

        public IMongoCollection<Post> Posts => Collection<Post>();

        public IMongoCollection<Tagging.Tag> Tags => Collection<Tagging.Tag>();

        public IMongoCollection<Comment> Comments => Collection<Comment>();

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureBlogging();
        }
    }
}
