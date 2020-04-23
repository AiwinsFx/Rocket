using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Utils
{
    [HtmlTargetElement(Attributes = "rocket-auto-focus")]
    public class RocketAutoFocusTagHelper : RocketTagHelper
    {
        [HtmlAttributeName("rocket-auto-focus")]
        public bool AutoFocus { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (AutoFocus)
            {
                output.Attributes.Add("data-auto-focus", "true");
            }
        }
    }
}