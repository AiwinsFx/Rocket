namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling.TagHelpers
{
    public class RocketScriptTagHelperService : RocketBundleItemTagHelperService<RocketScriptTagHelper>
    {
        public RocketScriptTagHelperService(RocketTagHelperScriptService resourceService)
            : base(resourceService)
        {
        }
    }
}