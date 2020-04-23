using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button
{
    [HtmlTargetElement("abp-button", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class RocketButtonTagHelper : RocketTagHelper<RocketButtonTagHelper, RocketButtonTagHelperService>, IButtonTagHelperBase
    {
        public RocketButtonType ButtonType { get; set; } = RocketButtonType.Default;

        public RocketButtonSize Size { get; set; } = RocketButtonSize.Default;

        public string BusyText { get; set; }

        public string Text { get; set; }

        public string Icon { get; set; }

        public bool? Disabled { get; set; }

        public FontIconType IconType { get; set; } = FontIconType.FontAwesome;

        public RocketButtonTagHelper(RocketButtonTagHelperService service) 
            : base(service)
        {

        }
    }
}

