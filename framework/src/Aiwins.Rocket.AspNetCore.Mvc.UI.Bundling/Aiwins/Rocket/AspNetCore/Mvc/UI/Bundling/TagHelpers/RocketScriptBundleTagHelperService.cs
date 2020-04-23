namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling.TagHelpers
{
    public class RocketScriptBundleTagHelperService : RocketBundleTagHelperService<RocketScriptBundleTagHelper>
    {
        public RocketScriptBundleTagHelperService(RocketTagHelperScriptService resourceHelper) 
            : base(resourceHelper)
        {
        }
    }
}