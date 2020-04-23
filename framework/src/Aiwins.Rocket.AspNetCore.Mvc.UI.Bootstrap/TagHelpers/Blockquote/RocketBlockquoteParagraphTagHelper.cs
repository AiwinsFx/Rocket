using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Blockquote
{
    [HtmlTargetElement("p", ParentTag = "rocket-blockquote")]
    public class RocketBlockquoteParagraphTagHelper : RocketTagHelper<RocketBlockquoteParagraphTagHelper, RocketBlockquoteParagraphTagHelperService>
    {
        public RocketBlockquoteParagraphTagHelper(RocketBlockquoteParagraphTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
