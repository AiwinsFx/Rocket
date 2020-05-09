using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Aiwins.Rocket.AutoMapper;
using Aiwins.Rocket.Caching;
using Aiwins.Rocket.Modularity;
using Aiwins.Blogging.Comments;
using Aiwins.Blogging.Posts;

namespace Aiwins.Blogging
{
    [DependsOn(
        typeof(BloggingDomainModule),
        typeof(BloggingApplicationContractsModule),
        typeof(RocketCachingModule),
        typeof(RocketAutoMapperModule))]
    public class BloggingApplicationModule : RocketModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<BloggingApplicationModule>();
            Configure<RocketAutoMapperOptions>(options =>
            {
                options.AddProfile<BloggingApplicationAutoMapperProfile>(validate: true);
            });

            Configure<AuthorizationOptions>(options =>
            {
                //TODO: Rename UpdatePolicy/DeletePolicy since it's candidate to conflicts with other modules!
                options.AddPolicy("BloggingUpdatePolicy", policy => policy.Requirements.Add(CommonOperations.Update));
                options.AddPolicy("BloggingDeletePolicy", policy => policy.Requirements.Add(CommonOperations.Delete));
            });

            context.Services.AddSingleton<IAuthorizationHandler, CommentAuthorizationHandler>();
            context.Services.AddSingleton<IAuthorizationHandler, PostAuthorizationHandler>();

        }
    }
}
