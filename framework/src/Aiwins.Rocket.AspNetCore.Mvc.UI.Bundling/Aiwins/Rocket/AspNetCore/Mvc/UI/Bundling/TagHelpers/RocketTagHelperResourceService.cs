using JetBrains.Annotations;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Aiwins.Rocket.AspNetCore.VirtualFileSystem;
using Aiwins.Rocket.DependencyInjection;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling.TagHelpers
{
    public abstract class RocketTagHelperResourceService : ITransientDependency
    {
        public ILogger<RocketTagHelperResourceService> Logger { get; set; }
        protected IBundleManager BundleManager { get; }
        protected IWebContentFileProvider WebContentFileProvider { get; }
        protected IWebHostEnvironment HostingEnvironment { get; }
        protected readonly RocketBundlingOptions Options;
        
        protected RocketTagHelperResourceService(
            IBundleManager bundleManager,
            IWebContentFileProvider webContentFileProvider,
            IOptions<RocketBundlingOptions> options,
            IWebHostEnvironment hostingEnvironment)
        {
            BundleManager = bundleManager;
            WebContentFileProvider = webContentFileProvider;
            HostingEnvironment = hostingEnvironment;
            Options = options.Value;

            Logger = NullLogger<RocketTagHelperResourceService>.Instance;
        }

        public virtual async Task ProcessAsync(
            [NotNull] TagHelperContext context,
            [NotNull] TagHelperOutput output,
            [NotNull] List<BundleTagHelperItem> bundleItems,
            [CanBeNull] string bundleName = null)
        {
            Check.NotNull(context, nameof(context));
            Check.NotNull(output, nameof(output));
            Check.NotNull(bundleItems, nameof(bundleItems));

            var stopwatch = Stopwatch.StartNew();

            output.TagName = null;

            if (bundleName.IsNullOrEmpty())
            {
                bundleName = GenerateBundleName(bundleItems);
            }

            CreateBundle(bundleName, bundleItems);

            var bundleFiles = await GetBundleFilesAsync(bundleName);

            output.Content.Clear();

            foreach (var bundleFile in bundleFiles)
            {
                var file = WebContentFileProvider.GetFileInfo(bundleFile);

                if (file == null || !file.Exists)
                {
                    throw new RocketException($"Could not find the bundle file '{bundleFile}' from {nameof(IWebContentFileProvider)}");
                }

                AddHtmlTag(context, output, bundleFile + "?_v=" + file.LastModified.UtcTicks);
            }

            stopwatch.Stop();
            Logger.LogDebug($"Added bundle '{bundleName}' to the page in {stopwatch.Elapsed.TotalMilliseconds:0.00} ms.");
        }

        protected abstract void CreateBundle(string bundleName, List<BundleTagHelperItem> bundleItems);

        protected abstract Task<IReadOnlyList<string>> GetBundleFilesAsync(string bundleName);

        protected abstract void AddHtmlTag(TagHelperContext context, TagHelperOutput output, string file);

        protected virtual string GenerateBundleName(List<BundleTagHelperItem> bundleItems)
        {
            return bundleItems.JoinAsString("|").ToMd5();
        }
    }
}