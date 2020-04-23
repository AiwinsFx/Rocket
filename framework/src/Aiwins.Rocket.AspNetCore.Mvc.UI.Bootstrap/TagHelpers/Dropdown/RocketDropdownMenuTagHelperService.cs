using Microsoft.AspNetCore.Razor.TagHelpers;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Dropdown
{
    public class RocketDropdownMenuTagHelperService : RocketTagHelperService<RocketDropdownMenuTagHelper>
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.AddClass("dropdown-menu");
            output.TagMode = TagMode.StartTagAndEndTag;

            SetAlign(context, output);
        }

        protected virtual void SetAlign(TagHelperContext context, TagHelperOutput output)
        {
            switch (TagHelper.Align)
            {
                case DropdownAlign.Right:
                    output.Attributes.AddClass("dropdown-menu-right");
                    return;
                case DropdownAlign.Left:
                    return;
            }
        }
    }
}