using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Aiwins.Rocket.AspNetCore.Mvc.Localization;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.Prismjs;
using Aiwins.Rocket.AutoMapper;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.UI.Navigation;
using Aiwins.Rocket.VirtualFileSystem;
using Aiwins.Blogging.Bundling;
using Aiwins.Blogging.Localization;

namespace Aiwins.Blogging
{
    [DependsOn(
        typeof(BloggingHttpApiModule),
        typeof(RocketAspNetCoreMvcUiBootstrapModule),
        typeof(RocketAspNetCoreMvcUiBundlingModule),
        typeof(RocketAutoMapperModule)
    )]
    public class BloggingWebModule : RocketModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<RocketMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(BloggingResource), typeof(BloggingWebModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(BloggingWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new BloggingMenuContributor());
            });

            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<BloggingWebModule>("Aiwins.Blogging");
            });

            context.Services.AddAutoMapperObjectMapper<BloggingWebModule>();
            Configure<RocketAutoMapperOptions>(options =>
            {
                options.AddProfile<RocketBloggingWebAutoMapperProfile>(validate: true);
            });

            Configure<RocketBundleContributorOptions>(options =>
            {
                options
                    .Extensions<PrismjsStyleBundleContributor>()
                    .Add<PrismjsStyleBundleContributorBloggingExtension>();

                options
                    .Extensions<PrismjsScriptBundleContributor>()
                    .Add<PrismjsScriptBundleContributorBloggingExtension>();
            });

            Configure<RazorPagesOptions>(options =>
            {
                var urlOptions = context.Services
                    .GetRequiredServiceLazy<IOptions<BloggingUrlOptions>>()
                    .Value.Value;

                var routePrefix = urlOptions.RoutePrefix;

                options.Conventions.AddPageRoute("/Blogs/Posts/Index", routePrefix + "{blogShortName}");
                options.Conventions.AddPageRoute("/Blogs/Posts/Detail", routePrefix + "{blogShortName}/{postUrl}");
                options.Conventions.AddPageRoute("/Blogs/Posts/Edit", routePrefix + "{blogShortName}/posts/{postId}/edit");
                options.Conventions.AddPageRoute("/Blogs/Posts/New", routePrefix + "{blogShortName}/posts/new");
            });
        }
    }
}
