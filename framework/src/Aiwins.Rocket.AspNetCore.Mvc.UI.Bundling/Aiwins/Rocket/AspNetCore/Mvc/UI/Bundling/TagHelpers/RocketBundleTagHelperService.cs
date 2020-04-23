using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling.TagHelpers
{
    public abstract class RocketBundleTagHelperService<TTagHelper> : RocketTagHelperService<TTagHelper>
        where TTagHelper : TagHelper, IBundleTagHelper
    {
        protected RocketTagHelperResourceService ResourceService { get; }

        protected RocketBundleTagHelperService(RocketTagHelperResourceService resourceService)
        {
            ResourceService = resourceService;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await ResourceService.ProcessAsync(
                context,
                output,
                await GetBundleItems(context, output),
                TagHelper.GetNameOrNull()
            );
        }

        protected virtual async Task<List<BundleTagHelperItem>> GetBundleItems(TagHelperContext context, TagHelperOutput output)
        {
            var bundleItems = new List<BundleTagHelperItem>();
            context.Items[RocketTagHelperConsts.ContextBundleItemListKey] = bundleItems;
            await output.GetChildContentAsync(); //TODO: Is there a way of executing children without getting content?
            return bundleItems;
        }
    }
}