using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card
{
    [HtmlTargetElement("img", Attributes = "rocket-card-image", TagStructure = TagStructure.WithoutEndTag)]
    [HtmlTargetElement("rocket-image", Attributes = "rocket-card-image", TagStructure = TagStructure.WithoutEndTag)]
    public class RocketCardImageTagHelper : RocketTagHelper<RocketCardImageTagHelper, RocketCardImageTagHelperService>
    {
        [HtmlAttributeName("rocket-card-image")]
        public RocketCardImagePosition Position { get; set; } = RocketCardImagePosition.Top;

        public RocketCardImageTagHelper(RocketCardImageTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}