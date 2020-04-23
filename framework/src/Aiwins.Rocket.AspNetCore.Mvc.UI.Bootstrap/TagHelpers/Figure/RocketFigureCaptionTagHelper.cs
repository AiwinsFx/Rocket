using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Figure
{
    [HtmlTargetElement("rocket-figcaption")]
    public class RocketFigureCaptionTagHelper : RocketTagHelper<RocketFigureCaptionTagHelper, RocketFigureCaptionTagHelperService>
    {
        public RocketFigureCaptionTagHelper(RocketFigureCaptionTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
