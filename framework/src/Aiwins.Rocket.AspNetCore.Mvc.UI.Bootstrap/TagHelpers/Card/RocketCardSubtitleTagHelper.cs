namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card
{
    public class RocketCardSubtitleTagHelper : RocketTagHelper<RocketCardSubtitleTagHelper, RocketCardSubtitleTagHelperService>
    {
        public static HtmlHeadingType DefaultHeading { get; set; } = HtmlHeadingType.H6;

        public HtmlHeadingType Heading { get; set; } = DefaultHeading;

        public RocketCardSubtitleTagHelper(RocketCardSubtitleTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}