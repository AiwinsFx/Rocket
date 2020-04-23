using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Table
{
    [HtmlTargetElement("th")]
    public class RocketTableHeadScopeTagHelper : RocketTagHelper<RocketTableHeadScopeTagHelper, RocketTableHeadScopeTagHelperService>
    {
        public RocketThScope Scope { get; set; } = RocketThScope.Default;

        public RocketTableHeadScopeTagHelper(RocketTableHeadScopeTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
