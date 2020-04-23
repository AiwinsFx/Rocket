using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling.TagHelpers
{
    [HtmlTargetElement("abp-script", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class RocketScriptTagHelper : RocketBundleItemTagHelper<RocketScriptTagHelper, RocketScriptTagHelperService>, IBundleItemTagHelper
    {
        public RocketScriptTagHelper(RocketScriptTagHelperService service)
            : base(service)
        {

        }

        protected override string GetFileExtension()
        {
            return "js";
        }
    }
}