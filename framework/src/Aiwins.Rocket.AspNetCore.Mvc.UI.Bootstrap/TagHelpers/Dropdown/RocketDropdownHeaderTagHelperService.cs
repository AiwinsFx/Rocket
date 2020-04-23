using Microsoft.AspNetCore.Razor.TagHelpers;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Dropdown
{
    public class RocketDropdownHeaderTagHelperService : RocketTagHelperService<RocketDropdownHeaderTagHelper>
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "h6";
            output.Attributes.AddClass("dropdown-header");
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}