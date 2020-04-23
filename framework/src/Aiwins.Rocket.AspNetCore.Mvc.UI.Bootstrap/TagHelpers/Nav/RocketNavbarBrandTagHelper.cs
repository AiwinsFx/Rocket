using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Nav
{
    [HtmlTargetElement(Attributes = "abp-navbar-brand")]
    public class RocketNavbarBrandTagHelper : RocketTagHelper<RocketNavbarBrandTagHelper, RocketNavbarBrandTagHelperService>
    {

        public RocketNavbarBrandTagHelper(RocketNavbarBrandTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
