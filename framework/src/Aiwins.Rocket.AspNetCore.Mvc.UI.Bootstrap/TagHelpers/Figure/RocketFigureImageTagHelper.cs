using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Figure
{
    [HtmlTargetElement("abp-image", ParentTag = "abp-figure")]
    public class RocketFigureImageTagHelper : RocketTagHelper<RocketFigureImageTagHelper, RocketFigureImageTagHelperService>
    {
        public RocketFigureImageTagHelper(RocketFigureImageTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
