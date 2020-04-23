using Microsoft.AspNetCore.Razor.TagHelpers;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Collapse
{

    [HtmlTargetElement("abp-button", Attributes = "abp-collapse-id")]
    [HtmlTargetElement("a", Attributes = "abp-collapse-id")]
    public class RocketCollapseButtonTagHelper : RocketTagHelper<RocketCollapseButtonTagHelper, RocketCollapseButtonTagHelperService>
    {
        [HtmlAttributeName("abp-collapse-id")]
        public string BodyId { get; set; }

        public RocketCollapseButtonTagHelper(RocketCollapseButtonTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
