using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.MongoDB;
using Aiwins.Rocket.Users.MongoDB;
using Aiwins.Blogging.Blogs;
using Aiwins.Blogging.Comments;
using Aiwins.Blogging.Posts;
using Aiwins.Blogging.Tagging;
using Aiwins.Blogging.Users;

namespace Aiwins.Blogging.MongoDB
{
    [DependsOn(
        typeof(BloggingDomainModule),
        typeof(RocketMongoDbModule),
        typeof(RocketUsersMongoDbModule)
    )]
    public class BloggingMongoDbModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<BloggingMongoDbContext>(options =>
            {
                options.AddRepository<Blog, MongoBlogRepository>();
                options.AddRepository<BlogUser, MongoBlogUserRepository>();
                options.AddRepository<Post, MongoPostRepository>();
                options.AddRepository<Tag, MongoTagRepository>();
                options.AddRepository<Comment, MongoCommentRepository>();
            });
        }
    }
}
