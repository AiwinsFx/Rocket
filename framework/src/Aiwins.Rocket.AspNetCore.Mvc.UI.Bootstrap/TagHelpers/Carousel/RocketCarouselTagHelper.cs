namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Carousel
{
    public class RocketCarouselTagHelper : RocketTagHelper<RocketCarouselTagHelper, RocketCarouselTagHelperService>
    {
        public string Id { get; set; }

        public bool? Crossfade { get; set; }

        public bool? Controls { get; set; }

        public bool? Indicators { get; set; }

        public RocketCarouselTagHelper(RocketCarouselTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
