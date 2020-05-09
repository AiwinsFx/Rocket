using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Aiwins.Rocket.AspNetCore.Mvc.Localization;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.Prismjs;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Theme.Shared;
using Aiwins.Rocket.AutoMapper;
using Aiwins.Rocket.Modularity;
using Aiwins.Rocket.VirtualFileSystem;
using Aiwins.Docs.Bundling;
using Aiwins.Docs.HtmlConverting;
using Aiwins.Docs.Localization;
using Aiwins.Docs.Markdown;

namespace Aiwins.Docs
{
    [DependsOn(
        typeof(DocsHttpApiModule),
        typeof(RocketAutoMapperModule),
        typeof(RocketAspNetCoreMvcUiBootstrapModule),
        typeof(RocketAspNetCoreMvcUiThemeSharedModule),
        typeof(RocketAspNetCoreMvcUiPackagesModule),
        typeof(RocketAspNetCoreMvcUiBundlingModule)
        )]
    public class DocsWebModule : RocketModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<RocketMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(DocsResource), typeof(DocsWebModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(DocsWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<RocketVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<DocsWebModule>("Aiwins.Docs");
            });

            var configuration = context.Services.GetConfiguration();

            Configure<RazorPagesOptions>(options =>
            {
                var docsOptions = context.Services
                    .GetRequiredServiceLazy<IOptions<DocsUiOptions>>()
                    .Value.Value;

                var routePrefix = docsOptions.RoutePrefix;

                options.Conventions.AddPageRoute("/Documents/Project/Index", routePrefix + "{projectName}");
                options.Conventions.AddPageRoute("/Documents/Project/Index", routePrefix + "{languageCode}/{projectName}");
                options.Conventions.AddPageRoute("/Documents/Project/Index", routePrefix + "{languageCode}/{projectName}/{version}/{*documentName}");
                options.Conventions.AddPageRoute("/Documents/Search", routePrefix + "search/{languageCode}/{projectName}/{version}/{*keyword}");
            });

            context.Services.AddAutoMapperObjectMapper<DocsWebModule>();
            Configure<RocketAutoMapperOptions>(options =>
            {
                options.AddProfile<DocsWebAutoMapperProfile>(validate: true);
            });

            Configure<DocumentToHtmlConverterOptions>(options =>
            {
                options.Converters[MarkdownDocumentToHtmlConverter.Type] = typeof(MarkdownDocumentToHtmlConverter);
            });

            Configure<RocketBundleContributorOptions>(options =>
            {
                options
                    .Extensions<PrismjsStyleBundleContributor>()
                    .Add<PrismjsStyleBundleContributorDocsExtension>();

                options
                    .Extensions<PrismjsScriptBundleContributor>()
                    .Add<PrismjsScriptBundleContributorDocsExtension>();
            });
        }
    }
}
