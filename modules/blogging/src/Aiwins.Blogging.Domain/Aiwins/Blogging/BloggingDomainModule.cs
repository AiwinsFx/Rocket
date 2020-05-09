using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.AutoMapper;
using Aiwins.Rocket.Domain;
using Aiwins.Rocket.EventBus.Distributed;
using Aiwins.Rocket.Modularity;
using Aiwins.Blogging.Blogs;
using Aiwins.Blogging.Comments;
using Aiwins.Blogging.Posts;
using Aiwins.Blogging.Tagging;

namespace Aiwins.Blogging
{
    [DependsOn(
        typeof(BloggingDomainSharedModule),
        typeof(RocketDddDomainModule),
        typeof(RocketAutoMapperModule))]
    public class BloggingDomainModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<BloggingDomainModule>();

            Configure<RocketAutoMapperOptions>(options =>
            {
                options.AddProfile<BloggingDomainMappingProfile>(validate: true);
            });

            Configure<RocketDistributedEventBusOptions>(options =>
            {
                options.EtoMappings.Add<Blog, BlogEto>(typeof(BloggingDomainModule));
                options.EtoMappings.Add<Comment, CommentEto>(typeof(BloggingDomainModule));
                options.EtoMappings.Add<Post, PostEto>(typeof(BloggingDomainModule));
                options.EtoMappings.Add<Tag, TagEto>(typeof(BloggingDomainModule));
            });
        }
    }
}
