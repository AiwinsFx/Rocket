using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling.TagHelpers
{
    [HtmlTargetElement("rocket-script-bundle", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class RocketScriptBundleTagHelper : RocketBundleTagHelper<RocketScriptBundleTagHelper, RocketScriptBundleTagHelperService>, IBundleTagHelper
    {
        public RocketScriptBundleTagHelper(RocketScriptBundleTagHelperService service)
            : base(service)
        {

        }
    }
}