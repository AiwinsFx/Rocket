namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Nav
{
    public class RocketNavItemTagHelper : RocketTagHelper<RocketNavItemTagHelper, RocketNavItemTagHelperService>
    {
        public bool? Dropdown { get; set; }

        public RocketNavItemTagHelper(RocketNavItemTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
