using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Modal
{
    [HtmlTargetElement("rocket-modal-footer")]
    public class RocketModalFooterTagHelper : RocketTagHelper<RocketModalFooterTagHelper, RocketModalFooterTagHelperService>
    {
        public RocketModalButtons Buttons { get; set; }
        public ButtonsAlign ButtonAlignment { get; set; } = ButtonsAlign.Default;

        public RocketModalFooterTagHelper(RocketModalFooterTagHelperService tagHelperService)
            : base(tagHelperService)
        {
        }
    }
}