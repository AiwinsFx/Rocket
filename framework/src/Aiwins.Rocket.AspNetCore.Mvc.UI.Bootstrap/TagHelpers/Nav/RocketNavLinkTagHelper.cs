using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Nav
{
    [HtmlTargetElement(Attributes = "abp-nav-link")]
    public class RocketNavLinkTagHelper : RocketTagHelper<RocketNavLinkTagHelper, RocketNavLinkTagHelperService>
    {
        public bool? Active { get; set; } 

        public bool? Disabled { get; set; }
        
        public RocketNavLinkTagHelper(RocketNavLinkTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
