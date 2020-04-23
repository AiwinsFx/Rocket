namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card
{
    public class RocketCardBodyTagHelper : RocketTagHelper<RocketCardBodyTagHelper, RocketCardBodyTagHelperService>
    {
        public string Title { get; set; }

        public string Subtitle { get; set; }

        public RocketCardBodyTagHelper(RocketCardBodyTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}