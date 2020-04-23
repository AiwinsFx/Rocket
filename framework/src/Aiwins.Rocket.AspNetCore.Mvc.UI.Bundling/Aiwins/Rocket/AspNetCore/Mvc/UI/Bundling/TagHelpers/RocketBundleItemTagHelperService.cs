using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling.TagHelpers
{
    public abstract class RocketBundleItemTagHelperService<TTagHelper> : RocketTagHelperService<TTagHelper>
        where TTagHelper : TagHelper, IBundleItemTagHelper
    {
        protected RocketTagHelperResourceService ResourceService { get; }

        protected RocketBundleItemTagHelperService(RocketTagHelperResourceService resourceService)
        {
            ResourceService = resourceService;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var tagHelperItems = context.Items.GetOrDefault(RocketTagHelperConsts.ContextBundleItemListKey) as List<BundleTagHelperItem>;
            if (tagHelperItems != null)
            {
                output.SuppressOutput();
                tagHelperItems.Add(TagHelper.CreateBundleTagHelperItem());
            }
            else
            {
                await ResourceService.ProcessAsync(
                    context,
                    output,
                    new List<BundleTagHelperItem>
                    {
                        TagHelper.CreateBundleTagHelperItem()
                    },
                    TagHelper.GetNameOrNull()
                );
            }
        }
    }
}