namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Nav
{
    public class RocketNavBarTagHelper : RocketTagHelper<RocketNavBarTagHelper, RocketNavBarTagHelperService>
    {
        public RocketNavbarSize Size { get; set; } = RocketNavbarSize.Default;

        public RocketNavbarStyle NavbarStyle { get; set; } = RocketNavbarStyle.Default;

        public RocketNavBarTagHelper(RocketNavBarTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
