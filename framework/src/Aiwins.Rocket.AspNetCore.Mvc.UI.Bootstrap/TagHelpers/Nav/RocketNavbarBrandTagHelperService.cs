using Microsoft.AspNetCore.Razor.TagHelpers;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Nav
{
    public class RocketNavbarBrandTagHelperService : RocketTagHelperService<RocketNavbarBrandTagHelper>
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll("abp-navbar-brand");
            output.Attributes.AddClass("navbar-brand");
        }
    }
}