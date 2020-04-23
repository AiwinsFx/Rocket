using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card
{
    [HtmlTargetElement("a", Attributes = "abp-card-link")]
    public class RocketCardLinkTagHelper : RocketTagHelper<RocketCardLinkTagHelper, RocketCardLinkTagHelperService>
    {
        public RocketCardLinkTagHelper(RocketCardLinkTagHelperService tagHelperService)
            : base(tagHelperService)
        {
         
        }
    }
}