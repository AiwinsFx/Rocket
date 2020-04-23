using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Utils
{
    [HtmlTargetElement(Attributes = "rocket-if")]
    public class RocketIfTagHelper : RocketTagHelper
    {
        [HtmlAttributeName("rocket-if")]
        public bool Condition { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!Condition)
            {
                output.SuppressOutput();
            }
        }
    }
}
