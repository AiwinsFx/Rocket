namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card
{
    public class RocketCardTitleTagHelper : RocketTagHelper<RocketCardTitleTagHelper, RocketCardTitleTagHelperService>
    {
        public static HtmlHeadingType DefaultHeading { get; set; } = HtmlHeadingType.H5;

        public HtmlHeadingType Heading { get; set; } = DefaultHeading;

        public RocketCardTitleTagHelper(RocketCardTitleTagHelperService tagHelperService) 
            : base(tagHelperService)
        {
        }
    }
}
