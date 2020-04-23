using Microsoft.AspNetCore.Razor.TagHelpers;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Collapse
{

    [HtmlTargetElement("rocket-button", Attributes = "rocket-collapse-id")]
    [HtmlTargetElement("a", Attributes = "rocket-collapse-id")]
    public class RocketCollapseButtonTagHelper : RocketTagHelper<RocketCollapseButtonTagHelper, RocketCollapseButtonTagHelperService>
    {
        [HtmlAttributeName("rocket-collapse-id")]
        public string BodyId { get; set; }

        public RocketCollapseButtonTagHelper(RocketCollapseButtonTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
