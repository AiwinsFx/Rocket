using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Tab
{
    [HtmlTargetElement("rocket-tab-dropdown")]
    public class RocketTabDropdownTagHelper : RocketTagHelper<RocketTabDropdownTagHelper, RocketTabDropdownTagHelperService>
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public RocketTabDropdownTagHelper(RocketTabDropdownTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
