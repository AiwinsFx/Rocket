namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Card
{
    public class RocketCardTagHelper : RocketTagHelper<RocketCardTagHelper, RocketCardTagHelperService>
    {
        public RocketCardBorderColorType Border { get; set; } = RocketCardBorderColorType.Default;

        public RocketCardTagHelper(RocketCardTagHelperService tagHelperService) 
            : base(tagHelperService)
        {
        }
    }
}
