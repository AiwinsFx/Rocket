using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Tab
{
    [HtmlTargetElement("rocket-tab")]
    public class RocketTabTagHelper : RocketTagHelper<RocketTabTagHelper, RocketTabTagHelperService>
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public bool? Active { get; set; }

        public string ParentDropdownName { get; set; }

        public RocketTabTagHelper(RocketTabTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
