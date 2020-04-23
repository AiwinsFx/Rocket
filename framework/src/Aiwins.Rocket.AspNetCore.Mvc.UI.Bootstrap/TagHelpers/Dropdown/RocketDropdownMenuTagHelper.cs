namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Dropdown
{
    public class RocketDropdownMenuTagHelper : RocketTagHelper<RocketDropdownMenuTagHelper, RocketDropdownMenuTagHelperService>
    {
            public DropdownAlign Align { get; set; } = DropdownAlign.Left;

        public RocketDropdownMenuTagHelper(RocketDropdownMenuTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
