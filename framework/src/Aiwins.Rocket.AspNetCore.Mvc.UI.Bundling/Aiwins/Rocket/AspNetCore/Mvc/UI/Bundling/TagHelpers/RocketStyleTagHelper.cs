using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling.TagHelpers
{
    [HtmlTargetElement("abp-style", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class RocketStyleTagHelper : RocketBundleItemTagHelper<RocketStyleTagHelper, RocketStyleTagHelperService>, IBundleItemTagHelper
    {
        public RocketStyleTagHelper(RocketStyleTagHelperService service)
            : base(service)
        {

        }

        protected override string GetFileExtension()
        {
            return "css";
        }
    }
}