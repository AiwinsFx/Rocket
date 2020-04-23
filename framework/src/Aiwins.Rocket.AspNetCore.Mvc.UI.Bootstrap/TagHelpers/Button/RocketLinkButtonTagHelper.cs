using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button
{
    [HtmlTargetElement("a", Attributes = "abp-button", TagStructure = TagStructure.NormalOrSelfClosing)]
    [HtmlTargetElement("input", Attributes = "abp-button", TagStructure = TagStructure.WithoutEndTag)]
    public class RocketLinkButtonTagHelper : RocketTagHelper<RocketLinkButtonTagHelper, RocketLinkButtonTagHelperService>, IButtonTagHelperBase
    {
        [HtmlAttributeName("abp-button")]
        public RocketButtonType ButtonType { get; set; }

        public RocketButtonSize Size { get; set; } = RocketButtonSize.Default;

        public string Text { get; set; }

        public string Icon { get; set; }

        public bool? Disabled { get; set; }

        public FontIconType IconType { get; } = FontIconType.FontAwesome;

        public RocketLinkButtonTagHelper(RocketLinkButtonTagHelperService service) 
            : base(service)
        {

        }
    }
}
