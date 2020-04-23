namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Table
{
    public class RocketTableTagHelper : RocketTagHelper<RocketTableTagHelper, RocketTableTagHelperService>
    {
        public bool? Responsive { get; set; }
        public bool? ResponsiveSm { get; set; }
        public bool? ResponsiveMd { get; set; }
        public bool? ResponsiveLg { get; set; }
        public bool? ResponsiveXl { get; set; }

        public bool? DarkTheme { get; set; }

        public bool? StripedRows { get; set; }

        public bool? HoverableRows { get; set; }

        public bool? Small { get; set; }

        public RocketTableBorderStyle BorderStyle { get; set; } = RocketTableBorderStyle.Default;

        public RocketTableTagHelper(RocketTableTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
