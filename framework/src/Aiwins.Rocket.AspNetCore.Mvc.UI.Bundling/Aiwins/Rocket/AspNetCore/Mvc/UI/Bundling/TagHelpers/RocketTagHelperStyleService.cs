using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Aiwins.Rocket.AspNetCore.VirtualFileSystem;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling.TagHelpers
{
    public class RocketTagHelperStyleService : RocketTagHelperResourceService
    {
        public RocketTagHelperStyleService(
            IBundleManager bundleManager,
            IWebContentFileProvider webContentFileProvider,
            IOptions<RocketBundlingOptions> options,
            IWebHostEnvironment hostingEnvironment
            ) : base(
                bundleManager,
                webContentFileProvider,
                options,
                hostingEnvironment)
        {
        }

        protected override void CreateBundle(string bundleName, List<BundleTagHelperItem> bundleItems)
        {
            Options.StyleBundles.TryAdd(
                bundleName,
                configuration => bundleItems.ForEach(bi => bi.AddToConfiguration(configuration)),
                HostingEnvironment.IsDevelopment() && bundleItems.Any()
            );
        }

        protected override async Task<IReadOnlyList<string>> GetBundleFilesAsync(string bundleName)
        {
            return await BundleManager.GetStyleBundleFilesAsync(bundleName);
        }

        protected override void AddHtmlTag(TagHelperContext context, TagHelperOutput output, string file)
        {
            output.Content.AppendHtml($"<link rel=\"stylesheet\" href=\"{file}\" />{Environment.NewLine}");
        }
    }
}