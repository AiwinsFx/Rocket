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
    public interface IBloggingMongoDbContext : IRocketMongoDbContext
    {
        IMongoCollection<BlogUser> Users { get; }

        IMongoCollection<Blog> Blogs { get; }

        IMongoCollection<Post> Posts { get; }

        IMongoCollection<Tagging.Tag> Tags { get; }

        IMongoCollection<Comment> Comments { get; }

    }
}