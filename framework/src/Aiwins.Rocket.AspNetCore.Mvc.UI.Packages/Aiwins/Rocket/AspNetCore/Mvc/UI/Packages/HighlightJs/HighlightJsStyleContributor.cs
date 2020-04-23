using System.Collections.Generic;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.HighlightJs
{
    public class HighlightJsStyleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            //TODO: Make this configurable
            context.Files.AddIfNotContains("/libs/highlight.js/styles/github.css");
        }
    }
}