using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card
{
    [HtmlTargetElement("img", Attributes = "abp-card-image", TagStructure = TagStructure.WithoutEndTag)]
    [HtmlTargetElement("abp-image", Attributes = "abp-card-image", TagStructure = TagStructure.WithoutEndTag)]
    public class RocketCardImageTagHelper : RocketTagHelper<RocketCardImageTagHelper, RocketCardImageTagHelperService>
    {
        [HtmlAttributeName("abp-card-image")]
        public RocketCardImagePosition Position { get; set; } = RocketCardImagePosition.Top;

        public RocketCardImageTagHelper(RocketCardImageTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}