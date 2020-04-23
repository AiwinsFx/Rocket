using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Blockquote
{
    [HtmlTargetElement("footer", ParentTag = "abp-blockquote")]
    public class RocketBlockquoteFooterTagHelper : RocketTagHelper<RocketBlockquoteFooterTagHelper, RocketBlockquoteFooterTagHelperService>
    {
        public RocketBlockquoteFooterTagHelper(RocketBlockquoteFooterTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
