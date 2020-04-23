namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Dropdown
{
    public class RocketDropdownTagHelper : RocketTagHelper<RocketDropdownTagHelper, RocketDropdownTagHelperService>
    {
        public DropdownDirection Direction { get; set; } = DropdownDirection.Down;

        public RocketDropdownTagHelper(RocketDropdownTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
