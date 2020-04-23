using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Button;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Dropdown
{
    public class RocketDropdownButtonTagHelper : RocketTagHelper<RocketDropdownButtonTagHelper, RocketDropdownButtonTagHelperService>
    {
        public string Text { get; set; }

        public RocketButtonSize Size { get; set; } = RocketButtonSize.Default;

        public DropdownStyle DropdownStyle { get; set; } = DropdownStyle.Single;

        public RocketButtonType ButtonType { get; set; } = RocketButtonType.Default;

        public string Icon { get; set; }

        public FontIconType IconType { get; set; } = FontIconType.FontAwesome;

        public bool? Link { get; set; }

        public bool? NavLink { get; set; }

        public RocketDropdownButtonTagHelper(RocketDropdownButtonTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
