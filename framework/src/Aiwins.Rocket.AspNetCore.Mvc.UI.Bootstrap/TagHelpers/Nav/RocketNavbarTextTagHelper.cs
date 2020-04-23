using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Nav
{
    [HtmlTargetElement("span", Attributes = "abp-navbar-text")]
    public class RocketNavbarTextTagHelper : RocketTagHelper<RocketNavbarTextTagHelper, RocketNavbarTextTagHelperService>
    {
        public RocketNavbarTextTagHelper(RocketNavbarTextTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
