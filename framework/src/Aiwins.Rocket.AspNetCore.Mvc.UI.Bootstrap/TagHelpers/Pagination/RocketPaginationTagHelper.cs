using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Pagination
{
    [HtmlTargetElement("rocket-paginator")]
    public class RocketPaginationTagHelper : RocketTagHelper<RocketPaginationTagHelper, RocketPaginationTagHelperService>
    {
        public PagerModel Model { get; set; }

        public bool? ShowInfo { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public RocketPaginationTagHelper(RocketPaginationTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
