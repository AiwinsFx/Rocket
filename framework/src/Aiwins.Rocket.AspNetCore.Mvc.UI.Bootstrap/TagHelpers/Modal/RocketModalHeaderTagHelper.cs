namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Modal
{
    public class RocketModalHeaderTagHelper : RocketTagHelper<RocketModalHeaderTagHelper, RocketModalHeaderTagHelperService>
    {
        public string Title { get; set; }
        
        public RocketModalHeaderTagHelper(RocketModalHeaderTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}