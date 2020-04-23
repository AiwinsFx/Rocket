using Microsoft.AspNetCore.Razor.TagHelpers;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Figure
{
    public class RocketFigureCaptionTagHelperService : RocketTagHelperService<RocketFigureCaptionTagHelper>
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "figcaption";
            output.Attributes.AddClass("figure-caption");
        }
    }
}