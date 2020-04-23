namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Dropdown
{
    public class RocketDropdownItemTagHelper : RocketTagHelper<RocketDropdownItemTagHelper, RocketDropdownItemTagHelperService>
    {
        public bool? Active { get; set; }

        public bool? Disabled { get; set; }

        public RocketDropdownItemTagHelper(RocketDropdownItemTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
