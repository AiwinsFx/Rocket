using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Tab
{
    [HtmlTargetElement("abp-tab-link", TagStructure = TagStructure.WithoutEndTag)]
    public class RocketTabLinkTagHelper : RocketTagHelper<RocketTabLinkTagHelper, RocketTabLinkTagHelperService>
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public string ParentDropdownName { get; set; }

        public string Href { get; set; }

        public RocketTabLinkTagHelper(RocketTabLinkTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
