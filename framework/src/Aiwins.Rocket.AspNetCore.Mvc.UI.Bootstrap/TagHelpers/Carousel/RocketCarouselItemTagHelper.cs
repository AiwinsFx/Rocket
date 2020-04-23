namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Carousel
{
    public class RocketCarouselItemTagHelper : RocketTagHelper<RocketCarouselItemTagHelper, RocketCarouselItemTagHelperService>
    {
        public bool? Active { get; set; }

        public string Src { get; set; }

        public string Alt { get; set; }

        public string CaptionTitle { get; set; }

        public string Caption { get; set; }

        public RocketCarouselItemTagHelper(RocketCarouselItemTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
