using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling.TagHelpers
{
    [HtmlTargetElement("abp-style-bundle", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class RocketStyleBundleTagHelper : RocketBundleTagHelper<RocketStyleBundleTagHelper, RocketStyleBundleTagHelperService>, IBundleTagHelper
    {
        public RocketStyleBundleTagHelper(RocketStyleBundleTagHelperService service)
            : base(service)
        {
        }
    }
}
